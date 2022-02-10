
using Migrate.SQLite;

namespace Migrate.Bridges;
public class TaskBridge
{
    public SQLite.Task Task { get; set; }
    public TaskDependencyAssociation? TDA { get; set; }
    public TaskDate? TaskDate { get; set; }
    public TaskHour? TaskHour { get; set; }

    public TaskBridge(SQLite.Task task, TaskDependencyAssociation? tda, TaskDate? taskDate,TaskHour? taskHour)
    {
        TDA = tda;
        Task = task;
        TaskHour = taskHour;
        TaskDate = taskDate;
    }
}
