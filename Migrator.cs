using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Migrate.Bridges;
using Migrate.PostgreSQL;
using Migrate.SQLite;

namespace Migrate;
class Migrator
{
    public static async Task<int> TryRun(OldDBContext oldDbContext, NewDBContext newDbContext) 
    {
        try
        {
            return await Run(oldDbContext, newDbContext);
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
            return 1;
        }
    }
    public static async Task<int> Run(OldDBContext oldDbContext, NewDBContext newDbContext) 
    {
        Console.WriteLine("Checking database availability");
        bool canConnect = await oldDbContext.Database.CanConnectAsync();
        if (!canConnect) 
        {
            Console.WriteLine("Unable to connect to source database, please check connection string is correct");
            return 1;
        }
        newDbContext.Database.EnsureCreated();
        canConnect = await newDbContext.Database.CanConnectAsync();
        if (!canConnect)
        {
            Console.WriteLine("Unable to connect to target database, plaase check connection string is correct");
            return 1;
        }
        Console.WriteLine("Databases are available, starting migration...");
        var taskCount = oldDbContext.Tasks.Count();
        Console.WriteLine($"{taskCount} Tasks found");
        var tasks = oldDbContext.Tasks;
        int index = 0;
        foreach (var task in tasks) 
        {
            var tda = oldDbContext.TaskDependencyAssociations.FirstOrDefault(entry => entry.LeftId == task.TaskId);
            var task_date = oldDbContext.TaskDates.FirstOrDefault(entry => entry.TaskId == task.TaskId);
            var task_hour = oldDbContext.TaskHours.FirstOrDefault(entry => entry.TaskId == task.TaskId);
            newDbContext.Tasks.Add(new TaskBridge(task, tda, task_date, task_hour));
            index+=1;
            Console.WriteLine($"{index} Tasks migrated");
        }
        await newDbContext.SaveChangesAsync();
        Console.WriteLine($"Tasks migration completed");
        Console.WriteLine($"Starting project_task_resource migration...");
        var task_resource_count = oldDbContext.TaskResources.Count();
        Console.WriteLine($"{task_resource_count} Task Resources found");
        var task_resources = oldDbContext.TaskResources;
        index = 0;
        var task_resource_level_sum = task_resources.Sum(entry => entry.TaskResourceLevel);
        foreach (var tr in task_resources)
        {
            var hour_budget = newDbContext.Tasks.FirstOrDefault(entry => entry.Id == tr.TaskResourceTaskId)?.TaskHourBudget;
            newDbContext.ProjectTaskResources.Add(new TaskResourceBridge(tr,task_resource_level_sum, hour_budget));
            index += 1;
            Console.WriteLine($"{index} task resource migrated");
        }
        await newDbContext.SaveChangesAsync();
        Console.WriteLine("Migration completed!");
        return 0;
    }
}
