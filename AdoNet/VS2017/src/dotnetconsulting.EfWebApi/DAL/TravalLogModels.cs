// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.
// Thorsten Kansy, www.dotnetconsulting.eu
using System;
using System.Collections.Generic;

namespace dotnetconsulting.EfWebApi.DAL
{
    // [Table("Books", Schema = "dbo")]
    public class TravelVlog
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public virtual HashSet<TravelDay> TravelDays { get; set; }
    }

    // [Table("Authors", Schema = "dbo")]
    public class TravelDay
    {
        public int Id { get; set; }

        public TravelVlog Vlog { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public virtual HashSet<Comment> Comments { get; set; }
    }

    public class Comment
    {
        public int Id { get; set; }

        public string Author { get; set; }
    }
}
