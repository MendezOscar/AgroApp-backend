using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Common.Models;
using AgroApp.Application.Features.Labor.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Labor.Queries;

public class GetLaborsQueryHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUser)
        : IRequestHandler<GetLaborsQuery, PagedResult<LaborDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly ICurrentUserService _currentUser = currentUser;

    public async Task<PagedResult<LaborDto>> Handle(
        GetLaborsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.LaborLogs
            .Where(l => l.CropId == request.CropId
                     && l.Crop.Plot.Farm.TenantId == _currentUser.TenantId);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(l => l.PerformedAt)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(l => new LaborDto(
                l.Id, l.CropId, l.UserId, l.ActivityType,
                l.HoursWorked, l.WorkersCount, l.Cost,
                l.PerformedAt, l.Notes, l.CreatedAt))
            .ToListAsync(cancellationToken);

        return new PagedResult<LaborDto>(
            items,
            totalCount,
            request.Page,
            request.PageSize,
            request.Page * request.PageSize < totalCount);
    }
}