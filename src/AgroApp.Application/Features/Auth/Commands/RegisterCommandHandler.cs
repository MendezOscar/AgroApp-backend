using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Auth.DTOs;
using AgroApp.Domain.Entities;
using AgroApp.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Auth.Commands;

public class RegisterCommandHandler(
    IApplicationDbContext context,
    IAuthService authService)
        : IRequestHandler<RegisterCommand, AuthResponse>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IAuthService _authService = authService;

    public async Task<AuthResponse> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        // Verificar si el email ya existe
        var emailExists = await _context.Users
            .AnyAsync(u => u.Email == request.Email,
                cancellationToken);
        if (emailExists)
            throw new InvalidOperationException(
                "El email ya está registrado.");

        // Crear tenant
        var tenant = new Tenant
        {
            Name = request.TenantName,
            Slug = request.TenantName
                .ToLower()
                .Replace(" ", "-"),
        };
        _context.Tenants.Add(tenant);

        // Crear usuario Admin
        var user = new User
        {
            TenantId = tenant.Id,
            Name = request.Name,
            Email = request.Email,
            PasswordHash = _authService.HashPassword(request.Password),
            Role = UserRole.Admin,
        };
        _context.Users.Add(user);

        // Generar tokens
        var accessToken = _authService.GenerateJwtToken(
            user.Id, user.Email,
            user.Role.ToString(), user.TenantId);
        var refreshToken = _authService.CreateRefreshToken(user.Id);
        _context.RefreshTokens.Add(refreshToken); // ← nuevo

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