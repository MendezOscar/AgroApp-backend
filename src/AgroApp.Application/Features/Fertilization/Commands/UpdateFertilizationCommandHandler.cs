using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Fertilization.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Fertilization.Commands;

public class UpdateFertilizationCommandHandler : IRequestHandler<UpdateFertilizationCommand, FertilizationDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public UpdateFertilizationCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<FertilizationDto?> Handle(UpdateFertilizationCommand request, CancellationToken cancellationToken)
    {
        var log = await _context.FertilizationLogs
            .Include(f => f.Crop.Plot.Farm)
            .FirstOrDefaultAsync(f => f.Id == request.Id
                                   && f.CropId == request.CropId
                                   && f.Crop.Plot.Farm.TenantId == _currentUser.TenantId,
                                   cancellationToken);

        if (log is null) return null;

        log.ProductName = request.ProductName;
        log.ProductType = request.ProductType;
        log.DoseKgHa = request.DoseKgHa;
        log.TotalKg = request.TotalKg;
        log.Method = request.Method;
        log.Cost = request.Cost;
        log.AppliedAt = request.AppliedAt;
        log.NextApplication = request.NextApplication;
        log.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return new FertilizationDto(
            log.Id, log.CropId, log.UserId, log.ProductName, log.ProductType,
            log.DoseKgHa, log.TotalKg, log.Method, log.Cost,
            log.AppliedAt, log.NextApplication, log.Notes, log.CreatedAt);
    }
}