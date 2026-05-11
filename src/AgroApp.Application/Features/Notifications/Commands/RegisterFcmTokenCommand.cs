using MediatR;

namespace AgroApp.Application.Features.Notifications.Commands;

public record RegisterFcmTokenCommand(string Token, string Platform = "mobile")
    : IRequest<bool>;