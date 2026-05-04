using AgroApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Farms.Commands;

public class DeleteFarmCommandHandler : IRequestHandler<DeleteFarmCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public DeleteFarmCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<bool> Handle(DeleteFarmCommand request, CancellationToken cancellationToken)
    {
        var farm = await _context.Farms
            .FirstOrDefaultAsync(f => f.Id == request.Id
                                   && f.TenantId == _currentUser.TenantId
                                   && f.IsActive, cancellationToken);

        if (farm is null) return false;

        // Soft delete
        farm.IsActive = false;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}