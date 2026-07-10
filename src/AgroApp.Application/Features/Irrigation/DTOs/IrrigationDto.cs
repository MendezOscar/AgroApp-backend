namespace AgroApp.Application.Features.Irrigation.DTOs;

public record IrrigationDto(
    Guid Id,
    Guid CropId,
    Guid UserId,
    string Method,
    decimal? VolumeLiters,
    int? DurationMin,
    decimal? Cost,
    DateTime AppliedAt,
    string? Notes,
    DateTime CreatedAt
);

public record CreateIrrigationRequest(
    string Method,
    decimal? VolumeLiters,
    int? DurationMin,
    DateTime AppliedAt,
    string? Notes,
    decimal? Cost = null,
    Guid? TaskId = null,
    Guid? OccurrenceId = null
);

public record UpdateIrrigationRequest(
    string Method,
    decimal? VolumeLiters,
    int? DurationMin,
    DateTime AppliedAt,
    string? Notes,
    decimal? Cost = null
);