using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Irrigation.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Irrigation.Commands;

public class SetIrrigationCostCommandHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUser)
        : IRequestHandler<SetIrrigationCostCommand, IrrigationDto?>
{
    public async Task<IrrigationDto?> Handle(
        SetIrrigationCostCommand request,
        CancellationToken cancellationToken)
    {
        var irrigation = await context.IrrigationLogs
            .FirstOrDefaultAsync(i => i.Id == request.Id
                                   && i.CropId == request.CropId
                                   && i.Crop.Plot.Farm.TenantId == currentUser.TenantId,
                                   cancellationToken);

        if (irrigation is null) return null;

        irrigation.Cost = request.Cost;
        await context.SaveChangesAsync(cancellationToken);

        return new IrrigationDto(
            irrigation.Id, irrigation.CropId, irrigation.UserId,
            irrigation.Method, irrigation.VolumeLiters, irrigation.DurationMin,
            irrigation.Cost, irrigation.AppliedAt, irrigation.Notes, irrigation.CreatedAt);
    }
}
