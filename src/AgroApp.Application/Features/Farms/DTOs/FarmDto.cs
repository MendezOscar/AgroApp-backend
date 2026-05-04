namespace AgroApp.Application.Features.Farms.DTOs;

public record FarmDto(
    Guid Id,
    string Name,
    string? Description,
    double? Lat,
    double? Lng,
    decimal? AreaHa,
    string? Country,
    string? Region,
    bool IsActive,
    DateTime CreatedAt
);

public record CreateFarmRequest(
    string Name,
    string? Description,
    double? Lat,
    double? Lng,
    decimal? AreaHa,
    string? Country,
    string? Region
);

public record UpdateFarmRequest(
    string Name,
    string? Description,
    double? Lat,
    double? Lng,
    decimal? AreaHa,
    string? Country,
    string? Region
);