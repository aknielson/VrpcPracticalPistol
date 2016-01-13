using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
       
        public int? Caliber_Id { get; set; }
        public virtual Caliber Caliber { get; set; }
        
        public int MagazineCapacity { get; set; }       

        public int? Match_Id { get; set; }
        public virtual Match Match { get; set; }

        [Display(Name = "Member")]
        public int? Member_Id { get; set; }

        public virtual Member Member { get; set; }
        
        public virtual ICollection<CompetitorStage> CompetitorStages { get; set; }
    }
}
