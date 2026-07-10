using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Labor.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Labor.Commands;

public class SetLaborCostCommandHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUser)
        : IRequestHandler<SetLaborCostCommand, LaborDto?>
{
    public async Task<LaborDto?> Handle(
        SetLaborCostCommand request,
        CancellationToken cancellationToken)
    {
        var labor = await context.LaborLogs
            .FirstOrDefaultAsync(l => l.Id == request.Id
                                   && l.CropId == request.CropId
                                   && l.Crop.Plot.Farm.TenantId == currentUser.TenantId,
                                   cancellationToken);

        if (labor is null) return null;

        labor.Cost = request.Cost;
        await context.SaveChangesAsync(cancellationToken);

        return new LaborDto(
            labor.Id, labor.CropId, labor.UserId, labor.ActivityType,
            labor.HoursWorked, labor.WorkersCount, labor.Cost,
            labor.PerformedAt, labor.Notes, labor.CreatedAt);
    }
}
