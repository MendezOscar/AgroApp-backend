namespace AgroApp.Application.Features.Plots.DTOs;

public record PlotDto(
    Guid Id,
    Guid FarmId,
    string Name,
    string? SoilType,
    decimal? AreaHa,
    string? GeoJson,
    string? Notes,
    bool IsActive,
    DateTime CreatedAt
);

public record CreatePlotRequest(
    string Name,
    string? SoilType,
    decimal? AreaHa,
    string? GeoJson,
    string? Notes
);

public record UpdatePlotRequest(
    string Name,
    string? SoilType,
    decimal? AreaHa,
    string? GeoJson,
    string? Notes
);