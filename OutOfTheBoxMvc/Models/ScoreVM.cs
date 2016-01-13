using DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OutOfTheBoxMvc.Models
{
    public class ScoreVM
    { 
        public int MatchId { get; set; }
        public List<SelectListItem> Competitor { get; set; }
        public List<SelectListItem> Stage { get; set; }

        public TimeSpan ElapsedTime { get; set; }

        public int PenaltyPoints { get; set; }
         
    }
}