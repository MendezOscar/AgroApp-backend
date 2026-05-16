using AgroApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroApp.Infrastructure.Persistence.Configurations;

public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable("tasks");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Priority)
            .HasConversion<string>().HasMaxLength(20);
        builder.Property(x => x.Status)
            .HasConversion<string>().HasMaxLength(20);
        builder.Property(x => x.CreatedAt).HasColumnName("created_at");
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");
        builder.Property(x => x.CompletedAt).HasColumnName("completed_at");
        builder.Property(x => x.DueDate).HasColumnName("due_date");
        builder.Property(x => x.CreatedBy).HasColumnName("created_by");
        builder.Property(x => x.AssignedTo).HasColumnName("assigned_to");
        builder.Property(x => x.TenantId).HasColumnName("tenant_id");
        builder.Property(x => x.PlotId).HasColumnName("plot_id");
        builder.Property(x => x.CropId).HasColumnName("crop_id");
        builder.Property(x => x.TaskType).HasConversion<string>().HasMaxLength(50).HasColumnName("task_type");

        builder.HasOne(x => x.Tenant).WithMany()
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Creator).WithMany()
            .HasForeignKey(x => x.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Assignee).WithMany()
            .HasForeignKey(x => x.AssignedTo)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Plot).WithMany()
            .HasForeignKey(x => x.PlotId)
            .OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(x => x.Crop).WithMany()
            .HasForeignKey(x => x.CropId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}