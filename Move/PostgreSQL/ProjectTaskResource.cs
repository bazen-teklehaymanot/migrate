﻿using System;
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
                TaskResourceTaskId = bridge.TaskResource?.TaskResourceTaskId,
                TaskResourceResourceId = bridge.TaskResource?.TaskResourceResourceId,
                TaskResourceLevel = bridge.TaskResource?.TaskResourceLevel
            };
            try
            {
                entity.TaskResourceHour = bridge.TaskHourBudget * (bridge.TaskResource.TaskResourceLevel / bridge.SumOfResourceLevel);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error setting TaskResourceId {ex.Message}");
            }
            return entity;
        }
    }
}