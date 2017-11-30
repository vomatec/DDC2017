// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.
// Thorsten Kansy, www.dotnetconsulting.eu
using Microsoft.EntityFrameworkCore;

namespace dotnetconsulting.EfWebApi.DAL
{
    //dotnet ef database update -v
    //dotnet ef migrations add Init -v
    //dotnet ef migrations remove -v

    public class TravelLogContext : DbContext
    {
        public DbSet<TravelVlog> TravelLogs { get; set; }

        public TravelLogContext(DbContextOptions<TravelLogContext> options): base(options)
        {

        }
    }
}
