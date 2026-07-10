using AgroApp.Domain.Common;

namespace AgroApp.Domain.Entities;

public class SoilAnalysis : BaseLog
{
    public Guid PlotId { get; set; }
    public DateOnly AnalyzedAt { get; set; }
    public decimal? Ph { get; set; }
    public decimal? NitrogenPct { get; set; }
    public decimal? PhosphorusPct { get; set; }
    public decimal? PotassiumPct { get; set; }
    public decimal? OrganicMatterPct { get; set; }
    public string? Notes { get; set; }

    public Plot Plot { get; set; } = null!;
}
