using System;
using System.Collections.Generic;

#nullable disable

namespace Migrate.SQLite
{
    public partial class Task
    {
        public Task()
        {
            TaskDates = new HashSet<TaskDate>();
            TaskHours = new HashSet<TaskHour>();
        }

        public long TaskId { get; set; }
        public long? TaskProjectId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public long? TaskIsMilestone { get; set; }
        public long? TaskProgress { get; set; }
        public long? TaskProjectElementId { get; set; }
        public long? TaskHasWeekendHours { get; set; }

        public virtual ICollection<TaskDate> TaskDates { get; set; }
        public virtual ICollection<TaskHour> TaskHours { get; set; }
    }
}
