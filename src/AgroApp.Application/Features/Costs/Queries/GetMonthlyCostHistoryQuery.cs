using AgroApp.Application.Features.Costs.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Costs.Queries;

public record GetMonthlyCostHistoryQuery(int Months = 6) : IRequest<List<MonthlyCostDto>>;
