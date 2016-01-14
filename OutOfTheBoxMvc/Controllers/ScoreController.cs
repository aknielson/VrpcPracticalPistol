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
            var match = db.Matches.OrderByDescending(x => x.Date).ToList().FirstOrDefault();
            var matchId = match?.Id;
            var competitors = db.Competitors.Where(x => x.Match_Id == matchId).ToList();
            
            if (db.CompetitorStages.Count() == 0 || !db.CompetitorStages.Any(x => x.Match_Id == matchId))
            {
                foreach (var competitor in competitors)
                {
                    foreach (var stage in db.Stages.Where(x => x.Match_Id == matchId).ToList())
                    {

                        var competitorStage = new CompetitorStage();
                        competitorStage.Match_Id = matchId;
                        competitorStage.Competitor_Id = competitor.Id;
                        competitorStage.Stage_Id = stage.Id;
                        //competitorStage.MatchStageTimes = new List<MatchStageTime>(stage.NumberOfStrings);
                        for(int i = 0; i < stage.NumberOfStrings; i++)
                        {
                            db.MatchStageTimes.Add(new MatchStageTime { CompetitorStage = competitorStage });
                        }
                        db.CompetitorStages.Add(competitorStage);
                    }

                }
                db.SaveChanges();
            }

            var competitorStageScores = db.CompetitorStages.Where(x => x.Match_Id == matchId).ToList();
            var competitorStageScoresEmpty = competitorStageScores.Where(x => x.MatchStageTimes.Any(p => p.StageTime != new TimeSpan(0))).ToList();

            var competitorStageScoresFilled = competitorStageScores.Where(x => x.MatchStageTimes.Any(p => p.StageTime == new TimeSpan(0))).ToList();
            // var competitorStageScoresFilled = competitorStageScores.Where(x => x.MatchStageTimes.Any()).ToList();
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