using AgroApp.Application.Features.Sensors.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Sensors.Commands;

public record CreateSensorReadingCommand(
    Guid DeviceId,
    decimal? Temperature,
    decimal? HumidityAir,
    decimal? HumiditySoil,
    decimal? Luminosity,
    decimal? RainMm,
    decimal? Ph,
    decimal? Ec
) : IRequest<SensorReadingDto>;