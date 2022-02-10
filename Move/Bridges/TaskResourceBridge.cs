using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Move.SQLite;

namespace Move.Bridges
{
    public class TaskResourceBridge
    {
        public TaskResource TaskResource { get; set; }
        public long? SumOfResourceLevel { get; set; }
        public double? TaskHourBudget { get; set; }

        public TaskResourceBridge(TaskResource tr, long? sor, double? hour_budget)
        {
            TaskResource = tr;
            SumOfResourceLevel = sor;
            TaskHourBudget = hour_budget;
        }
    }
}