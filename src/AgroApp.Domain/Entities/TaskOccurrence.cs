using AgroApp.Domain.Enums;

namespace AgroApp.Domain.Entities;

public class TaskOccurrence
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TemplateId { get; set; }
    public Guid TenantId { get; set; }
    public Guid? AssignedTo { get; set; }
    public DateOnly ScheduledDate { get; set; }
    public ShiftType Shift { get; set; } = ShiftType.Day;
    public Enums.TaskStatus Status { get; set; } = Enums.TaskStatus.Pending;
    public DateTime? CompletedAt { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navegación
    public TaskTemplate Template { get; set; } = null!;
    public Tenant Tenant { get; set; } = null!;
    public User? Assignee { get; set; }
}