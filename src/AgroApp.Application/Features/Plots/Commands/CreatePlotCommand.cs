using AgroApp.Application.Features.Plots.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Plots.Commands;

public record CreatePlotCommand(
    Guid FarmId,
    string Name,
    string? SoilType,
    decimal? AreaHa,
    string? GeoJson,
    string? Notes
) : IRequest<PlotDto>;