using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Alerts.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Alerts.Queries;

public class GetAlertsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser) : IRequestHandler<GetAlertsQuery, List<AlertDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly ICurrentUserService _currentUser = currentUser;

    public async Task<List<AlertDto>> Handle(GetAlertsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Alerts
            .Where(a => a.TenantId == _currentUser.TenantId);

        if (request.OnlyUnread)
            query = query.Where(a => !a.IsRead);

        return await query
            .OrderByDescending(a => a.TriggeredAt)
            .Take(50)
            .Select(a => new AlertDto(
                a.Id, a.DeviceId, a.PlotId, a.AlertType,
                a.Severity.ToString(), a.Message,
                a.IsRead, a.TriggeredAt, a.ReadAt))
            .ToListAsync(cancellationToken);
    }
}