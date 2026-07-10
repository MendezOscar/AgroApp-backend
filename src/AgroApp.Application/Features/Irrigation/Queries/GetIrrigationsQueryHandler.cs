using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Common.Models;
using AgroApp.Application.Features.Irrigation.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Irrigation.Queries;

public class GetIrrigationsQueryHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUser)
        : IRequestHandler<GetIrrigationsQuery, PagedResult<IrrigationDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly ICurrentUserService _currentUser = currentUser;

    public async Task<PagedResult<IrrigationDto>> Handle(
        GetIrrigationsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.IrrigationLogs
            .Where(i => i.CropId == request.CropId
                     && i.Crop.Plot.Farm.TenantId == _currentUser.TenantId);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(i => i.AppliedAt)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(i => new IrrigationDto(
                i.Id, i.CropId, i.UserId, i.Method,
                i.VolumeLiters, i.DurationMin, i.Cost,
                i.AppliedAt, i.Notes, i.CreatedAt))
            .ToListAsync(cancellationToken);

        return new PagedResult<IrrigationDto>(
            items,
            totalCount,
            request.Page,
            request.PageSize,
            request.Page * request.PageSize < totalCount);
    }
}