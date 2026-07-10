using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Labor.DTOs;
using AgroApp.Domain.Entities;
using AgroApp.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Labor.Commands;

public class CreateLaborCommandHandler : IRequestHandler<CreateLaborCommand, LaborDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;
    private readonly INotificationService _notifications;

    public CreateLaborCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser,
        INotificationService notifications)
    {
        _context = context;
        _currentUser = currentUser;
        _notifications = notifications;
    }

    public async Task<LaborDto> Handle(CreateLaborCommand request, CancellationToken cancellationToken)
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
            if (task.TaskType != TaskType.Labor)
                throw new InvalidOperationException("La tarea no es de tipo Labor.");
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
            if (occurrence.Template.TaskType != TaskType.Labor)
                throw new InvalidOperationException("El turno no es de tipo Labor.");
            if (occurrence.Template.CropId != request.CropId)
                throw new InvalidOperationException("El turno no corresponde a este cultivo.");
            if (occurrence.Status == Domain.Enums.TaskStatus.Completed)
                throw new InvalidOperationException("El turno ya está completado.");
        }

        var labor = new LaborLog
        {
            CropId = request.CropId,
            UserId = _currentUser.UserId,
            TaskId = request.TaskId,
            ActivityType = request.ActivityType,
            HoursWorked = request.HoursWorked,
            WorkersCount = request.WorkersCount,
            Cost = request.Cost,
            PerformedAt = request.PerformedAt,
            Notes = request.Notes
        };

        _context.LaborLogs.Add(labor);

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
                body: $"Labor registrada: {task.Title}",
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
                body: $"Labor registrada: {occurrence.Template.Title}",
                data: new Dictionary<string, string>
                {
                    ["occurrenceId"] = occurrence.Id.ToString(),
                    ["type"] = "shift_completed"
                });
        }

        return new LaborDto(
            labor.Id, labor.CropId, labor.UserId, labor.ActivityType,
            labor.HoursWorked, labor.WorkersCount, labor.Cost,
            labor.PerformedAt, labor.Notes, labor.CreatedAt);
    }
}