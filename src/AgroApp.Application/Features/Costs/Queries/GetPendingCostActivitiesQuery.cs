using AgroApp.Application.Features.Costs.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Costs.Queries;

public record GetPendingCostActivitiesQuery(Guid FarmId) : IRequest<List<PendingCostActivityDto>>;
