using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class WorkItemConfiguration : IEntityTypeConfiguration<WorkItem>
{
    public void Configure(EntityTypeBuilder<WorkItem> builder)
    {
        builder.Property(t => t.Title)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasMaxLength(8000);

        builder.OwnsOne(w => w.Priority, priority =>
        {
            priority.Property(p => p.Level)
                .HasColumnName("PriorityLevel")
                .IsRequired();

            priority.Property(p => p.Name)
                .HasColumnName("PriorityName")
                .HasMaxLength(50)
                .IsRequired();
        });

        builder.OwnsOne(w => w.Stage, stage =>
        {
            stage.Property(s => s.Id)
                .HasColumnName("StageId")
                .IsRequired();

            stage.Property(s => s.Name)
                .HasColumnName("StageName")
                .HasMaxLength(50)
                .IsRequired(); 
        });
    }
}
