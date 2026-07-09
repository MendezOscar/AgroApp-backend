using AgroApp.Application.Features.Plots.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Plots.Queries;

public record GetFarmPlotsGeoQuery(Guid FarmId) : IRequest<List<PlotGeoDto>>;
