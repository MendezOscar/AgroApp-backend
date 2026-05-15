using MediatR;

namespace AgroApp.Application.Features.Users.Commands;

public record ChangePasswordCommand(
    string CurrentPassword,
    string NewPassword
) : IRequest<bool>;