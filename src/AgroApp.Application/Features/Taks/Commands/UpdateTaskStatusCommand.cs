using AgroApp.Application.Features.Tasks.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Tasks.Commands;

public record UpdateTaskStatusCommand(
    Guid Id,
    string Status,
    string? Notes
) : IRequest<TaskDto?>;