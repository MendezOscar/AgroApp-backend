using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Auth.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Auth.Commands;

public class LoginCommandHandler(
    IApplicationDbContext context,
    IAuthService authService)
        : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IAuthService _authService = authService;

    public async Task<AuthResponse> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Tenant)
            .FirstOrDefaultAsync(
                u => u.Email == request.Email,
                cancellationToken)
            ?? throw new UnauthorizedAccessException(
                "Credenciales inválidas.");

        if (!user.IsActive)
            throw new UnauthorizedAccessException("Usuario inactivo.");

        if (!_authService.VerifyPassword(
                request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException(
                "Credenciales inválidas.");

        // Generar tokens
        var accessToken = _authService.GenerateJwtToken(
            user.Id, user.Email,
            user.Role.ToString(), user.TenantId);
        var refreshToken = _authService.CreateRefreshToken(user.Id);

        // Revocar tokens anteriores
        var oldTokens = await _context.RefreshTokens
            .Where(t => t.UserId == user.Id && !t.IsRevoked)
            .ToListAsync(cancellationToken);
        foreach (var old in oldTokens)
            old.IsRevoked = true;

        _context.RefreshTokens.Add(refreshToken);
        user.LastLoginAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);

        return new AuthResponse(
            accessToken,
            refreshToken.Token,
            user.Name,
            user.Email,
            user.Role.ToString(),
            user.TenantId);
    }
}