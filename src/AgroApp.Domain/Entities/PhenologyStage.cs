using AgroApp.Domain.Common;

namespace AgroApp.Domain.Entities;

public class PhenologyStage : BaseLog
{
    public Guid CropId { get; set; }
    public Guid TenantId { get; set; }
    public Guid? TemplateId { get; set; }
    public string StageName { get; set; } = string.Empty;
    public int StageOrder { get; set; }
    public DateOnly StartedAt { get; set; }
    public DateOnly? EndedAt { get; set; }
    public string? Observations { get; set; }
    public bool IsCustom { get; set; } = false;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navegación
    public Crop Crop { get; set; } = null!;
    public Tenant Tenant { get; set; } = null!;
    public PhenologyTemplate? Template { get; set; }
}