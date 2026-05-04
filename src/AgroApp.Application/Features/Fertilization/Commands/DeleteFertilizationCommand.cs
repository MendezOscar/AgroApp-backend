using MediatR;

namespace AgroApp.Application.Features.Fertilization.Commands;

public record DeleteFertilizationCommand(Guid CropId, Guid Id) : IRequest<bool>;