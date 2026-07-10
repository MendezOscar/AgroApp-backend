using AgroApp.Application.Features.SoilAnalyses.DTOs;
using MediatR;

namespace AgroApp.Application.Features.SoilAnalyses.Commands;

public record CreateSoilAnalysisCommand(
    Guid PlotId,
    DateOnly AnalyzedAt,
    decimal? Ph,
    decimal? NitrogenPct,
    decimal? PhosphorusPct,
    decimal? PotassiumPct,
    decimal? OrganicMatterPct,
    string? Notes
) : IRequest<SoilAnalysisDto>;
