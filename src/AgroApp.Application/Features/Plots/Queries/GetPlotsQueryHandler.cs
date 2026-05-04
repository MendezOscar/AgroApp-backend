using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Plots.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Plots.Queries;

public class GetPlotsQueryHandler : IRequestHandler<GetPlotsQuery, List<PlotDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetPlotsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<List<PlotDto>> Handle(GetPlotsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Plots
            .Where(p => p.FarmId == request.FarmId
                     && p.Farm.TenantId == _currentUser.TenantId
                     && p.IsActive)
            .OrderBy(p => p.Name)
            .Select(p => new PlotDto(
                p.Id, p.FarmId, p.Name, p.SoilType,
                p.AreaHa, p.GeoJson, p.Notes,
                p.IsActive, p.CreatedAt))
            .ToListAsync(cancellationToken);
    }
}