using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DomainClasses
{
    public partial class Competitor
    {
       
        public Competitor()
        {
            CompetitorStages = new HashSet<CompetitorStage>();
        }

        public int Id { get; set; }

        public string FireArm { get; set; }

        public string Caliber { get; set; }

        public string UspsaDivision { get; set; }

        public string UspsaDivisionClassification { get; set; }

        public string UspsaPowerFactor { get; set; }

        public int? Match_Id { get; set; }

        public int? Member_Id { get; set; }

        public virtual Match Match { get; set; }

        public virtual Member Member { get; set; }

        
        public virtual ICollection<CompetitorStage> CompetitorStages { get; set; }
    }
}
