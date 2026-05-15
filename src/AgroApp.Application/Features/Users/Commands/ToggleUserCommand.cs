using MediatR;

namespace AgroApp.Application.Features.Users.Commands;

public record ToggleUserCommand(Guid UserId, bool IsActive) : IRequest<bool>;