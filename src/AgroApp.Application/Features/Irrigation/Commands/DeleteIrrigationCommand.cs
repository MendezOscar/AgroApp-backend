using MediatR;

namespace AgroApp.Application.Features.Irrigation.Commands;

public record DeleteIrrigationCommand(Guid CropId, Guid Id) : IRequest<bool>;