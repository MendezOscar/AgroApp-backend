using AgroApp.Application.Features.Crops.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Crops.Queries;

public record GetYieldHistoryQuery(Guid FarmId, int Months = 12) : IRequest<List<YieldHistoryDto>>;
