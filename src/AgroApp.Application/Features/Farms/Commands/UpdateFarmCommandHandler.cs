using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Farms.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Farms.Commands;

public class UpdateFarmCommandHandler : IRequestHandler<UpdateFarmCommand, FarmDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public UpdateFarmCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<FarmDto?> Handle(UpdateFarmCommand request, CancellationToken cancellationToken)
    {
        var farm = await _context.Farms
            .FirstOrDefaultAsync(f => f.Id == request.Id
                                   && f.TenantId == _currentUser.TenantId
                                   && f.IsActive, cancellationToken);

        if (farm is null) return null;

        farm.Name = request.Name;
        farm.Description = request.Description;
        farm.Lat = request.Lat;
        farm.Lng = request.Lng;
        farm.AreaHa = request.AreaHa;
        farm.Country = request.Country;
        farm.Region = request.Region;

        await _context.SaveChangesAsync(cancellationToken);

        return new FarmDto(
            farm.Id, farm.Name, farm.Description,
            farm.Lat, farm.Lng, farm.AreaHa,
            farm.Country, farm.Region,
            farm.IsActive, farm.CreatedAt);
    }
}