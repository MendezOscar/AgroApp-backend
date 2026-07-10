using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Costs.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Costs.Queries;

public class GetMonthlyCostHistoryQueryHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUser)
        : IRequestHandler<GetMonthlyCostHistoryQuery, List<MonthlyCostDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly ICurrentUserService _currentUser = currentUser;

    public async Task<List<MonthlyCostDto>> Handle(
        GetMonthlyCostHistoryQuery request,
        CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var since = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc)
            .AddMonths(-(request.Months - 1));

        var fertilizationByMonth = await _context.FertilizationLogs
            .Where(f => f.Crop.Plot.Farm.TenantId == _currentUser.TenantId
                     && f.AppliedAt >= since)
            .GroupBy(f => new { f.AppliedAt.Year, f.AppliedAt.Month })
            .Select(g => new
            {
                g.Key.Year,
                g.Key.Month,
                Cost = g.Sum(f => f.Cost ?? 0)
            })
            .ToListAsync(cancellationToken);

        var laborByMonth = await _context.LaborLogs
            .Where(l => l.Crop.Plot.Farm.TenantId == _currentUser.TenantId
                     && l.PerformedAt >= since)
            .GroupBy(l => new { l.PerformedAt.Year, l.PerformedAt.Month })
            .Select(g => new
            {
                g.Key.Year,
                g.Key.Month,
                Cost = g.Sum(l => l.Cost ?? 0)
            })
            .ToListAsync(cancellationToken);

        var irrigationByMonth = await _context.IrrigationLogs
            .Where(i => i.Crop.Plot.Farm.TenantId == _currentUser.TenantId
                     && i.AppliedAt >= since)
            .GroupBy(i => new { i.AppliedAt.Year, i.AppliedAt.Month })
            .Select(g => new
            {
                g.Key.Year,
                g.Key.Month,
                Cost = g.Sum(i => i.Cost ?? 0)
            })
            .ToListAsync(cancellationToken);

        var result = new List<MonthlyCostDto>();
        for (var i = request.Months - 1; i >= 0; i--)
        {
            var month = now.AddMonths(-i);

            var fertCost = fertilizationByMonth
                .FirstOrDefault(f => f.Year == month.Year && f.Month == month.Month)
                ?.Cost ?? 0;
            var laborCost = laborByMonth
                .FirstOrDefault(l => l.Year == month.Year && l.Month == month.Month)
                ?.Cost ?? 0;
            var irrigationCost = irrigationByMonth
                .FirstOrDefault(w => w.Year == month.Year && w.Month == month.Month)
                ?.Cost ?? 0;

            result.Add(new MonthlyCostDto(
                month.Year, month.Month, fertCost, laborCost, irrigationCost,
                fertCost + laborCost + irrigationCost));
        }

        return result;
    }
}
