using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Shifts.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Shifts.Queries;

public class GetOccurrencesQueryHandler
    : IRequestHandler<GetOccurrencesQuery, List<TaskOccurrenceDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetOccurrencesQueryHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<List<TaskOccurrenceDto>> Handle(
        GetOccurrencesQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.TaskOccurrences
            .Include(o => o.Template)
                .ThenInclude(t => t.Plot)
            .Include(o => o.Template)
                .ThenInclude(t => t.Crop)
            .Include(o => o.Assignee)
            .Where(o => o.TenantId == _currentUser.TenantId);

        if (request.OnlyMine)
            query = query.Where(o =>
                o.AssignedTo == _currentUser.UserId);

        if (request.Date.HasValue)
            query = query.Where(o =>
                o.ScheduledDate == request.Date.Value);
        else
            // Por defecto: próximos 7 días
            query = query.Where(o =>
                o.ScheduledDate >= DateOnly.FromDateTime(DateTime.UtcNow) &&
                o.ScheduledDate <= DateOnly.FromDateTime(
                    DateTime.UtcNow.AddDays(7)));

        // Si el turno exige una etapa fenológica, ocultar las ocurrencias
        // mientras el cultivo no esté en esa etapa (se generan todas de una
        // vez al crear el turno; la etapa del cultivo cambia después).
        query = query.Where(o =>
            o.Template.RequiredPhenologyStage == null ||
            _context.PhenologyStages.Any(s =>
                s.CropId == o.Template.CropId &&
                s.StageName == o.Template.RequiredPhenologyStage &&
                s.EndedAt == null));

        return await query
            .OrderBy(o => o.ScheduledDate)
            .ThenBy(o => o.Shift)
            .Select(o => new TaskOccurrenceDto(
                o.Id, o.TemplateId, o.Template.Title,
                o.Template.TaskType.ToString(),
                o.Template.Priority.ToString(),
                o.AssignedTo, o.Assignee != null ? o.Assignee.Name : null,
                o.Template.Plot != null ? o.Template.Plot.Name : null,
                o.Template.CropId,
                o.Template.Crop != null ? o.Template.Crop.CropType : null,
                o.ScheduledDate, o.Shift.ToString(),
                o.Status.ToString(), o.CompletedAt, o.Notes))
            .ToListAsync(cancellationToken);
    }
}