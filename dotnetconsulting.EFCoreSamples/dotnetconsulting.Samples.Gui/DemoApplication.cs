// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using dotnetconsulting.Samples.EFContext;
using dotnetconsulting.Samples.Gui.DemoJobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace dotnetconsulting.Samples.Gui
{
    public class DemoApplication
    {
        private readonly IServiceProvider diContainter;

        public DemoApplication(IServiceProvider dependencyInjectionContainer)
        {
            diContainter = dependencyInjectionContainer;
        }

        public void Run()
        {
            ILogger logger = diContainter.GetService<ILogger<DemoApplication>>();
            logger.LogInformation("== Running ==");

            // Data Seeding
            SamplesContext1 efContext = diContainter.GetService<SamplesContext1>();
            efContext.SeedDemoData();

            // Demos
            IDemoJob demoJob;
            demoJob = diContainter.GetService<CreateEntities>();
            // demoJob = diContainter.GetService<QueryEntities>();
            // demoJob = diContainter.GetService<ModifyEntities>();
            // demoJob = diContainter.GetService<DeleteEntities>();
            // demoJob = diContainter.GetService<DbFunction>();
            // demoJob = diContainter.GetService<ChangeTracker>();
            // demoJob = diContainter.GetService<DirectSql>();
            // demoJob = diContainter.GetService<Concurrency>();
            // demoJob = diContainter.GetService<GlobalQueryFilter>();
            // demoJob = diContainter.GetService<ShadowProperties>();
            // demoJob = diContainter.GetService<ExplicitlyCompiledQueries>();
            // demoJob = diContainter.GetService<LoadingStrategies>();
            // demoJob = diContainter.GetService<GraphUpdate>();
            
            // Und Action!
            Console.WriteLine($"=== {demoJob.Title} ===");
            demoJob.Run();

            logger.LogInformation("== Fertig ==");
            Console.ReadKey();
        }
    }
}