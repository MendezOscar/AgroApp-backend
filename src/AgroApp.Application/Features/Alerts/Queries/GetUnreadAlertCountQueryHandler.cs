using AgroApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Alerts.Queries;

public class GetUnreadAlertCountQueryHandler : IRequestHandler<GetUnreadAlertCountQuery, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetUnreadAlertCountQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<int> Handle(GetUnreadAlertCountQuery request, CancellationToken cancellationToken)
    {
        return await _context.Alerts
            .CountAsync(a => a.TenantId == _currentUser.TenantId && !a.IsRead, cancellationToken);
    }
}