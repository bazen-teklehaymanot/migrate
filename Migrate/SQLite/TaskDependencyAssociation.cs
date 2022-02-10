using System;
using System.Collections.Generic;

#nullable disable

namespace Move.SQLite
{
    public partial class TaskDependencyAssociation
    {
        public long? LeftId { get; set; }
        public long? RightId { get; set; }

        public virtual Task Left { get; set; }
        public virtual Task Right { get; set; }
    }
}
