using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DomainClasses;

namespace OutOfTheBoxMvc.Models
{
    public class MatchViewModel
    {
        public Match Match { get; set; }
        public List<Stage> Stages { get; set; }
        public List<SelectListItem> MembersSelectList { get; set; }
        public List<int> Competitors { get; set; }

    }
}
