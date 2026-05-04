using MediatR;

namespace AgroApp.Application.Features.Farms.Commands;

public record DeleteFarmCommand(Guid Id) : IRequest<bool>;