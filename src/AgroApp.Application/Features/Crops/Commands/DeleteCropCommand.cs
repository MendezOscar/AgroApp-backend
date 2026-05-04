using MediatR;

namespace AgroApp.Application.Features.Crops.Commands;

public record DeleteCropCommand(Guid PlotId, Guid Id) : IRequest<bool>;