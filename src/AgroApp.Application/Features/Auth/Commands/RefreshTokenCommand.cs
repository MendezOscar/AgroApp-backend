using AgroApp.Application.Features.Auth.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Auth.Commands;

public record RefreshTokenCommand(string RefreshToken)
    : IRequest<AuthResponse>;