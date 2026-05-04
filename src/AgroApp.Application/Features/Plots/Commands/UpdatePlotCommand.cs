using AgroApp.Application.Features.Plots.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Plots.Commands;

public record UpdatePlotCommand(
    Guid FarmId,
    Guid Id,
    string Name,
    string? SoilType,
    decimal? AreaHa,
    string? GeoJson,
    string? Notes
) : IRequest<PlotDto?>;