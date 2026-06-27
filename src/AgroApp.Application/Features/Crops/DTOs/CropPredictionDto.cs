namespace AgroApp.Application.Features.Crops.DTOs;

public record CropPredictionDto(
    decimal? PredictedYieldKg,
    string? YieldBasis,
    DateOnly? PredictedHarvestDate,
    string? HarvestBasis
);
