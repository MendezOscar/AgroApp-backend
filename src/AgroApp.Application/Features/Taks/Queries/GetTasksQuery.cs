using AgroApp.Application.Common.Models;
using AgroApp.Application.Features.Tasks.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Tasks.Queries;

public record GetTasksQuery(
    bool OnlyMine = false,
    string? Status = null,
    int Page = 1,
    int PageSize = 20
) : IRequest<PagedResult<TaskDto>>;