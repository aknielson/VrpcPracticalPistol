using System.Collections.Generic;

namespace DomainClasses
{
    public class Member
    {
        
        public Member()
        {
            Competitors = new HashSet<Competitor>();
            Stages = new HashSet<Stage>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public virtual ICollection<Competitor> Competitors { get; set; }

        public virtual ICollection<Stage> Stages { get; set; }
    }
}
