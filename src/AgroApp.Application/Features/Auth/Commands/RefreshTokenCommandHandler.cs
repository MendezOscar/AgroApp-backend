using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Auth.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Auth.Commands;

public class RefreshTokenCommandHandler(
    IApplicationDbContext context,
    IAuthService authService)
        : IRequestHandler<RefreshTokenCommand, AuthResponse>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IAuthService _authService = authService;

    public async Task<AuthResponse> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var refreshToken = await _context.RefreshTokens
            .Include(t => t.User)
                .ThenInclude(u => u.Tenant)
            .FirstOrDefaultAsync(
                t => t.Token == request.RefreshToken,
                cancellationToken)
            ?? throw new UnauthorizedAccessException(
                "Refresh token inválido.");

        if (!refreshToken.IsActive)
            throw new UnauthorizedAccessException(
                "Refresh token expirado o revocado.");

        var user = refreshToken.User;

        if (!user.IsActive)
            throw new UnauthorizedAccessException("Usuario inactivo.");

        // Revocar token actual
        refreshToken.IsRevoked = true;

        // Generar nuevos tokens
        var newAccessToken = _authService.GenerateJwtToken(
            user.Id, user.Email,
            user.Role.ToString(), user.TenantId);
        var newRefreshToken = _authService.CreateRefreshToken(user.Id);
        _context.RefreshTokens.Add(newRefreshToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new AuthResponse(
            newAccessToken,
            newRefreshToken.Token,
            user.Name,
            user.Email,
            user.Role.ToString(),
            user.TenantId);
    }
}