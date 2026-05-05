namespace AgroApp.Domain.Entities;

public class AlertRule
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TenantId { get; set; }
    public Guid? PlotId { get; set; }
    public string Metric { get; set; } = string.Empty;
    public string Operator { get; set; } = string.Empty;
    public decimal Threshold { get; set; }
    public string Severity { get; set; } = "warning";
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Tenant Tenant { get; set; } = null!;
    public Plot? Plot { get; set; }
}