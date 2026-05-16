using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Tasks.DTOs;
using AgroApp.Domain.Entities;
using AgroApp.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Tasks.Commands;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;
    private readonly INotificationService _notifications;

    public CreateTaskCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser,
        INotificationService notifications)
    {
        _context = context;
        _currentUser = currentUser;
        _notifications = notifications;
    }

    public async Task<TaskDto> Handle(
        CreateTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = new TaskItem
        {
            TenantId = _currentUser.TenantId,
            CreatedBy = _currentUser.UserId,
            AssignedTo = request.AssignedTo,
            PlotId = request.PlotId,
            CropId = request.CropId,
            Title = request.Title,
            Description = request.Description,
            Priority = Enum.Parse<TaskPriority>(request.Priority, true),
            TaskType = Enum.Parse<TaskType>(request.TaskType, true),  // ← nuevo
            DueDate = request.DueDate,
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync(cancellationToken);

        // Recargar con navegación
        var saved = await _context.Tasks
            .Include(t => t.Assignee)
            .Include(t => t.Creator)
            .Include(t => t.Plot)
            .Include(t => t.Crop)
            .FirstAsync(t => t.Id == task.Id, cancellationToken);

        // Notificar al farmer asignado
        await _notifications.SendToUserAsync(
            request.AssignedTo,
            title: "📋 Nueva tarea asignada",
            body: $"{task.Title} — vence el {task.DueDate:dd/MM/yyyy}",
            data: new Dictionary<string, string>
            {
                ["taskId"] = task.Id.ToString(),
                ["type"] = "task_assigned"
            });

        return new TaskDto(
            saved.Id, saved.CreatedBy, saved.AssignedTo,
            saved.Assignee.Name, saved.Creator.Name,
            saved.PlotId, saved.Plot?.Name,
            saved.CropId, saved.Crop?.CropType,
            saved.Title, saved.Description,
            saved.Priority.ToString(), saved.Status.ToString(),
            saved.TaskType.ToString(),   // ← nuevo
            saved.DueDate, saved.CompletedAt,
            saved.Notes, saved.CreatedAt);
    }
}