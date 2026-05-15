using AgroApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Users.Commands;

public class ToggleUserCommandHandler : IRequestHandler<ToggleUserCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public ToggleUserCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<bool> Handle(
        ToggleUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u =>
                u.Id == request.UserId &&
                u.TenantId == _currentUser.TenantId,
                cancellationToken);

        if (user is null) return false;

        user.IsActive = request.IsActive;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}