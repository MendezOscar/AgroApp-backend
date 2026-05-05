using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Sensors.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Sensors.Queries;

public class GetSensorDevicesQueryHandler : IRequestHandler<GetSensorDevicesQuery, List<SensorDeviceDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetSensorDevicesQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<List<SensorDeviceDto>> Handle(GetSensorDevicesQuery request, CancellationToken cancellationToken)
    {
        return await _context.SensorDevices
            .Where(s => s.PlotId == request.PlotId
                     && s.Plot.Farm.TenantId == _currentUser.TenantId)
            .OrderBy(s => s.DeviceCode)
            .Select(s => new SensorDeviceDto(
                s.Id, s.PlotId, s.DeviceCode, s.DeviceType,
                s.Lat, s.Lng, s.BatteryPct, s.FirmwareVer,
                s.IsActive, s.LastSeenAt, s.CreatedAt))
            .ToListAsync(cancellationToken);
    }
}