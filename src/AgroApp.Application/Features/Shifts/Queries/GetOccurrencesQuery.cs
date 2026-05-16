using AgroApp.Application.Features.Shifts.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Shifts.Queries;

public record GetOccurrencesQuery(
    DateOnly? Date = null,
    bool OnlyMine = false
) : IRequest<List<TaskOccurrenceDto>>;