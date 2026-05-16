using AgroApp.Application.Features.Shifts.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Shifts.Commands;

public record CreateTaskTemplateCommand(
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
) : IRequest<TaskTemplateDto>;