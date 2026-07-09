using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Tasks.DTOs;
using AgroApp.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Tasks.Commands;

public class UpdateTaskStatusCommandHandler
    : IRequestHandler<UpdateTaskStatusCommand, TaskDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;
    private readonly INotificationService _notifications;

    public UpdateTaskStatusCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser,
        INotificationService notifications)
    {
        _context = context;
        _currentUser = currentUser;
        _notifications = notifications;
    }

    public async Task<TaskDto?> Handle(
        UpdateTaskStatusCommand request,
        CancellationToken cancellationToken)
    {
        var task = await _context.Tasks
            .Include(t => t.Assignee)
            .Include(t => t.Creator)
            .Include(t => t.Plot)
            .Include(t => t.Crop)
            .FirstOrDefaultAsync(t =>
                t.Id == request.Id &&
                t.TenantId == _currentUser.TenantId,
                cancellationToken);

        if (task is null) return null;

        var newStatus = Enum.Parse<Domain.Enums.TaskStatus>(request.Status, true);

        var requiresRegistration = task.TaskType is TaskType.Irrigation
            or TaskType.Fertilization or TaskType.Labor;
        var isCompletingNow = newStatus == Domain.Enums.TaskStatus.Completed
            && task.Status != Domain.Enums.TaskStatus.Completed;
        if (isCompletingNow && requiresRegistration)
            throw new InvalidOperationException(
                "Esta tarea debe completarse registrando la actividad correspondiente.");

        task.Status = newStatus;
        task.Notes  = request.Notes ?? task.Notes;

        if (newStatus == Domain.Enums.TaskStatus.Completed)
            task.CompletedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        // Notificar al creador cuando se completa
        if (newStatus == Domain.Enums.TaskStatus.Completed)
        {
            await _notifications.SendToUserAsync(
                task.CreatedBy,
                title: "✅ Tarea completada",
                body: $"{task.Assignee.Name} completó: {task.Title}",
                data: new Dictionary<string, string>
                {
                    ["taskId"] = task.Id.ToString(),
                    ["type"]   = "task_completed"
                });
        }

        return new TaskDto(
            task.Id, task.CreatedBy, task.AssignedTo,
            task.Assignee.Name, task.Creator.Name,
            task.PlotId, task.Plot?.Name,
            task.CropId, task.Crop?.CropType,
            task.Title, task.Description,
            task.Priority.ToString(), task.Status.ToString(),
            task.TaskType.ToString(),
            task.DueDate, task.CompletedAt,
            task.Notes, task.CreatedAt);
    }
}