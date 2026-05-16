using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Shifts.DTOs;
using AgroApp.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Shifts.Commands;

public class UpdateOccurrenceStatusCommandHandler
    : IRequestHandler<UpdateOccurrenceStatusCommand, TaskOccurrenceDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;
    private readonly INotificationService _notifications;

    public UpdateOccurrenceStatusCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser,
        INotificationService notifications)
    {
        _context = context;
        _currentUser = currentUser;
        _notifications = notifications;
    }

    public async Task<TaskOccurrenceDto?> Handle(
        UpdateOccurrenceStatusCommand request,
        CancellationToken cancellationToken)
    {
        var occurrence = await _context.TaskOccurrences
            .Include(o => o.Template)
                .ThenInclude(t => t.Plot)
            .Include(o => o.Template)
                .ThenInclude(t => t.Crop)
            .Include(o => o.Assignee)
            .FirstOrDefaultAsync(o =>
                o.Id == request.OccurrenceId &&
                o.TenantId == _currentUser.TenantId,
                cancellationToken);

        if (occurrence is null) return null;

        var newStatus = Enum.Parse<Domain.Enums.TaskStatus>(request.Status, true);
        occurrence.Status = newStatus;
        occurrence.Notes  = request.Notes ?? occurrence.Notes;

        if (newStatus == Domain.Enums.TaskStatus.Completed)
            occurrence.CompletedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        // Notificar al creador de la plantilla
        if (newStatus == Domain.Enums.TaskStatus.Completed)
        {
            await _notifications.SendToUserAsync(
                occurrence.Template.CreatedBy,
                title: "✅ Turno completado",
                body: $"{occurrence.Assignee?.Name} completó: {occurrence.Template.Title}",
                data: new Dictionary<string, string>
                {
                    ["occurrenceId"] = occurrence.Id.ToString(),
                    ["type"] = "shift_completed"
                });
        }

        return new TaskOccurrenceDto(
            occurrence.Id, occurrence.TemplateId,
            occurrence.Template.Title,
            occurrence.Template.TaskType.ToString(),
            occurrence.Template.Priority.ToString(),
            occurrence.AssignedTo, occurrence.Assignee?.Name,
            occurrence.Template.Plot?.Name,
            occurrence.Template.Crop?.CropType,
            occurrence.ScheduledDate, occurrence.Shift.ToString(),
            occurrence.Status.ToString(),
            occurrence.CompletedAt, occurrence.Notes);
    }
}