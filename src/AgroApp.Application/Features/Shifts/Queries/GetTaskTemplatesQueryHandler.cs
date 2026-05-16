using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Shifts.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Shifts.Queries;

public class GetTaskTemplatesQueryHandler
    : IRequestHandler<GetTaskTemplatesQuery, List<TaskTemplateDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetTaskTemplatesQueryHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<List<TaskTemplateDto>> Handle(
        GetTaskTemplatesQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.TaskTemplates
            .Include(t => t.Creator)
            .Include(t => t.Plot)
            .Include(t => t.Crop)
            .Include(t => t.Occurrences)
            .Where(t => t.TenantId == _currentUser.TenantId && t.IsActive)
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new TaskTemplateDto(
                t.Id, t.CreatedBy, t.Creator.Name,
                t.PlotId, t.Plot != null ? t.Plot.Name : null,
                t.CropId, t.Crop != null ? t.Crop.CropType : null,
                t.Title, t.Description,
                t.TaskType.ToString(), t.Priority.ToString(),
                t.Shift.ToString(), t.RecurrenceType.ToString(),
                t.WeekDays, t.StartDate, t.EndDate,
                t.IsActive, t.Occurrences.Count, t.CreatedAt))
            .ToListAsync(cancellationToken);
    }
}