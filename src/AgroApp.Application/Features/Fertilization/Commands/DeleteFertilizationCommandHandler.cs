using AgroApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Fertilization.Commands;

public class DeleteFertilizationCommandHandler : IRequestHandler<DeleteFertilizationCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public DeleteFertilizationCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<bool> Handle(DeleteFertilizationCommand request, CancellationToken cancellationToken)
    {
        var log = await _context.FertilizationLogs
            .Include(f => f.Crop.Plot.Farm)
            .FirstOrDefaultAsync(f => f.Id == request.Id
                                   && f.CropId == request.CropId
                                   && f.Crop.Plot.Farm.TenantId == _currentUser.TenantId,
                                   cancellationToken);

        if (log is null) return false;

        _context.FertilizationLogs.Remove(log);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}