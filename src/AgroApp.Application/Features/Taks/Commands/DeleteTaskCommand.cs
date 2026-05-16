using MediatR;

namespace AgroApp.Application.Features.Tasks.Commands;

public record DeleteTaskCommand(Guid Id) : IRequest<bool>;