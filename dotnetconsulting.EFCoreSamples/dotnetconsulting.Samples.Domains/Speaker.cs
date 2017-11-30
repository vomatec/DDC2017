// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace dotnetconsulting.Samples.Domains
{
    public class Speaker
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Homepage { get; set; }

        public string Infos { get; set; }

        public ICollection<SpeakerSession> SpeakerSessions { get; set; } = new List<SpeakerSession>();

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public bool IsDeleted { get; set; }

        public Speaker()
        {
            SpeakerSessions = new HashSet<SpeakerSession>();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}