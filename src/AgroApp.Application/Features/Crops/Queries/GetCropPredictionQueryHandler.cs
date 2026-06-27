using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Crops.DTOs;
using AgroApp.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Crops.Queries;

public class GetCropPredictionQueryHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUser)
        : IRequestHandler<GetCropPredictionQuery, CropPredictionDto?>
{
    private readonly IApplicationDbContext _context = context;
    private readonly ICurrentUserService _currentUser = currentUser;

    public async Task<CropPredictionDto?> Handle(
        GetCropPredictionQuery request,
        CancellationToken cancellationToken)
    {
        var crop = await _context.Crops
            .Where(c => c.Id == request.CropId
                     && c.PlotId == request.PlotId
                     && c.Plot.Farm.TenantId == _currentUser.TenantId)
            .Select(c => new { c.CropType, c.PlantedAt, c.EstimatedHarvest })
            .FirstOrDefaultAsync(cancellationToken);

        if (crop is null) return null;

        var historicalYields = await _context.Crops
            .Where(c => c.CropType == crop.CropType
                     && c.Status == CropStatus.Harvested
                     && c.YieldKg != null
                     && c.Plot.Farm.TenantId == _currentUser.TenantId)
            .Select(c => c.YieldKg!.Value)
            .ToListAsync(cancellationToken);

        decimal? predictedYieldKg = null;
        string? yieldBasis = null;
        if (historicalYields.Count > 0)
        {
            predictedYieldKg = historicalYields.Average();
            yieldBasis = $"Promedio histórico de {historicalYields.Count} cosecha(s) de {crop.CropType}";
        }

        DateOnly? predictedHarvestDate = null;
        string? harvestBasis = null;
        if (crop.EstimatedHarvest is not null)
        {
            predictedHarvestDate = crop.EstimatedHarvest;
            harvestBasis = "Fecha estimada registrada manualmente";
        }
        else
        {
            var cycleDays = await _context.PhenologyTemplates
                .Where(t => t.CropType == crop.CropType.ToLower())
                .SumAsync(t => t.MaxDays, cancellationToken);

            if (cycleDays > 0)
            {
                predictedHarvestDate = crop.PlantedAt.AddDays(cycleDays);
                harvestBasis = $"Estimado según plantilla de fenología ({cycleDays} días de ciclo)";
            }
        }

        return new CropPredictionDto(predictedYieldKg, yieldBasis, predictedHarvestDate, harvestBasis);
    }
}
