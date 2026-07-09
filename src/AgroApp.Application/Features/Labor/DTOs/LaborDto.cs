namespace AgroApp.Application.Features.Labor.DTOs;

public record LaborDto(
    Guid Id,
    Guid CropId,
    Guid UserId,
    string ActivityType,
    decimal? HoursWorked,
    int WorkersCount,
    decimal? Cost,
    DateTime PerformedAt,
    string? Notes,
    DateTime CreatedAt
);

public record CreateLaborRequest(
    string ActivityType,
    decimal? HoursWorked,
    int WorkersCount,
    decimal? Cost,
    DateTime PerformedAt,
    string? Notes,
    Guid? TaskId = null
);

public record UpdateLaborRequest(
    string ActivityType,
    decimal? HoursWorked,
    int WorkersCount,
    decimal? Cost,
    DateTime PerformedAt,
    string? Notes
);