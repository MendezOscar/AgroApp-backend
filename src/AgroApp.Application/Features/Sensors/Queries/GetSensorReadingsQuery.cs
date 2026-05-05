using AgroApp.Application.Features.Sensors.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Sensors.Queries;

public record GetSensorReadingsQuery(Guid DeviceId, int Limit = 100) : IRequest<List<SensorReadingDto>>;