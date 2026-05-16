using AgroApp.Application.Features.Shifts.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Shifts.Commands;

public record UpdateOccurrenceStatusCommand(
    Guid OccurrenceId,
    string Status,
    string? Notes
) : IRequest<TaskOccurrenceDto?>;