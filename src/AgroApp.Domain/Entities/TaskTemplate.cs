using AgroApp.Domain.Enums;

namespace AgroApp.Domain.Entities;

public class TaskTemplate
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TenantId { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? PlotId { get; set; }
    public Guid? CropId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TaskType TaskType { get; set; } = TaskType.Other;
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public ShiftType Shift { get; set; } = ShiftType.Day;
    public RecurrenceType RecurrenceType { get; set; } = RecurrenceType.Once;
    public string? WeekDays { get; set; } // "1,3,5"
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? RequiredPhenologyStage { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navegación
    public Tenant Tenant { get; set; } = null!;
    public User Creator { get; set; } = null!;
    public Plot? Plot { get; set; }
    public Crop? Crop { get; set; }
    public ICollection<TaskOccurrence> Occurrences { get; set; } =
        new List<TaskOccurrence>();
}