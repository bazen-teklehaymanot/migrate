using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Move.PostgreSQL
{
    public partial class NewDBContext : DbContext
    {
        private readonly string _datasource;

        public NewDBContext()
        {
        }
        public NewDBContext(string dataSource)
        {
            _datasource = dataSource;
        }
        public NewDBContext(DbContextOptions<NewDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ProjectTaskResource> ProjectTaskResources { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //var connectionString = $"Data Source={_datasource}";
                optionsBuilder.UseNpgsql(_datasource);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectTaskResource>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("project_task_resource");

                entity.Property(e => e.TaskResourceHour).HasColumnName("task_resource_hour");

                entity.Property(e => e.TaskResourceLevel).HasColumnName("task_resource_level");

                entity.Property(e => e.TaskResourceResourceId).HasColumnName("task_resource_resource_id");

                entity.Property(e => e.TaskResourceTaskId).HasColumnName("task_resource_task_id");

                entity.HasOne(d => d.TaskResourceTask)
                    .WithMany()
                    .HasForeignKey(d => d.TaskResourceTaskId);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("task");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ParentId).HasColumnName("parentId");

                entity.Property(e => e.TaskEndDate).HasColumnName("task_end_date");

                entity.Property(e => e.TaskHasWeekendHours).HasColumnName("task_has_weekend_hours");

                entity.Property(e => e.TaskHourBudget).HasColumnName("task_hour_budget");

                entity.Property(e => e.TaskHourForecast).HasColumnName("task_hour_forecast");

                entity.Property(e => e.TaskIsMilestone).HasColumnName("task_is_milestone");

                entity.Property(e => e.TaskName).HasColumnName("task_name");

                entity.Property(e => e.TaskProgress).HasColumnName("task_progress");

                entity.Property(e => e.TaskProjectElementId).HasColumnName("task_project_element_id");

                entity.Property(e => e.TaskProjectId).HasColumnName("task_project_id");

                entity.Property(e => e.TaskStartDate).HasColumnName("task_start_date");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
