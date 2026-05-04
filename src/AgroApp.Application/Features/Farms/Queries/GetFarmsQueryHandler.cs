using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Farms.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Farms.Queries;

public class GetFarmsQueryHandler : IRequestHandler<GetFarmsQuery, List<FarmDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetFarmsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<List<FarmDto>> Handle(GetFarmsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Farms
            .Where(f => f.TenantId == _currentUser.TenantId && f.IsActive)
            .OrderBy(f => f.Name)
            .Select(f => new FarmDto(
                f.Id, f.Name, f.Description,
                f.Lat, f.Lng, f.AreaHa,
                f.Country, f.Region,
                f.IsActive, f.CreatedAt))
            .ToListAsync(cancellationToken);
    }
}