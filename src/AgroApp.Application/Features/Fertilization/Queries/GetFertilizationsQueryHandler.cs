using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Common.Models;
using AgroApp.Application.Features.Fertilization.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Fertilization.Queries;

public class GetFertilizationsQueryHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUser)
        : IRequestHandler<GetFertilizationsQuery, PagedResult<FertilizationDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly ICurrentUserService _currentUser = currentUser;

    public async Task<PagedResult<FertilizationDto>> Handle(
        GetFertilizationsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.FertilizationLogs
            .Where(f => f.CropId == request.CropId
                     && f.Crop.Plot.Farm.TenantId == _currentUser.TenantId);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(f => f.AppliedAt)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(f => new FertilizationDto(
                f.Id, f.CropId, f.UserId, f.ProductName, f.ProductType,
                f.DoseKgHa, f.TotalKg, f.Method, f.Cost,
                f.AppliedAt, f.NextApplication, f.Notes, f.CreatedAt))
            .ToListAsync(cancellationToken);

        return new PagedResult<FertilizationDto>(
            items,
            totalCount,
            request.Page,
            request.PageSize,
            request.Page * request.PageSize < totalCount);
    }
}