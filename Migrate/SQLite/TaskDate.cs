using System;
using System.Collections.Generic;

#nullable disable

namespace Move.SQLite
{
    public partial class TaskDate
    {
        public long TaskDateId { get; set; }
        public long? TaskId { get; set; }
        public long? TaskDateTypeId { get; set; }
        public long? TaskDate1 { get; set; }
        public long? UserId { get; set; }
        public long? TimeCreated { get; set; }

        public virtual Task Task { get; set; }
    }
}
