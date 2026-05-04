using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Farms.DTOs;
using AgroApp.Domain.Entities;
using MediatR;

namespace AgroApp.Application.Features.Farms.Commands;

public class CreateFarmCommandHandler : IRequestHandler<CreateFarmCommand, FarmDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public CreateFarmCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<FarmDto> Handle(CreateFarmCommand request, CancellationToken cancellationToken)
    {
        var farm = new Farm
        {
            TenantId = _currentUser.TenantId,
            OwnerId = _currentUser.UserId,
            Name = request.Name,
            Description = request.Description,
            Lat = request.Lat,
            Lng = request.Lng,
            AreaHa = request.AreaHa,
            Country = request.Country,
            Region = request.Region
        };

        _context.Farms.Add(farm);
        await _context.SaveChangesAsync(cancellationToken);

        return new FarmDto(
            farm.Id, farm.Name, farm.Description,
            farm.Lat, farm.Lng, farm.AreaHa,
            farm.Country, farm.Region,
            farm.IsActive, farm.CreatedAt);
    }
}