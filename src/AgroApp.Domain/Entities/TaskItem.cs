using AgroApp.Domain.Enums;


namespace AgroApp.Domain.Entities;

public class TaskItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TenantId { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid AssignedTo { get; set; }
    public Guid? PlotId { get; set; }
    public Guid? CropId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public Enums.TaskStatus Status { get; set; } = Enums.TaskStatus.Pending;
    public TaskType TaskType { get; set; } = TaskType.Other;
    public DateOnly DueDate { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public Tenant Tenant { get; set; } = null!;
    public User Creator { get; set; } = null!;
    public User Assignee { get; set; } = null!;
    public Plot? Plot { get; set; }
    public Crop? Crop { get; set; }
}