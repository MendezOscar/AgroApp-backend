using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Plots.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Plots.Queries;

public class GetPlotByIdQueryHandler : IRequestHandler<GetPlotByIdQuery, PlotDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetPlotByIdQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<PlotDto?> Handle(GetPlotByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Plots
            .Where(p => p.Id == request.Id
                     && p.FarmId == request.FarmId
                     && p.Farm.TenantId == _currentUser.TenantId
                     && p.IsActive)
            .Select(p => new PlotDto(
                p.Id, p.FarmId, p.Name, p.SoilType,
                p.AreaHa, p.GeoJson, p.Notes,
                p.IsActive, p.CreatedAt))
            .FirstOrDefaultAsync(cancellationToken);
    }
}