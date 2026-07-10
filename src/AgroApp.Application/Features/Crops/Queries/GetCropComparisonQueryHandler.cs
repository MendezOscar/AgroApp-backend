using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Crops.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Crops.Queries;

public class GetCropComparisonQueryHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUser)
        : IRequestHandler<GetCropComparisonQuery, List<CropComparisonDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly ICurrentUserService _currentUser = currentUser;

    public async Task<List<CropComparisonDto>> Handle(
        GetCropComparisonQuery request,
        CancellationToken cancellationToken)
    {
        var crops = await _context.Crops
            .Where(c => c.Plot.FarmId == request.FarmId
                     && c.Plot.Farm.TenantId == _currentUser.TenantId)
            .Select(c => new
            {
                c.Id,
                c.CropType,
                c.Variety,
                PlotName = c.Plot.Name,
                c.Status,
                c.YieldKg,
                c.Plot.AreaHa
            })
            .ToListAsync(cancellationToken);

        var fertCostByCrop = await _context.FertilizationLogs
            .Where(f => f.Crop.Plot.FarmId == request.FarmId
                     && f.Crop.Plot.Farm.TenantId == _currentUser.TenantId)
            .GroupBy(f => f.CropId)
            .Select(g => new { CropId = g.Key, Cost = g.Sum(f => f.Cost ?? 0) })
            .ToDictionaryAsync(x => x.CropId, x => x.Cost, cancellationToken);

        var laborCostByCrop = await _context.LaborLogs
            .Where(l => l.Crop.Plot.FarmId == request.FarmId
                     && l.Crop.Plot.Farm.TenantId == _currentUser.TenantId)
            .GroupBy(l => l.CropId)
            .Select(g => new { CropId = g.Key, Cost = g.Sum(l => l.Cost ?? 0) })
            .ToDictionaryAsync(x => x.CropId, x => x.Cost, cancellationToken);

        var irrigationCostByCrop = await _context.IrrigationLogs
            .Where(i => i.Crop.Plot.FarmId == request.FarmId
                     && i.Crop.Plot.Farm.TenantId == _currentUser.TenantId)
            .GroupBy(i => i.CropId)
            .Select(g => new { CropId = g.Key, Cost = g.Sum(i => i.Cost ?? 0) })
            .ToDictionaryAsync(x => x.CropId, x => x.Cost, cancellationToken);

        var revenueByCrop = await _context.CropSales
            .Where(s => s.Crop.Plot.FarmId == request.FarmId
                     && s.Crop.Plot.Farm.TenantId == _currentUser.TenantId)
            .GroupBy(s => s.CropId)
            .Select(g => new { CropId = g.Key, Revenue = g.Sum(s => s.QuantityKg * s.PricePerKg) })
            .ToDictionaryAsync(x => x.CropId, x => x.Revenue, cancellationToken);

        return crops.Select(c =>
        {
            var totalCost = fertCostByCrop.GetValueOrDefault(c.Id)
                           + laborCostByCrop.GetValueOrDefault(c.Id)
                           + irrigationCostByCrop.GetValueOrDefault(c.Id);
            var totalRevenue = revenueByCrop.GetValueOrDefault(c.Id);
            var margin = totalRevenue - totalCost;

            var areaHa = c.AreaHa is > 0 ? c.AreaHa : null;

            return new CropComparisonDto(
                c.Id, c.CropType, c.Variety, c.PlotName,
                c.Status.ToString(), c.YieldKg, totalCost,
                c.AreaHa,
                areaHa is null ? null : c.YieldKg / areaHa,
                areaHa is null ? null : totalCost / areaHa,
                totalRevenue, margin,
                areaHa is null ? null : margin / areaHa);
        }).ToList();
    }
}
