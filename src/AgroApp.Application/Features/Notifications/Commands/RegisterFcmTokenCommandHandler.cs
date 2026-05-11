using AgroApp.Application.Common.Interfaces;
using AgroApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Notifications.Commands;

public class RegisterFcmTokenCommandHandler
    : IRequestHandler<RegisterFcmTokenCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public RegisterFcmTokenCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<bool> Handle(
        RegisterFcmTokenCommand request,
        CancellationToken cancellationToken)
    {
        // Verificar si ya existe
        var existing = await _context.FcmTokens
            .FirstOrDefaultAsync(t =>
                t.UserId == _currentUser.UserId &&
                t.Token == request.Token,
                cancellationToken);

        if (existing is not null) return true;

        var token = new FcmToken
        {
            UserId = _currentUser.UserId,
            Token = request.Token,
            Platform = request.Platform
        };

        _context.FcmTokens.Add(token);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}