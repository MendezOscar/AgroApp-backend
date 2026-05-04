using AgroApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Irrigation.Commands;

public class DeleteIrrigationCommandHandler : IRequestHandler<DeleteIrrigationCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public DeleteIrrigationCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<bool> Handle(DeleteIrrigationCommand request, CancellationToken cancellationToken)
    {
        var irrigation = await _context.IrrigationLogs
            .Include(i => i.Crop.Plot.Farm)
            .FirstOrDefaultAsync(i => i.Id == request.Id
                                   && i.CropId == request.CropId
                                   && i.Crop.Plot.Farm.TenantId == _currentUser.TenantId,
                                   cancellationToken);

        if (irrigation is null) return false;

        _context.IrrigationLogs.Remove(irrigation);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}