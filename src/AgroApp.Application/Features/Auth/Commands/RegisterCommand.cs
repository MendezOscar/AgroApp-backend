using AgroApp.Application.Features.Auth.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Auth.Commands;

public record RegisterCommand(
    string TenantName,
    string Name,
    string Email,
    string Password
) : IRequest<AuthResponse>;