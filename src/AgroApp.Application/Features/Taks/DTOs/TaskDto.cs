namespace AgroApp.Application.Features.Tasks.DTOs;

public record TaskDto(
    Guid Id,
    Guid CreatedBy,
    Guid AssignedTo,
    string AssigneeName,
    string CreatorName,
    Guid? PlotId,
    string? PlotName,
    Guid? CropId,
    string? CropName,
    string Title,
    string? Description,
    string Priority,
    string Status,
    string TaskType,      // ← nuevo
    DateOnly DueDate,
    DateTime? CompletedAt,
    string? Notes,
    DateTime CreatedAt
);

public record CreateTaskRequest(
    Guid AssignedTo,
    Guid? PlotId,
    Guid? CropId,
    string Title,
    string? Description,
    string Priority,
    string TaskType,      // ← nuevo
    DateOnly DueDate
);

public record UpdateTaskStatusRequest(
    string Status,
    string? Notes
);