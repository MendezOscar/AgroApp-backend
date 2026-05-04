using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Plots.DTOs;
using AgroApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Plots.Commands;

public class CreatePlotCommandHandler : IRequestHandler<CreatePlotCommand, PlotDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public CreatePlotCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<PlotDto> Handle(CreatePlotCommand request, CancellationToken cancellationToken)
    {
        // Verificar que la finca pertenece al tenant
        var farmExists = await _context.Farms
            .AnyAsync(f => f.Id == request.FarmId
                        && f.TenantId == _currentUser.TenantId
                        && f.IsActive, cancellationToken);

        if (!farmExists)
            throw new InvalidOperationException("Finca no encontrada.");

        var plot = new Plot
        {
            FarmId = request.FarmId,
            Name = request.Name,
            SoilType = request.SoilType,
            AreaHa = request.AreaHa,
            GeoJson = request.GeoJson,
            Notes = request.Notes
        };

        _context.Plots.Add(plot);
        await _context.SaveChangesAsync(cancellationToken);

        return new PlotDto(
            plot.Id, plot.FarmId, plot.Name, plot.SoilType,
            plot.AreaHa, plot.GeoJson, plot.Notes,
            plot.IsActive, plot.CreatedAt);
    }
}