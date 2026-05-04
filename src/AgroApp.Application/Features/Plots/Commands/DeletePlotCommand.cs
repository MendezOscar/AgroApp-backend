using MediatR;

namespace AgroApp.Application.Features.Plots.Commands;

public record DeletePlotCommand(Guid FarmId, Guid Id) : IRequest<bool>;