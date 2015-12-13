using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DomainClasses;

namespace OutOfTheBoxMvc.Models
{
    public class CompetitorsViewModel
    {
        public Match Match { get; set; }
        public List<Competitor> Competitors { get; set; }

        public List<SelectListItem> MembersList { get; set; }
    }
}
