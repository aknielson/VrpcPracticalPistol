using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DomainClasses
{
    public partial class CompetitorStage
    {
        
        public CompetitorStage()
        {
            MatchStageTimes = new HashSet<MatchStageTime>();
        }

        public int Id { get; set; }

        public int PenaltyPoints { get; set; }

        public int? Competitor_Id { get; set; }

        public int? Stage_Id { get; set; }

        public int? Match_Id { get; set; }

        public virtual Competitor Competitor { get; set; }

        public virtual Match Match { get; set; }

        public virtual Stage Stage { get; set; }

        
        public virtual ICollection<MatchStageTime> MatchStageTimes { get; set; }
    }
}
