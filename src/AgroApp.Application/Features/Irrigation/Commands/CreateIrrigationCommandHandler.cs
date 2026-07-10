using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Irrigation.DTOs;
using AgroApp.Domain.Entities;
using AgroApp.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Irrigation.Commands;

public class CreateIrrigationCommandHandler : IRequestHandler<CreateIrrigationCommand, IrrigationDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;
    private readonly INotificationService _notifications;

    public CreateIrrigationCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser,
        INotificationService notifications)
    {
        _context = context;
        _currentUser = currentUser;
        _notifications = notifications;
    }

    public async Task<IrrigationDto> Handle(CreateIrrigationCommand request, CancellationToken cancellationToken)
    {
        var crop = await _context.Crops
            .Include(c => c.Plot.Farm)
            .FirstOrDefaultAsync(c => c.Id == request.CropId
                                   && c.Plot.Farm.TenantId == _currentUser.TenantId,
                                   cancellationToken);

        if (crop is null)
            throw new InvalidOperationException("Cultivo no encontrado.");

        TaskItem? task = null;
        if (request.TaskId is not null)
        {
            task = await _context.Tasks.FirstOrDefaultAsync(t =>
                t.Id == request.TaskId && t.TenantId == _currentUser.TenantId, cancellationToken);

            if (task is null)
                throw new InvalidOperationException("Tarea no encontrada.");
            if (task.TaskType != TaskType.Irrigation)
                throw new InvalidOperationException("La tarea no es de tipo Riego.");
            if (task.CropId != request.CropId)
                throw new InvalidOperationException("La tarea no corresponde a este cultivo.");
            if (task.Status == Domain.Enums.TaskStatus.Completed)
                throw new InvalidOperationException("La tarea ya está completada.");
        }

        TaskOccurrence? occurrence = null;
        if (request.OccurrenceId is not null)
        {
            occurrence = await _context.TaskOccurrences
                .Include(o => o.Template)
                .FirstOrDefaultAsync(o => o.Id == request.OccurrenceId
                                       && o.TenantId == _currentUser.TenantId, cancellationToken);

            if (occurrence is null)
                throw new InvalidOperationException("Turno no encontrado.");
            if (occurrence.Template.TaskType != TaskType.Irrigation)
                throw new InvalidOperationException("El turno no es de tipo Riego.");
            if (occurrence.Template.CropId != request.CropId)
                throw new InvalidOperationException("El turno no corresponde a este cultivo.");
            if (occurrence.Status == Domain.Enums.TaskStatus.Completed)
                throw new InvalidOperationException("El turno ya está completado.");
        }

        var irrigation = new IrrigationLog
        {
            CropId = request.CropId,
            UserId = _currentUser.UserId,
            TaskId = request.TaskId,
            Method = request.Method,
            VolumeLiters = request.VolumeLiters,
            DurationMin = request.DurationMin,
            Cost = request.Cost,
            AppliedAt = request.AppliedAt,
            Notes = request.Notes
        };

        _context.IrrigationLogs.Add(irrigation);

        if (task is not null)
        {
            task.Status = Domain.Enums.TaskStatus.Completed;
            task.CompletedAt = DateTime.UtcNow;
        }

        if (occurrence is not null)
        {
            occurrence.Status = Domain.Enums.TaskStatus.Completed;
            occurrence.CompletedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync(cancellationToken);

        if (task is not null)
        {
            await _notifications.SendToUserAsync(
                task.CreatedBy,
                title: "✅ Tarea completada",
                body: $"Riego registrado: {task.Title}",
                data: new Dictionary<string, string>
                {
                    ["taskId"] = task.Id.ToString(),
                    ["type"] = "task_completed"
                });
        }

        if (occurrence is not null)
        {
            await _notifications.SendToUserAsync(
                occurrence.Template.CreatedBy,
                title: "✅ Turno completado",
                body: $"Riego registrado: {occurrence.Template.Title}",
                data: new Dictionary<string, string>
                {
                    ["occurrenceId"] = occurrence.Id.ToString(),
                    ["type"] = "shift_completed"
                });
        }

        return new IrrigationDto(
            irrigation.Id, irrigation.CropId, irrigation.UserId,
            irrigation.Method, irrigation.VolumeLiters, irrigation.DurationMin,
            irrigation.Cost, irrigation.AppliedAt, irrigation.Notes, irrigation.CreatedAt);
    }
}