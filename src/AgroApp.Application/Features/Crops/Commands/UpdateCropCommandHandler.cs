using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Crops.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Crops.Commands;

public class UpdateCropCommandHandler : IRequestHandler<UpdateCropCommand, CropDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public UpdateCropCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<CropDto?> Handle(UpdateCropCommand request, CancellationToken cancellationToken)
    {
        var crop = await _context.Crops
            .Include(c => c.Plot.Farm)
            .FirstOrDefaultAsync(c => c.Id == request.Id
                                   && c.PlotId == request.PlotId
                                   && c.Plot.Farm.TenantId == _currentUser.TenantId,
                                   cancellationToken);

        if (crop is null) return null;

        crop.CropType = request.CropType;
        crop.Variety = request.Variety;
        crop.PlantedAt = request.PlantedAt;
        crop.EstimatedHarvest = request.EstimatedHarvest;
        crop.HarvestedAt = request.HarvestedAt;
        crop.Status = request.Status;
        crop.YieldKg = request.YieldKg;
        crop.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return new CropDto(
            crop.Id, crop.PlotId, crop.CropType, crop.Variety,
            crop.PlantedAt, crop.EstimatedHarvest, crop.HarvestedAt,
            crop.Status.ToString(), crop.YieldKg, crop.Notes, crop.CreatedAt);
    }
}