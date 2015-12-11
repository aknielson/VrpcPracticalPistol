using System;

namespace DomainClasses
{
    public partial class MatchStageTime
    {
        public int Id { get; set; }

        public TimeSpan StageTime { get; set; }

        public int? CompetitorStage_Id { get; set; }

        public virtual CompetitorStage CompetitorStage { get; set; }
    }
}
