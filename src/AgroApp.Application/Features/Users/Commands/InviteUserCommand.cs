using MediatR;

namespace AgroApp.Application.Features.Users.Commands;

public record InviteUserCommand(
    string Name,
    string Email,
    string Password,
    string Role
) : IRequest<bool>;