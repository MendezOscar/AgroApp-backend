using AgroApp.Application.Features.Farms.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Farms.Queries;

public record GetFarmSummaryQuery(Guid FarmId) : IRequest<FarmSummaryDto?>;
