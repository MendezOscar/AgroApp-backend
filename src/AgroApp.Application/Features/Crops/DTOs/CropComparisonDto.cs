namespace AgroApp.Application.Features.Crops.DTOs;

public record CropComparisonDto(
    Guid Id,
    string CropType,
    string? Variety,
    string PlotName,
    string Status,
    decimal? YieldKg,
    decimal TotalCost
);
