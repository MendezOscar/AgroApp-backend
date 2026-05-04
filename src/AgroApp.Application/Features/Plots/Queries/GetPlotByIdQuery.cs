using AgroApp.Application.Features.Plots.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Plots.Queries;

public record GetPlotByIdQuery(Guid FarmId, Guid Id) : IRequest<PlotDto?>;