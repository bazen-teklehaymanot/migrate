// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.CommandLineUtils;
using Migrate;
using Migrate.PostgreSQL;
using Migrate.SQLite;
using System;

namespace Migrate
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var app = new CommandLineApplication();
            app.Name = "Move";
            app.HelpOption("-?|-h|--help");

            app.Command("db", cmd =>
            {
                cmd.HelpOption("-?|-h|--help");
                cmd.Description = "Migrates SQLite DB entries into PostgresSQL";
                var sourceOpt = cmd.Option("-s|--source",
                                                               "Path to source SQLite database",
                                                               CommandOptionType.SingleValue);
                var targetOpt = cmd.Option("-t|--target",
                                                  "Connection string to destination connection string. eg) \"Host = <host>; Database = <database>; Username = <user>; Password = <password>\"",
                                                  CommandOptionType.SingleValue);
                cmd.OnExecute(async () =>
                {
                    string sourceConnection = sourceOpt.Value();
                    string targetConnection = targetOpt.Value();
                    Console.WriteLine($"Source: {sourceConnection}");
                    Console.WriteLine($"Target: {targetConnection}");
                    if (string.IsNullOrEmpty(sourceConnection))
                    {
                        Console.WriteLine("SQLite database file path is required");
                        return 1;
                    }
                    if (string.IsNullOrEmpty(targetConnection))
                    {
                        Console.WriteLine("PostgreSQL connection string required");
                        return 1;
                    }
                    return await Migrator.TryRun(new OldDBContext(sourceConnection), new NewDBContext(targetConnection));
                });
            });


            app.Execute(args);

        }
    }
}