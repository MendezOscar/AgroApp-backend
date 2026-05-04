using AgroApp.Application.Features.Plots.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Plots.Queries;

public record GetPlotsQuery(Guid FarmId) : IRequest<List<PlotDto>>;