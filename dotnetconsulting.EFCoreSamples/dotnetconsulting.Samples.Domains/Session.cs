using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace dotnetconsulting.Samples.Domains
{
    public class Session
    {
        public int Id { get; set; }

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        public string Abstract { get; set; }

        public DifficultyLevel Difficulty { get; set; }

        public int Duration { get; set; }

        public int? EventId { get; set; }

        public ICollection<SpeakerSession> SpeakerSessions { get; set; }

        public int SpeakerId { get; set; }

        public TechEvent TechEvent { get; set; }

        public int TechEventId { get; set; }

        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public bool IsDeleted { get; set; }

        public Session()
        {
            SpeakerSessions = new HashSet<SpeakerSession>();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

    public enum DifficultyLevel
    {
        Level1,
        Level2,
        Level3,
        Level4
    }
}