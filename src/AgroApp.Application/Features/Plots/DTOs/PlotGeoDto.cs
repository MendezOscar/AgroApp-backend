namespace AgroApp.Application.Features.Plots.DTOs;

public record PlotGeoDto(
    Guid Id,
    string Name,
    string? GeoJson,
    decimal? AreaHa,
    string? CurrentCropType,
    string? Status
);
