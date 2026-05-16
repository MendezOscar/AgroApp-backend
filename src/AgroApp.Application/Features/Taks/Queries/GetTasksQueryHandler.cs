using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Tasks.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Tasks.Queries;

public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, List<TaskDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetTasksQueryHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<List<TaskDto>> Handle(
        GetTasksQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Tasks
            .Include(t => t.Assignee)
            .Include(t => t.Creator)
            .Include(t => t.Plot)
            .Include(t => t.Crop)
            .Where(t => t.TenantId == _currentUser.TenantId);

        // Farmers solo ven sus propias tareas
        if (request.OnlyMine)
            query = query.Where(t => t.AssignedTo == _currentUser.UserId);

        if (request.Status != null)
            query = query.Where(t => t.Status.ToString() == request.Status);

        return await query
            .OrderBy(t => t.DueDate)
            .ThenByDescending(t => t.Priority)
            .Select(t => new TaskDto(
                t.Id, t.CreatedBy, t.AssignedTo,
                t.Assignee.Name, t.Creator.Name,
                t.PlotId, t.Plot != null ? t.Plot.Name : null,
                t.CropId, t.Crop != null ? t.Crop.CropType : null,
                t.Title, t.Description,
                t.Priority.ToString(), t.Status.ToString(),
                t.TaskType.ToString(),
                t.DueDate, t.CompletedAt, t.Notes, t.CreatedAt))
            .ToListAsync(cancellationToken);
    }
}