// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.
// Thorsten Kansy, www.dotnetconsulting.eu

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace dotnetconsulting.EFCore1
{
    public class LibraryDbContext : DbContext
    {
        private readonly IConfigurationRoot _config;
        public LibraryDbContext(IConfigurationRoot config)
        {
            _config = config;
            // Schauen wir dem EF auf die Finger
            this.LogToConsole();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // ConnectionString auf Konfiguration auslesen 
            string conString = _config["ConnectionStrings:EFCoreLibrary"];

            optionsBuilder.UseSqlServer(conString);
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
    }

    public static class DbContextExtensions
    {
        public static void LogToConsole(this DbContext context)
        {
            ILoggerFactory loggerFactory = context.GetService<ILoggerFactory>();
            loggerFactory.AddConsole();
        }
    }
}
