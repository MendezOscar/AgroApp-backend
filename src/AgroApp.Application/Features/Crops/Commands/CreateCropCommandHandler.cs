using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Crops.DTOs;
using AgroApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Crops.Commands;

public class CreateCropCommandHandler : IRequestHandler<CreateCropCommand, CropDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public CreateCropCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<CropDto> Handle(CreateCropCommand request, CancellationToken cancellationToken)
    {
        var plotExists = await _context.Plots
            .Include(p => p.Farm)
            .AnyAsync(p => p.Id == request.PlotId
                        && p.Farm.TenantId == _currentUser.TenantId
                        && p.IsActive, cancellationToken);

        if (!plotExists)
            throw new InvalidOperationException("Parcela no encontrada.");

        var crop = new Crop
        {
            PlotId = request.PlotId,
            CropType = request.CropType,
            Variety = request.Variety,
            PlantedAt = request.PlantedAt,
            EstimatedHarvest = request.EstimatedHarvest,
            Notes = request.Notes
        };

        _context.Crops.Add(crop);
        await _context.SaveChangesAsync(cancellationToken);

        return new CropDto(
            crop.Id, crop.PlotId, crop.CropType, crop.Variety,
            crop.PlantedAt, crop.EstimatedHarvest, crop.HarvestedAt,
            crop.Status.ToString(), crop.YieldKg, crop.Notes, crop.CreatedAt);
    }
}