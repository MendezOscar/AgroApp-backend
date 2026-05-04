using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Plots.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Plots.Commands;

public class UpdatePlotCommandHandler : IRequestHandler<UpdatePlotCommand, PlotDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public UpdatePlotCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<PlotDto?> Handle(UpdatePlotCommand request, CancellationToken cancellationToken)
    {
        var plot = await _context.Plots
            .Include(p => p.Farm)
            .FirstOrDefaultAsync(p => p.Id == request.Id
                                   && p.FarmId == request.FarmId
                                   && p.Farm.TenantId == _currentUser.TenantId
                                   && p.IsActive, cancellationToken);

        if (plot is null) return null;

        plot.Name = request.Name;
        plot.SoilType = request.SoilType;
        plot.AreaHa = request.AreaHa;
        plot.GeoJson = request.GeoJson;
        plot.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return new PlotDto(
            plot.Id, plot.FarmId, plot.Name, plot.SoilType,
            plot.AreaHa, plot.GeoJson, plot.Notes,
            plot.IsActive, plot.CreatedAt);
    }
}