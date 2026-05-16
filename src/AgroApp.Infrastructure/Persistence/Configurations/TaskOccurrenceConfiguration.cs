using AgroApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroApp.Infrastructure.Persistence.Configurations;

public class TaskOccurrenceConfiguration : IEntityTypeConfiguration<TaskOccurrence>
{
    public void Configure(EntityTypeBuilder<TaskOccurrence> builder)
    {
        builder.ToTable("task_occurrences");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Shift).HasConversion<string>().HasMaxLength(20);
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
        builder.Property(x => x.ScheduledDate).HasColumnName("scheduled_date");
        builder.Property(x => x.CompletedAt).HasColumnName("completed_at");
        builder.Property(x => x.TemplateId).HasColumnName("template_id");
        builder.Property(x => x.TenantId).HasColumnName("tenant_id");
        builder.Property(x => x.AssignedTo).HasColumnName("assigned_to");
        builder.Property(x => x.CreatedAt).HasColumnName("created_at");
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");

        builder.HasOne(x => x.Template).WithMany(x => x.Occurrences)
            .HasForeignKey(x => x.TemplateId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Tenant).WithMany()
            .HasForeignKey(x => x.TenantId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Assignee).WithMany()
            .HasForeignKey(x => x.AssignedTo).OnDelete(DeleteBehavior.SetNull);
    }
}