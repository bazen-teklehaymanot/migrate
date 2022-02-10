using System;
using System.Collections.Generic;

#nullable disable

namespace Move.SQLite
{
    public partial class Task
    {
        public Task()
        {
            TaskDates = new HashSet<TaskDate>();
            TaskDependecyAssociationLefts = new HashSet<TaskDependecyAssociation>();
            TaskDependecyAssociationRights = new HashSet<TaskDependecyAssociation>();
            TaskHours = new HashSet<TaskHour>();
            TaskResources = new HashSet<TaskResource>();
        }

        public long TaskId { get; set; }
        public long TaskProjectId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public byte[] TaskIsMilestone { get; set; }
        public long? TaskProgress { get; set; }
        public long TaskProjectElementId { get; set; }
        public byte[] TaskHasWeekendHours { get; set; }

        public virtual ICollection<TaskDate> TaskDates { get; set; }
        public virtual ICollection<TaskDependecyAssociation> TaskDependecyAssociationLefts { get; set; }
        public virtual ICollection<TaskDependecyAssociation> TaskDependecyAssociationRights { get; set; }
        public virtual ICollection<TaskHour> TaskHours { get; set; }
        public virtual ICollection<TaskResource> TaskResources { get; set; }
    }
}
