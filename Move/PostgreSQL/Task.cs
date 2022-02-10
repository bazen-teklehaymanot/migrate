using System;
using System.Collections.Generic;
using Migrate.Bridges;

#nullable disable

namespace Migrate.PostgreSQL
{
    public partial class Task
    {
        public long Id { get; set; }
        public long? TaskProjectId { get; set; }
        public string TaskName { get; set; }
        public long? TaskIsMilestone { get; set; }
        public long? TaskProgress { get; set; }
        public long? TaskProjectElementId { get; set; }
        public long? TaskHasWeekendHours { get; set; }
        public long? ParentId { get; set; }
        public long? TaskStartDate { get; set; }
        public long? TaskEndDate { get; set; }
        public long? TaskHourBudget { get; set; }
        public long? TaskHourForecast { get; set; }

        public static implicit operator Task(TaskBridge bridge) 
        {
            var entity =  new Task 
            {
                Id = bridge.Task.TaskId,
                TaskProjectId = bridge.Task.TaskProjectId,
                TaskName = bridge.Task.TaskName,
                TaskIsMilestone = bridge.Task.TaskIsMilestone,
                TaskProgress = bridge.Task.TaskProgress,
                TaskProjectElementId = bridge.Task.TaskProjectElementId,
                TaskHasWeekendHours = bridge.Task.TaskHasWeekendHours,
            };
            try
            {
                entity.ParentId = bridge.TDA?.LeftId;
                entity.TaskStartDate = bridge.TaskDate?.TaskDateId == 1 ? bridge.TaskDate.TaskDate1 : default;
                entity.TaskEndDate = bridge.TaskDate?.TaskDateId == 4 ? bridge.TaskDate.TaskDate1 : default;
                entity.TaskHourBudget = bridge.TaskHour?.TaskHourTypeId == 1 ? bridge.TaskHour.TaskHours : default;
                entity.TaskHourForecast = bridge.TaskHour?.TaskHourTypeId == 1 ? bridge.TaskHour.TaskHours : default;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            return entity;
        }
    }
}
