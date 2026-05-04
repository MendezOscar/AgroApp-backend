using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Fertilization.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Fertilization.Queries;

public class GetFertilizationsQueryHandler : IRequestHandler<GetFertilizationsQuery, List<FertilizationDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetFertilizationsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<List<FertilizationDto>> Handle(GetFertilizationsQuery request, CancellationToken cancellationToken)
    {
        return await _context.FertilizationLogs
            .Where(f => f.CropId == request.CropId
                     && f.Crop.Plot.Farm.TenantId == _currentUser.TenantId)
            .OrderByDescending(f => f.AppliedAt)
            .Select(f => new FertilizationDto(
                f.Id, f.CropId, f.UserId, f.ProductName, f.ProductType,
                f.DoseKgHa, f.TotalKg, f.Method, f.Cost,
                f.AppliedAt, f.NextApplication, f.Notes, f.CreatedAt))
            .ToListAsync(cancellationToken);
    }
}