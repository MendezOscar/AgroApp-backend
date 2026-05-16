using AgroApp.Application.Features.Tasks.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Tasks.Queries;

public record GetTasksQuery(
    bool OnlyMine = false,
    string? Status = null
) : IRequest<List<TaskDto>>;