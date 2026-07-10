using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Shifts.DTOs;
using AgroApp.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Shifts.Commands;

public class AssignOccurrenceCommandHandler
    : IRequestHandler<AssignOccurrenceCommand, TaskOccurrenceDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;
    private readonly INotificationService _notifications;

    public AssignOccurrenceCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser,
        INotificationService notifications)
    {
        _context = context;
        _currentUser = currentUser;
        _notifications = notifications;
    }

    public async Task<TaskOccurrenceDto?> Handle(
        AssignOccurrenceCommand request,
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

        occurrence.AssignedTo = request.AssignedTo;
        if (request.Shift != null)
            occurrence.Shift = Enum.Parse<ShiftType>(request.Shift, true);

        await _context.SaveChangesAsync(cancellationToken);

        // Notificar al farmer
        var shiftLabel = occurrence.Shift == ShiftType.Day
            ? "Turno Diurno" : "Turno Nocturno";
        await _notifications.SendToUserAsync(
            request.AssignedTo,
            title: "📋 Turno asignado",
            body: $"{occurrence.Template.Title} — {occurrence.ScheduledDate:dd/MM/yyyy} {shiftLabel}",
            data: new Dictionary<string, string>
            {
                ["occurrenceId"] = occurrence.Id.ToString(),
                ["type"] = "shift_assigned"
            });

        return MapToDto(occurrence);
    }

    private static TaskOccurrenceDto MapToDto(
        Domain.Entities.TaskOccurrence o) => new(
        o.Id, o.TemplateId, o.Template.Title,
        o.Template.TaskType.ToString(),
        o.Template.Priority.ToString(),
        o.AssignedTo, o.Assignee?.Name,
        o.Template.Plot?.Name,
        o.Template.CropId,
        o.Template.Crop?.CropType,
        o.ScheduledDate, o.Shift.ToString(),
        o.Status.ToString(), o.CompletedAt, o.Notes);
}