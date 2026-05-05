using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Sensors.DTOs;
using AgroApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Sensors.Commands;

public class CreateSensorDeviceCommandHandler : IRequestHandler<CreateSensorDeviceCommand, SensorDeviceDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public CreateSensorDeviceCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<SensorDeviceDto> Handle(CreateSensorDeviceCommand request, CancellationToken cancellationToken)
    {
        var plotExists = await _context.Plots
            .Include(p => p.Farm)
            .AnyAsync(p => p.Id == request.PlotId
                        && p.Farm.TenantId == _currentUser.TenantId
                        && p.IsActive, cancellationToken);

        if (!plotExists)
            throw new InvalidOperationException("Parcela no encontrada.");

        var codeExists = await _context.SensorDevices
            .AnyAsync(s => s.DeviceCode == request.DeviceCode, cancellationToken);

        if (codeExists)
            throw new InvalidOperationException($"El código de dispositivo '{request.DeviceCode}' ya está registrado.");

        var device = new SensorDevice
        {
            PlotId = request.PlotId,
            DeviceCode = request.DeviceCode,
            DeviceType = request.DeviceType,
            Lat = request.Lat,
            Lng = request.Lng,
            FirmwareVer = request.FirmwareVer
        };

        _context.SensorDevices.Add(device);
        await _context.SaveChangesAsync(cancellationToken);

        return new SensorDeviceDto(
            device.Id, device.PlotId, device.DeviceCode, device.DeviceType,
            device.Lat, device.Lng, device.BatteryPct, device.FirmwareVer,
            device.IsActive, device.LastSeenAt, device.CreatedAt);
    }
}