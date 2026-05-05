using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Sensors.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Sensors.Queries;

public class GetSensorReadingsQueryHandler : IRequestHandler<GetSensorReadingsQuery, List<SensorReadingDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetSensorReadingsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<List<SensorReadingDto>> Handle(GetSensorReadingsQuery request, CancellationToken cancellationToken)
    {
        return await _context.SensorReadings
            .Where(r => r.DeviceId == request.DeviceId
                     && r.Device.Plot.Farm.TenantId == _currentUser.TenantId)
            .OrderByDescending(r => r.RecordedAt)
            .Take(request.Limit)
            .Select(r => new SensorReadingDto(
                r.Id, r.DeviceId, r.Temperature, r.HumidityAir,
                r.HumiditySoil, r.Luminosity, r.RainMm,
                r.Ph, r.Ec, r.RecordedAt))
            .ToListAsync(cancellationToken);
    }
}