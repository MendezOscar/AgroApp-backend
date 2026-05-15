using AgroApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Users.Commands;

public class ChangePasswordCommandHandler
    : IRequestHandler<ChangePasswordCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IAuthService _authService;
    private readonly ICurrentUserService _currentUser;

    public ChangePasswordCommandHandler(
        IApplicationDbContext context,
        IAuthService authService,
        ICurrentUserService currentUser)
    {
        _context = context;
        _authService = authService;
        _currentUser = currentUser;
    }

    public async Task<bool> Handle(
        ChangePasswordCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == _currentUser.UserId,
                cancellationToken);

        if (user is null) return false;

        if (!_authService.VerifyPassword(
                request.CurrentPassword, user.PasswordHash))
            throw new UnauthorizedAccessException(
                "La contraseña actual es incorrecta.");

        user.PasswordHash =
            _authService.HashPassword(request.NewPassword);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}