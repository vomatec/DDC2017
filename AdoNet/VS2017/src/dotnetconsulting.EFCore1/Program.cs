// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.
// Thorsten Kansy, www.dotnetconsulting.eu

using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace dotnetconsulting.EFCore1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Konfiguration vorbereiten
            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            IConfigurationRoot config = configBuilder.Build();
            
            // Einfügen
            using (LibraryDbContext context = new LibraryDbContext(config))
            {
                Author autor = new Author() { Name = "Thorsten Kansy", Birthday = new DateTime(1991, 12, 18) };
                context.Add(autor);
                context.SaveChanges();
            }
            Debugger.Break();

            // Lesen
            using (LibraryDbContext context = new LibraryDbContext(config))
            {
                var query = from authors in context.Authors
                            where authors.Name == "Thorsten Kansy"
                            select authors;

                int c = query.Count();
                Console.WriteLine($"c={c}");
            }
            Debugger.Break();

            // Aktualisieren
            using (LibraryDbContext context = new LibraryDbContext(config))
            {
                Author author = context.Authors.Find(5);

                if (author != null)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        author.Books.Add(new Book() { Title = $"Buch #{i + 1}", Pages = 999 });
                    }
                }

                context.SaveChanges();
            }
            Debugger.Break();

            // Löschen
            using (LibraryDbContext context = new LibraryDbContext(config))
            {
                var author = (from authors in context.Authors
                            where authors.Name == "Thorsten Kansy"
                            select authors).FirstOrDefault();

                if (author != null)
                    context.Authors.Remove(author);

                context.SaveChanges();
            }
            Debugger.Break();

            Console.WriteLine("== Fertig ==");
            Console.ReadKey();
        }
    }
}
