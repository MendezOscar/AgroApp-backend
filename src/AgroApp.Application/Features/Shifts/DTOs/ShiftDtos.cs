namespace AgroApp.Application.Features.Shifts.DTOs;

public record TaskTemplateDto(
    Guid Id,
    Guid CreatedBy,
    string CreatorName,
    Guid? PlotId,
    string? PlotName,
    Guid? CropId,
    string? CropName,
    string Title,
    string? Description,
    string TaskType,
    string Priority,
    string Shift,
    string RecurrenceType,
    string? WeekDays,
    DateOnly StartDate,
    DateOnly? EndDate,
    bool IsActive,
    int OccurrenceCount,
    DateTime CreatedAt
);

public record TaskOccurrenceDto(
    Guid Id,
    Guid TemplateId,
    string TemplateTitle,
    string TaskType,
    string Priority,
    Guid? AssignedTo,
    string? AssigneeName,
    string? PlotName,
    Guid? CropId,
    string? CropName,
    DateOnly ScheduledDate,
    string Shift,
    string Status,
    DateTime? CompletedAt,
    string? Notes
);

public record CreateTaskTemplateRequest(
    Guid? PlotId,
    Guid? CropId,
    string Title,
    string? Description,
    string TaskType,
    string Priority,
    string Shift,
    string RecurrenceType,
    string? WeekDays,
    DateOnly StartDate,
    DateOnly? EndDate
);

public record AssignOccurrenceRequest(
    Guid AssignedTo,
    string? Shift
);

public record UpdateOccurrenceStatusRequest(
    string Status,
    string? Notes
);