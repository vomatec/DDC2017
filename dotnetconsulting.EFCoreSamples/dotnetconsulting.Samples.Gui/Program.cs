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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace dotnetconsulting.Samples.Gui
{
    class Program
    {
        static void Main(string[] args)
        {
            // Konfiguration vorbereiten
            IConfigurationRoot config = createConfigurationRoot();

            // Konfiguration und DI initlaisieren
            IServiceProvider dependencyInjectionContainer = createDependencyInjectionContainer(config);

            // DemoApp starten
            DemoApplication app = new DemoApplication(dependencyInjectionContainer);
            app.Run();
        }

        private static IConfigurationRoot createConfigurationRoot()
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            return configBuilder.Build();
        }

        private static IServiceProvider createDependencyInjectionContainer(IConfigurationRoot config)
        {
            IServiceCollection diContainer = new ServiceCollection();

            // Logging einrichtgen
            diContainer.AddSingleton(new LoggerFactory().AddConsole())
                       .AddLogging()

            // Konfiguration
            .AddSingleton(config)

            // EF Context konfigurieren
            .AddDbContext<SamplesContext1>(
                o => o.UseSqlServer(config["ConnectionStrings:EFConString"])
                .ConfigureWarnings(w => w.Throw(RelationalEventId.QueryClientEvaluationWarning))
                .ConfigureWarnings(w => w.Throw(RelationalEventId.ConnectionError))
                .EnableSensitiveDataLogging(true)
            )

            .AddDbContextPool<SamplesContext3>(
                    o => o.UseSqlServer(config["ConnectionStrings:EFConString"])
            )

            // Jobs hinzufügen
            .AddTransient<CreateEntities>()
            .AddTransient<QueryEntities>()
            .AddTransient<ModifyEntities>()
            .AddTransient<DeleteEntities>()
            .AddTransient<ChangeTracker>()
            .AddTransient<DbFunction>()
            .AddTransient<Concurrency>()
            .AddTransient<DirectSql>()
            .AddTransient<GlobalQueryFilter>()
            .AddTransient<ShadowProperties>()
            .AddTransient<ExplicitlyCompiledQueries>()
            .AddTransient<LoadingStrategies>()
            .AddTransient<GraphUpdate>();

            // Rückgabe
            IServiceProvider serviceProvider = diContainer.BuildServiceProvider();
            return serviceProvider;
        }
    }
}