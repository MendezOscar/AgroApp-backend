using AgroApp.Application.Features.Sensors.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Sensors.Commands;

public record CreateSensorDeviceCommand(
    Guid PlotId,
    string DeviceCode,
    string DeviceType,
    double? Lat,
    double? Lng,
    string? FirmwareVer
) : IRequest<SensorDeviceDto>;