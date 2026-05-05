namespace AgroApp.Application.Features.Sensors.DTOs;

public record SensorDeviceDto(
    Guid Id,
    Guid PlotId,
    string DeviceCode,
    string DeviceType,
    double? Lat,
    double? Lng,
    int? BatteryPct,
    string? FirmwareVer,
    bool IsActive,
    DateTime? LastSeenAt,
    DateTime CreatedAt
);

public record SensorReadingDto(
    Guid Id,
    Guid DeviceId,
    decimal? Temperature,
    decimal? HumidityAir,
    decimal? HumiditySoil,
    decimal? Luminosity,
    decimal? RainMm,
    decimal? Ph,
    decimal? Ec,
    DateTime RecordedAt
);

public record CreateSensorDeviceRequest(
    string DeviceCode,
    string DeviceType,
    double? Lat,
    double? Lng,
    string? FirmwareVer
);

public record UpdateSensorDeviceRequest(
    string DeviceCode,
    string DeviceType,
    double? Lat,
    double? Lng,
    int? BatteryPct,
    string? FirmwareVer
);

public record CreateSensorReadingRequest(
    decimal? Temperature,
    decimal? HumidityAir,
    decimal? HumiditySoil,
    decimal? Luminosity,
    decimal? RainMm,
    decimal? Ph,
    decimal? Ec
);