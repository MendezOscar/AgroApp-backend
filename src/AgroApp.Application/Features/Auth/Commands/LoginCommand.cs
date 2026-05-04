using AgroApp.Application.Features.Auth.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Auth.Commands;

public record LoginCommand(
    string Email,
    string Password
) : IRequest<AuthResponse>;