using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Fertilization.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Fertilization.Queries;

public class GetFertilizationByIdQueryHandler : IRequestHandler<GetFertilizationByIdQuery, FertilizationDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetFertilizationByIdQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<FertilizationDto?> Handle(GetFertilizationByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.FertilizationLogs
            .Where(f => f.Id == request.Id
                     && f.CropId == request.CropId
                     && f.Crop.Plot.Farm.TenantId == _currentUser.TenantId)
            .Select(f => new FertilizationDto(
                f.Id, f.CropId, f.UserId, f.ProductName, f.ProductType,
                f.DoseKgHa, f.TotalKg, f.Method, f.Cost,
                f.AppliedAt, f.NextApplication, f.Notes, f.CreatedAt))
            .FirstOrDefaultAsync(cancellationToken);
    }
}