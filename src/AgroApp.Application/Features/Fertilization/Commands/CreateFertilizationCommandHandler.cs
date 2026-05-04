using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Fertilization.DTOs;
using AgroApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Fertilization.Commands;

public class CreateFertilizationCommandHandler : IRequestHandler<CreateFertilizationCommand, FertilizationDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public CreateFertilizationCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<FertilizationDto> Handle(CreateFertilizationCommand request, CancellationToken cancellationToken)
    {
        var crop = await _context.Crops
            .Include(c => c.Plot.Farm)
            .FirstOrDefaultAsync(c => c.Id == request.CropId
                                   && c.Plot.Farm.TenantId == _currentUser.TenantId,
                                   cancellationToken);

        if (crop is null)
            throw new InvalidOperationException("Cultivo no encontrado.");

        var log = new FertilizationLog
        {
            CropId = request.CropId,
            UserId = _currentUser.UserId,
            ProductName = request.ProductName,
            ProductType = request.ProductType,
            DoseKgHa = request.DoseKgHa,
            TotalKg = request.TotalKg,
            Method = request.Method,
            Cost = request.Cost,
            AppliedAt = request.AppliedAt,
            NextApplication = request.NextApplication,
            Notes = request.Notes
        };

        _context.FertilizationLogs.Add(log);
        await _context.SaveChangesAsync(cancellationToken);

        return new FertilizationDto(
            log.Id, log.CropId, log.UserId, log.ProductName, log.ProductType,
            log.DoseKgHa, log.TotalKg, log.Method, log.Cost,
            log.AppliedAt, log.NextApplication, log.Notes, log.CreatedAt);
    }
}