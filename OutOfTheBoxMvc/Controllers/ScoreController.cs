using DataModel;
using DomainClasses;
using OutOfTheBoxMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OutOfTheBoxMvc.Controllers
{
    public class ScoreController : Controller
    {
        private VrpcPracticalPistolContext db = new VrpcPracticalPistolContext();
        // GET: Score
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Entry(int matchId)
        {
            ScoreVM scoreVM = new ScoreVM();
            scoreVM.MatchId = matchId;

            scoreVM.Competitor = new List<SelectListItem>();
            var competitorList = db.Competitors.Where(x => x.Match_Id.Equals(matchId));
            scoreVM.Competitor = new List<SelectListItem>();
            foreach (var competitor in competitorList)
            {
                scoreVM.Competitor.Add(new SelectListItem { Value = competitor.Id.ToString(), Text = competitor.Member.FirstName + " " + competitor.Member.LastName });
            }
            return View();
        }
    }
}