using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Labor.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Labor.Queries;

public class GetLaborsQueryHandler : IRequestHandler<GetLaborsQuery, List<LaborDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetLaborsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<List<LaborDto>> Handle(GetLaborsQuery request, CancellationToken cancellationToken)
    {
        return await _context.LaborLogs
            .Where(l => l.CropId == request.CropId
                     && l.Crop.Plot.Farm.TenantId == _currentUser.TenantId)
            .OrderByDescending(l => l.PerformedAt)
            .Select(l => new LaborDto(
                l.Id, l.CropId, l.UserId, l.ActivityType,
                l.HoursWorked, l.WorkersCount, l.Cost,
                l.PerformedAt, l.Notes, l.CreatedAt))
            .ToListAsync(cancellationToken);
    }
}