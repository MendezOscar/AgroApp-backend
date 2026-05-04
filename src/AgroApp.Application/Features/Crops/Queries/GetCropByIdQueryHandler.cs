using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Crops.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Crops.Queries;

public class GetCropByIdQueryHandler : IRequestHandler<GetCropByIdQuery, CropDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetCropByIdQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<CropDto?> Handle(GetCropByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Crops
            .Where(c => c.Id == request.Id
                     && c.PlotId == request.PlotId
                     && c.Plot.Farm.TenantId == _currentUser.TenantId)
            .Select(c => new CropDto(
                c.Id, c.PlotId, c.CropType, c.Variety,
                c.PlantedAt, c.EstimatedHarvest, c.HarvestedAt,
                c.Status.ToString(), c.YieldKg, c.Notes, c.CreatedAt))
            .FirstOrDefaultAsync(cancellationToken);
    }
}