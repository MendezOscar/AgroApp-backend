using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Costs.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Costs.Queries;

public class GetPendingCostActivitiesQueryHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUser)
        : IRequestHandler<GetPendingCostActivitiesQuery, List<PendingCostActivityDto>>
{
    public async Task<List<PendingCostActivityDto>> Handle(
        GetPendingCostActivitiesQuery request,
        CancellationToken cancellationToken)
    {
        var irrigations = await context.IrrigationLogs
            .Where(i => i.Cost == null
                     && i.Crop.Plot.FarmId == request.FarmId
                     && i.Crop.Plot.Farm.TenantId == currentUser.TenantId)
            .Select(i => new PendingCostActivityDto(
                i.Id, "Irrigation", i.CropId, i.Crop.CropType, i.Crop.Plot.Name,
                i.AppliedAt, i.Method))
            .ToListAsync(cancellationToken);

        var fertilizations = await context.FertilizationLogs
            .Where(f => f.Cost == null
                     && f.Crop.Plot.FarmId == request.FarmId
                     && f.Crop.Plot.Farm.TenantId == currentUser.TenantId)
            .Select(f => new PendingCostActivityDto(
                f.Id, "Fertilization", f.CropId, f.Crop.CropType, f.Crop.Plot.Name,
                f.AppliedAt, f.ProductName))
            .ToListAsync(cancellationToken);

        var labors = await context.LaborLogs
            .Where(l => l.Cost == null
                     && l.Crop.Plot.FarmId == request.FarmId
                     && l.Crop.Plot.Farm.TenantId == currentUser.TenantId)
            .Select(l => new PendingCostActivityDto(
                l.Id, "Labor", l.CropId, l.Crop.CropType, l.Crop.Plot.Name,
                l.PerformedAt, l.ActivityType))
            .ToListAsync(cancellationToken);

        return irrigations
            .Concat(fertilizations)
            .Concat(labors)
            .OrderByDescending(a => a.Date)
            .ToList();
    }
}
