using AgroApp.Domain.Enums;

namespace AgroApp.Application.Features.Crops.DTOs;

public record CropDto(
    Guid Id,
    Guid PlotId,
    string CropType,
    string? Variety,
    DateOnly PlantedAt,
    DateOnly? EstimatedHarvest,
    DateOnly? HarvestedAt,
    string Status,
    decimal? YieldKg,
    string? Notes,
    DateTime CreatedAt
);

public record CreateCropRequest(
    string CropType,
    string? Variety,
    DateOnly PlantedAt,
    DateOnly? EstimatedHarvest,
    string? Notes
);

public record UpdateCropRequest(
    string CropType,
    string? Variety,
    DateOnly PlantedAt,
    DateOnly? EstimatedHarvest,
    DateOnly? HarvestedAt,
    CropStatus Status,
    decimal? YieldKg,
    string? Notes
);