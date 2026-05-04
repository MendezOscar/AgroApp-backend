using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Auth.DTOs;
using AgroApp.Domain.Entities;
using AgroApp.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Auth.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IAuthService _authService;

    public RegisterCommandHandler(IApplicationDbContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Verificar si el email ya existe
        var emailExists = await _context.Users
            .AnyAsync(u => u.Email == request.Email, cancellationToken);

        if (emailExists)
            throw new InvalidOperationException("El email ya está registrado.");

        // Crear tenant
        var slug = request.TenantName.ToLower()
            .Replace(" ", "-")
            .Replace("á", "a").Replace("é", "e")
            .Replace("í", "i").Replace("ó", "o")
            .Replace("ú", "u");

        var tenant = new Tenant
        {
            Name = request.TenantName,
            Slug = $"{slug}-{Guid.NewGuid().ToString()[..6]}",
            Plan = "free"
        };

        _context.Tenants.Add(tenant);

        // Crear usuario admin del tenant
        var user = new User
        {
            TenantId = tenant.Id,
            Name = request.Name,
            Email = request.Email,
            PasswordHash = _authService.HashPassword(request.Password),
            Role = UserRole.Admin
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        var token = _authService.GenerateJwtToken(
            user.Id, user.Email, user.Role.ToString(), tenant.Id);

        return new AuthResponse(token, user.Name, user.Email, user.Role.ToString(), tenant.Id);
    }
}