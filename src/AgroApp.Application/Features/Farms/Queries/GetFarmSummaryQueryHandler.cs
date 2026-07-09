using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Farms.DTOs;
using AgroApp.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Farms.Queries;

public class GetFarmSummaryQueryHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUser)
        : IRequestHandler<GetFarmSummaryQuery, FarmSummaryDto?>
{
    private readonly IApplicationDbContext _context = context;
    private readonly ICurrentUserService _currentUser = currentUser;

    public async Task<FarmSummaryDto?> Handle(GetFarmSummaryQuery request, CancellationToken cancellationToken)
    {
        var farm = await _context.Farms
            .Where(f => f.Id == request.FarmId && f.TenantId == _currentUser.TenantId && f.IsActive)
            .FirstOrDefaultAsync(cancellationToken);

        if (farm is null) return null;

        var plots = await _context.Plots
            .Where(p => p.FarmId == request.FarmId)
            .Select(p => new { p.AreaHa, p.IsActive })
            .ToListAsync(cancellationToken);

        var activeCropCount = await _context.Crops
            .CountAsync(c => c.Plot.FarmId == request.FarmId && c.Status == CropStatus.Active, cancellationToken);

        var now = DateTime.UtcNow;
        var monthStart = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);

        var fertCost = await _context.FertilizationLogs
            .Where(f => f.Crop.Plot.FarmId == request.FarmId && f.AppliedAt >= monthStart)
            .SumAsync(f => f.Cost ?? 0, cancellationToken);

        var laborCost = await _context.LaborLogs
            .Where(l => l.Crop.Plot.FarmId == request.FarmId && l.PerformedAt >= monthStart)
            .SumAsync(l => l.Cost ?? 0, cancellationToken);

        var unresolvedAlertCount = await _context.Alerts
            .CountAsync(a => a.Plot != null && a.Plot.FarmId == request.FarmId && !a.IsRead, cancellationToken);

        return new FarmSummaryDto(
            farm.Id,
            farm.Name,
            plots.Sum(p => p.AreaHa ?? 0),
            plots.Count,
            plots.Count(p => p.IsActive),
            activeCropCount,
            fertCost + laborCost,
            unresolvedAlertCount);
    }
}
