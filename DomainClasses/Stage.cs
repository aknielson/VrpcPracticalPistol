using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainClasses
{
    public partial class Stage
    {
        
        public Stage()
        {
            CompetitorStages = new HashSet<CompetitorStage>();
        }

        public int Id { get; set; }

        [Required]
        public string StageName { get; set; }

        public bool IncludeInCombinedScore { get; set; }

        public bool IsVirginia { get; set; }

        public int NumberOfStrings { get; set; }

        public int? Designer_Id { get; set; }

        public int? Match_Id { get; set; }

        
        public virtual ICollection<CompetitorStage> CompetitorStages { get; set; }

        public virtual Match Match { get; set; }

        public virtual Member Member { get; set; }
    }
}
