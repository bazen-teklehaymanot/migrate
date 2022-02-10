using System;
using System.Collections.Generic;
using Migrate.Bridges;

#nullable disable

namespace Migrate.PostgreSQL
{
    public partial class ProjectTaskResource
    {
        public long? TaskResourceTaskId { get; set; }
        public long? TaskResourceResourceId { get; set; }
        public long? TaskResourceLevel { get; set; }
        public long? TaskResourceHour { get; set; }

        public virtual Task TaskResourceTask { get; set; }

        public static implicit operator ProjectTaskResource(TaskResourceBridge bridge) 
        {
            var entity = new ProjectTaskResource 
            {
                TaskResourceTaskId = bridge.TaskResource.TaskResourceTaskId,
                TaskResourceResourceId = bridge.TaskResource.TaskResourceResourceId,
                TaskResourceLevel = bridge.TaskResource.TaskResourceLevel,
                TaskResourceHour = bridge.TaskHourBudget * (bridge.TaskResource.TaskResourceLevel/bridge.SumOfResourceLevel)
            };

            return entity;
        }
    }
}
