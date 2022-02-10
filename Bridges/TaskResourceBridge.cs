using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Migrate.SQLite;

namespace Migrate.Bridges;
public class TaskResourceBridge
{
    public TaskResource TaskResource { get; set; }
    public long? SumOfResourceLevel { get; set; }
    public long? TaskHourBudget { get; set; }

    public TaskResourceBridge(TaskResource tr, long? sor, long? hour_budget)
    {
        TaskResource = tr;
        SumOfResourceLevel = sor;
        TaskHourBudget = hour_budget;
    }
}
