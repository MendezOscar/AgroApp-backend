using AgroApp.Application.Features.Tasks.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Tasks.Commands;

public record CreateTaskCommand(
    Guid AssignedTo,
    Guid? PlotId,
    Guid? CropId,
    string Title,
    string? Description,
    string Priority,
    string TaskType,
    DateOnly DueDate
) : IRequest<TaskDto>;