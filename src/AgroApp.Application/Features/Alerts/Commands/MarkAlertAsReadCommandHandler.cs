using AgroApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Alerts.Commands;

public class MarkAlertAsReadCommandHandler : IRequestHandler<MarkAlertAsReadCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public MarkAlertAsReadCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<bool> Handle(MarkAlertAsReadCommand request, CancellationToken cancellationToken)
    {
        var alert = await _context.Alerts
            .FirstOrDefaultAsync(a => a.Id == request.Id
                                   && a.TenantId == _currentUser.TenantId,
                                   cancellationToken);

        if (alert is null) return false;

        alert.IsRead = true;
        alert.ReadAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}