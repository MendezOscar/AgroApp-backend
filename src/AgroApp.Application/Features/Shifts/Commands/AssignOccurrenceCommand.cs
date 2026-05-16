using AgroApp.Application.Features.Shifts.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Shifts.Commands;

public record AssignOccurrenceCommand(
    Guid OccurrenceId,
    Guid AssignedTo,
    string? Shift
) : IRequest<TaskOccurrenceDto?>;