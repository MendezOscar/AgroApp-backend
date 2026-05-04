using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Farms.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Farms.Queries;

public class GetFarmByIdQueryHandler : IRequestHandler<GetFarmByIdQuery, FarmDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetFarmByIdQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<FarmDto?> Handle(GetFarmByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Farms
            .Where(f => f.Id == request.Id
                     && f.TenantId == _currentUser.TenantId
                     && f.IsActive)
            .Select(f => new FarmDto(
                f.Id, f.Name, f.Description,
                f.Lat, f.Lng, f.AreaHa,
                f.Country, f.Region,
                f.IsActive, f.CreatedAt))
            .FirstOrDefaultAsync(cancellationToken);
    }
}