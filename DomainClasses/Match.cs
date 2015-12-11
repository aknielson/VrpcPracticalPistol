using System;
using System.Collections.Generic;

namespace DomainClasses
{
    public class Match
    {
        
        public Match()
        {
            Competitors = new HashSet<Competitor>();
            CompetitorStages = new HashSet<CompetitorStage>();
            Stages = new HashSet<Stage>();
        }

        public int Id { get; set; }

        public DateTime Date { get; set; }

      
        public virtual ICollection<Competitor> Competitors { get; set; }

        public virtual ICollection<CompetitorStage> CompetitorStages { get; set; }

     
        public virtual ICollection<Stage> Stages { get; set; }
    }
}
