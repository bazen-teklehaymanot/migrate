using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Move.SQLite
{
    public partial class OldDBContext : DbContext
    {
        private readonly string _datasource;
        public OldDBContext()
        {
        }

        public OldDBContext(DbContextOptions<OldDBContext> options)
            : base(options)
        {
        }
        public OldDBContext(string dataSource)
        {
            _datasource = dataSource;
        }

        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskDate> TaskDates { get; set; }
        public virtual DbSet<TaskDependecyAssociation> TaskDependecyAssociations { get; set; }
        public virtual DbSet<TaskHour> TaskHours { get; set; }
        public virtual DbSet<TaskResource> TaskResources { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = $"Data Source={_datasource}";
                optionsBuilder.UseSqlite(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("task");

                entity.Property(e => e.TaskId)
                    .ValueGeneratedNever()
                    .HasColumnName("task_id");

                entity.Property(e => e.TaskDescription).HasColumnName("task_description");

                entity.Property(e => e.TaskHasWeekendHours).HasColumnName("task_has_weekend_hours");

                entity.Property(e => e.TaskIsMilestone).HasColumnName("task_is_milestone");

                entity.Property(e => e.TaskName).HasColumnName("task_name");

                entity.Property(e => e.TaskProgress).HasColumnName("task_progress");

                entity.Property(e => e.TaskProjectElementId).HasColumnName("task_project_element_id");

                entity.Property(e => e.TaskProjectId).HasColumnName("task_project_id");
            });

            modelBuilder.Entity<TaskDate>(entity =>
            {
                entity.ToTable("task_date");

                entity.Property(e => e.TaskDateId)
                    .ValueGeneratedNever()
                    .HasColumnName("task_date_id");

                entity.Property(e => e.TaskDate1).HasColumnName("task_date");

                entity.Property(e => e.TaskDateTypeId).HasColumnName("task_date_type_id");

                entity.Property(e => e.TaskId).HasColumnName("task_id");

                entity.Property(e => e.TimeCreated).HasColumnName("time_created");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TaskDates)
                    .HasForeignKey(d => d.TaskId);
            });

            modelBuilder.Entity<TaskDependecyAssociation>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("task_dependecy_association");

                entity.Property(e => e.LeftId).HasColumnName("left_id");

                entity.Property(e => e.RightId).HasColumnName("right_id");

                entity.HasOne(d => d.Left)
                    .WithMany()
                    .HasForeignKey(d => d.LeftId);

                entity.HasOne(d => d.Right)
                    .WithMany()
                    .HasForeignKey(d => d.RightId);
            });

            modelBuilder.Entity<TaskHour>(entity =>
            {
                entity.ToTable("task_hours");

                entity.Property(e => e.TaskHourId)
                    .ValueGeneratedNever()
                    .HasColumnName("task_hour_id");

                entity.Property(e => e.TaskHourTypeId).HasColumnName("task_hour_type_id");

                entity.Property(e => e.TaskHours).HasColumnName("task_hours");

                entity.Property(e => e.TaskId).HasColumnName("task_id");

                entity.Property(e => e.TimeCreated).HasColumnName("time_created");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TaskHours)
                    .HasForeignKey(d => d.TaskId);
            });

            modelBuilder.Entity<TaskResource>(entity =>
            {
                entity.ToTable("task_resource");

                entity.Property(e => e.TaskResourceId)
                    .ValueGeneratedNever()
                    .HasColumnName("task_resource_id");

                entity.Property(e => e.TaskResourceLevel).HasColumnName("task_resource_level");

                entity.Property(e => e.TaskResourceResourceId).HasColumnName("task_resource_resource_id");

                entity.Property(e => e.TaskResourceTaskId).HasColumnName("task_resource_task_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
