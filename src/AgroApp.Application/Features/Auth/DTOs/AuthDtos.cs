namespace AgroApp.Application.Features.Auth.DTOs;

public record RegisterRequest(
    string TenantName,
    string Name,
    string Email,
    string Password
);

public record LoginRequest(
    string Email,
    string Password
);

public record AuthResponse(
    string Token,
    string RefreshToken,
    string Name,
    string Email,
    string Role,
    Guid TenantId
);