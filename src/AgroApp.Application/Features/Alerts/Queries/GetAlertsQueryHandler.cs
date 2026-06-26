using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Common.Models;
using AgroApp.Application.Features.Alerts.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Alerts.Queries;

public class GetAlertsQueryHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUser)
        : IRequestHandler<GetAlertsQuery, PagedResult<AlertDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly ICurrentUserService _currentUser = currentUser;

    public async Task<PagedResult<AlertDto>> Handle(
        GetAlertsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Alerts
            .Where(a => a.TenantId == _currentUser.TenantId);

        if (request.OnlyUnread)
            query = query.Where(a => !a.IsRead);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(a => a.TriggeredAt)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(a => new AlertDto(
                a.Id, a.DeviceId, a.PlotId, a.AlertType,
                a.Severity.ToString(), a.Message,
                a.IsRead, a.TriggeredAt, a.ReadAt))
            .ToListAsync(cancellationToken);

        return new PagedResult<AlertDto>(
            items,
            totalCount,
            request.Page,
            request.PageSize,
            request.Page * request.PageSize < totalCount);
    }
}