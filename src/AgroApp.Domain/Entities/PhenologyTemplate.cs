namespace AgroApp.Domain.Entities;

public class PhenologyTemplate
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string CropType { get; set; } = string.Empty;
    public string StageName { get; set; } = string.Empty;
    public int StageOrder { get; set; }
    public string? Description { get; set; }
    public int MinDays { get; set; }
    public int MaxDays { get; set; }
    public string? Icon { get; set; }
    public string? Recommendations { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}