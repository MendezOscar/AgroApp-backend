namespace AgroApp.Application.Features.SoilAnalyses.DTOs;

public record SoilAnalysisDto(
    Guid Id,
    Guid PlotId,
    DateOnly AnalyzedAt,
    decimal? Ph,
    decimal? NitrogenPct,
    decimal? PhosphorusPct,
    decimal? PotassiumPct,
    decimal? OrganicMatterPct,
    string? Notes,
    DateTime CreatedAt
);

public record CreateSoilAnalysisRequest(
    DateOnly AnalyzedAt,
    decimal? Ph,
    decimal? NitrogenPct,
    decimal? PhosphorusPct,
    decimal? PotassiumPct,
    decimal? OrganicMatterPct,
    string? Notes
);
