using AgroApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Labor.Commands;

public class DeleteLaborCommandHandler : IRequestHandler<DeleteLaborCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public DeleteLaborCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<bool> Handle(DeleteLaborCommand request, CancellationToken cancellationToken)
    {
        var labor = await _context.LaborLogs
            .Include(l => l.Crop.Plot.Farm)
            .FirstOrDefaultAsync(l => l.Id == request.Id
                                   && l.CropId == request.CropId
                                   && l.Crop.Plot.Farm.TenantId == _currentUser.TenantId,
                                   cancellationToken);

        if (labor is null) return false;

        _context.LaborLogs.Remove(labor);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}