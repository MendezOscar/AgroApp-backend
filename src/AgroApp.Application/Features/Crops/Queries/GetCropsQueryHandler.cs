using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Crops.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Crops.Queries;

public class GetCropsQueryHandler : IRequestHandler<GetCropsQuery, List<CropDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetCropsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<List<CropDto>> Handle(GetCropsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Crops
            .Where(c => c.PlotId == request.PlotId
                     && c.Plot.Farm.TenantId == _currentUser.TenantId)
            .OrderByDescending(c => c.PlantedAt)
            .Select(c => new CropDto(
                c.Id, c.PlotId, c.CropType, c.Variety,
                c.PlantedAt, c.EstimatedHarvest, c.HarvestedAt,
                c.Status.ToString(), c.YieldKg, c.Notes, c.CreatedAt))
            .ToListAsync(cancellationToken);
    }
}