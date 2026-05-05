using AgroApp.Application.Features.Sensors.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Sensors.Queries;

public record GetSensorDevicesQuery(Guid PlotId) : IRequest<List<SensorDeviceDto>>;