using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Plots.DTOs;
using AgroApp.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Plots.Queries;

public class GetFarmPlotsGeoQueryHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUser)
        : IRequestHandler<GetFarmPlotsGeoQuery, List<PlotGeoDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly ICurrentUserService _currentUser = currentUser;

    public async Task<List<PlotGeoDto>> Handle(GetFarmPlotsGeoQuery request, CancellationToken cancellationToken)
    {
        var plots = await _context.Plots
            .Where(p => p.FarmId == request.FarmId
                     && p.Farm.TenantId == _currentUser.TenantId
                     && p.IsActive)
            .OrderBy(p => p.Name)
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.GeoJson,
                p.AreaHa,
                CurrentCrop = p.Crops
                    .Where(c => c.Status == CropStatus.Active)
                    .OrderByDescending(c => c.PlantedAt)
                    .Select(c => new { c.CropType, c.Status })
                    .FirstOrDefault(),
            })
            .ToListAsync(cancellationToken);

        return plots
            .Select(p => new PlotGeoDto(
                p.Id, p.Name, p.GeoJson, p.AreaHa,
                p.CurrentCrop?.CropType,
                p.CurrentCrop?.Status.ToString()))
            .ToList();
    }
}
