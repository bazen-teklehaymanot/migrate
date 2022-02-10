using System;
using System.Collections.Generic;

#nullable disable

namespace Move.SQLite
{
    public partial class TaskHour
    {
        public long TaskHourId { get; set; }
        public long TaskId { get; set; }
        public long? TaskHourTypeId { get; set; }
        public double TaskHours { get; set; }
        public long? UserId { get; set; }
        public byte[] TimeCreated { get; set; }

        public virtual Task Task { get; set; }
    }
}
