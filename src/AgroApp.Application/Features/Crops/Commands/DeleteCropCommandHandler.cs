using AgroApp.Application.Common.Interfaces;
using AgroApp.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Crops.Commands;

public class DeleteCropCommandHandler : IRequestHandler<DeleteCropCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public DeleteCropCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<bool> Handle(DeleteCropCommand request, CancellationToken cancellationToken)
    {
        var crop = await _context.Crops
            .Include(c => c.Plot.Farm)
            .FirstOrDefaultAsync(c => c.Id == request.Id
                                   && c.PlotId == request.PlotId
                                   && c.Plot.Farm.TenantId == _currentUser.TenantId,
                                   cancellationToken);

        if (crop is null) return false;

        // Soft delete — marcar como cancelado
        crop.Status = CropStatus.Cancelled;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}