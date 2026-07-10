namespace AgroApp.Application.Features.Fertilization.DTOs;

public record FertilizationDto(
    Guid Id,
    Guid CropId,
    Guid UserId,
    string ProductName,
    string? ProductType,
    decimal? DoseKgHa,
    decimal? TotalKg,
    string? Method,
    decimal? Cost,
    DateTime AppliedAt,
    DateOnly? NextApplication,
    string? Notes,
    DateTime CreatedAt
);

public record CreateFertilizationRequest(
    string ProductName,
    string? ProductType,
    decimal? DoseKgHa,
    decimal? TotalKg,
    string? Method,
    decimal? Cost,
    DateTime AppliedAt,
    DateOnly? NextApplication,
    string? Notes,
    Guid? TaskId = null,
    Guid? OccurrenceId = null
);

public record UpdateFertilizationRequest(
    string ProductName,
    string? ProductType,
    decimal? DoseKgHa,
    decimal? TotalKg,
    string? Method,
    decimal? Cost,
    DateTime AppliedAt,
    DateOnly? NextApplication,
    string? Notes
);

public record SetCostRequest(decimal Cost);