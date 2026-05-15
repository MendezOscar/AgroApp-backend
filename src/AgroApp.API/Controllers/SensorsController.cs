using AgroApp.Application.Features.Sensors.Commands;
using AgroApp.Application.Features.Sensors.DTOs;
using AgroApp.Application.Features.Sensors.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AgroApp.API.Authorization;
using AgroApp.Application.Common.Constants;

namespace AgroApp.API.Controllers;

[ApiController]
[Authorize]
public class SensorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SensorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // Sensor Devices
    [HttpGet("api/plots/{plotId}/sensors")]
    [RequireRole(Roles.All)]
    public async Task<ActionResult<List<SensorDeviceDto>>> GetAll(Guid plotId)
    {
        var result = await _mediator.Send(new GetSensorDevicesQuery(plotId));
        return Ok(result);
    }

    [HttpPost("api/plots/{plotId}/sensors")]
    [RequireRole(Roles.Admin, Roles.Manager)]
    public async Task<ActionResult<SensorDeviceDto>> Create(Guid plotId, [FromBody] CreateSensorDeviceRequest request)
    {
        var command = new CreateSensorDeviceCommand(
            plotId, request.DeviceCode, request.DeviceType,
            request.Lat, request.Lng, request.FirmwareVer);

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    // Sensor Readings — No requiere auth (lo llama el ESP32 con API key en Fase 2)
    [HttpPost("api/sensors/{deviceId}/readings")]
    [AllowAnonymous] // ESP32 no tiene token
    public async Task<ActionResult<SensorReadingDto>> CreateReading(Guid deviceId, [FromBody] CreateSensorReadingRequest request)
    {
        var command = new CreateSensorReadingCommand(
            deviceId, request.Temperature, request.HumidityAir,
            request.HumiditySoil, request.Luminosity, request.RainMm,
            request.Ph, request.Ec);

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("api/sensors/{deviceId}/readings")]
    [RequireRole(Roles.All)]
    public async Task<ActionResult<List<SensorReadingDto>>> GetReadings(Guid deviceId, [FromQuery] int limit = 100)
    {
        var result = await _mediator.Send(new GetSensorReadingsQuery(deviceId, limit));
        return Ok(result);
    }

    [HttpGet("api/sensors/{deviceId}/readings/latest")]
    [RequireRole(Roles.All)]
    public async Task<ActionResult<SensorReadingDto>> GetLatestReading(Guid deviceId)
    {
        var result = await _mediator.Send(new GetLatestSensorReadingQuery(deviceId));
        return result is null ? NotFound() : Ok(result);
    }
}