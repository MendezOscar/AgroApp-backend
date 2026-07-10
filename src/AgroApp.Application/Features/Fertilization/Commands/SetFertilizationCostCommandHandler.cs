using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Fertilization.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Fertilization.Commands;

public class SetFertilizationCostCommandHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUser)
        : IRequestHandler<SetFertilizationCostCommand, FertilizationDto?>
{
    public async Task<FertilizationDto?> Handle(
        SetFertilizationCostCommand request,
        CancellationToken cancellationToken)
    {
        var fertilization = await context.FertilizationLogs
            .FirstOrDefaultAsync(f => f.Id == request.Id
                                   && f.CropId == request.CropId
                                   && f.Crop.Plot.Farm.TenantId == currentUser.TenantId,
                                   cancellationToken);

        if (fertilization is null) return null;

        fertilization.Cost = request.Cost;
        await context.SaveChangesAsync(cancellationToken);

        return new FertilizationDto(
            fertilization.Id, fertilization.CropId, fertilization.UserId,
            fertilization.ProductName, fertilization.ProductType,
            fertilization.DoseKgHa, fertilization.TotalKg, fertilization.Method,
            fertilization.Cost, fertilization.AppliedAt, fertilization.NextApplication,
            fertilization.Notes, fertilization.CreatedAt);
    }
}
