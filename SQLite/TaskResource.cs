using System;
using System.Collections.Generic;

#nullable disable

namespace Migrate.SQLite
{
    public partial class TaskResource
    {
        public long TaskResourceId { get; set; }
        public long? TaskResourceTaskId { get; set; }
        public long? TaskResourceResourceId { get; set; }
        public long? TaskResourceLevel { get; set; }
    }
}
