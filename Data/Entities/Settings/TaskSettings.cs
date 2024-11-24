using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManager.Data.Entities.Settings
{
    public class TaskSettings : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.HasKey(task => task.Id).Metadata.IsPrimaryKey();
            builder.Property(task => task.Id).ValueGeneratedOnAdd();
            builder.Property(task => task.Title).IsRequired().HasMaxLength(150);
            builder.Property(task => task.Description).IsRequired().HasMaxLength(400);
            builder.Property(task => task.StartDate).IsRequired();
            builder.Property(task => task.EndDate).IsRequired();
            builder.Property(task => task.Status).IsRequired();
        }
    }
}
