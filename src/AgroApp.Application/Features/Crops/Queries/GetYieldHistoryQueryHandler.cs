using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Crops.DTOs;
using AgroApp.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Crops.Queries;

public class GetYieldHistoryQueryHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUser)
        : IRequestHandler<GetYieldHistoryQuery, List<YieldHistoryDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly ICurrentUserService _currentUser = currentUser;

    public async Task<List<YieldHistoryDto>> Handle(GetYieldHistoryQuery request, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var since = DateOnly.FromDateTime(new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc))
            .AddMonths(-(request.Months - 1));

        var harvestsByMonth = await _context.Crops
            .Where(c => c.Plot.FarmId == request.FarmId
                     && c.Plot.Farm.TenantId == _currentUser.TenantId
                     && c.Status == CropStatus.Harvested
                     && c.HarvestedAt != null
                     && c.HarvestedAt >= since)
            .GroupBy(c => new { c.HarvestedAt!.Value.Year, c.HarvestedAt!.Value.Month })
            .Select(g => new
            {
                g.Key.Year,
                g.Key.Month,
                TotalYieldKg = g.Sum(c => c.YieldKg ?? 0),
                Count = g.Count(),
            })
            .ToListAsync(cancellationToken);

        var result = new List<YieldHistoryDto>();
        for (var i = request.Months - 1; i >= 0; i--)
        {
            var month = now.AddMonths(-i);
            var match = harvestsByMonth.FirstOrDefault(h => h.Year == month.Year && h.Month == month.Month);
            result.Add(new YieldHistoryDto(month.Year, month.Month, match?.TotalYieldKg ?? 0, match?.Count ?? 0));
        }

        return result;
    }
}
