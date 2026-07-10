using AgroApp.Application.Features.SoilAnalyses.DTOs;
using MediatR;

namespace AgroApp.Application.Features.SoilAnalyses.Queries;

public record GetSoilAnalysesQuery(Guid PlotId) : IRequest<List<SoilAnalysisDto>>;
