using AgroApp.Application.Common.Interfaces;
using AgroApp.Domain.Entities;
using AgroApp.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Users.Commands;

public class InviteUserCommandHandler : IRequestHandler<InviteUserCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IAuthService _authService;
    private readonly ICurrentUserService _currentUser;

    public InviteUserCommandHandler(
        IApplicationDbContext context,
        IAuthService authService,
        ICurrentUserService currentUser)
    {
        _context = context;
        _authService = authService;
        _currentUser = currentUser;
    }

    public async Task<bool> Handle(
        InviteUserCommand request,
        CancellationToken cancellationToken)
    {
        var emailExists = await _context.Users
            .AnyAsync(u => u.Email == request.Email, cancellationToken);

        if (emailExists)
            throw new InvalidOperationException("El email ya está registrado.");

        var user = new User
        {
            TenantId = _currentUser.TenantId,
            Name = request.Name,
            Email = request.Email,
            PasswordHash = _authService.HashPassword(request.Password),
            Role = Enum.Parse<UserRole>(request.Role, true)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}