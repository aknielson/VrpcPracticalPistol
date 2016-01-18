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
        public ActionResult Index(int? currentMatchId = null)
        {           
            if (currentMatchId == null)
            {
                var match = db.Matches.OrderByDescending(x => x.Date).ToList().FirstOrDefault();
                currentMatchId = match?.Id;
            }
            BuildStateScores(currentMatchId.Value);
            var matchCompetitors = db.Competitors.Where(x => x.Match_Id == currentMatchId).ToList();

            int maxrows = matchCompetitors.Count();
            var competitorGrid = new CompetitorStage[3, maxrows];
            var namegrid = new string[3, maxrows];

            var crap = db.CompetitorStages.Where(x => x.Match_Id == currentMatchId).ToList();

            int currentRow = 0;
            ////////////////////////////
            foreach (var cs in db.CompetitorStages.Where(x => x.Match_Id == currentMatchId).OrderBy(x => x.Stage_Id).ToList())
            {
                if (cs.Stage.StageOrder < 4)
                {
                    if (namegrid[cs.Stage.StageOrder.Value - 1, currentRow] == null)
                        namegrid[cs.Stage.StageOrder.Value - 1, currentRow] = cs.Competitor.Member.FirstName;
                    else
                    {
                        currentRow++;
                        if (currentRow == maxrows)
                            currentRow = 0;
                        namegrid[cs.Stage.StageOrder.Value - 1, currentRow] = cs.Competitor.Member.FirstName;
                    }
                    
                }
                if (currentRow == maxrows)
                    currentRow = 0;
            }

            var ffeed = namegrid;




            /////////////////////////////

            

            while (currentRow < maxrows)
            {
                foreach (var competitor in db.Competitors.Where(x => x.Match_Id == currentMatchId))
                {
                    if (currentRow >= maxrows)
                        break;
                    if (competitorGrid[0, currentRow] == null)
                    {
                        var stageOneForCompetitor = competitor.CompetitorStages.FirstOrDefault(x => x.Stage.StageOrder == (1));
                        if (!stageOneForCompetitor.IsScoringComplete.Value && !SpotFilled(competitorGrid,stageOneForCompetitor))
                        {
                            competitorGrid[0, currentRow] = stageOneForCompetitor;
                            namegrid[0, currentRow] = stageOneForCompetitor.Competitor.Member.FirstName;
                            if (competitorGrid[0, currentRow] != null && competitorGrid[1, currentRow] != null && competitorGrid[2, currentRow] != null)
                                currentRow++;
                            continue;
                        }
                    }

                    if (competitorGrid[1, currentRow] == null)
                    {
                        var stageTwoForCompetitor = competitor.CompetitorStages.FirstOrDefault(x => x.Stage.StageOrder == (2));
                        if (!stageTwoForCompetitor.IsScoringComplete.Value && !SpotFilled(competitorGrid, stageTwoForCompetitor))
                        {
                            competitorGrid[1, currentRow] = stageTwoForCompetitor;

                            namegrid[1, currentRow] = stageTwoForCompetitor.Competitor.Member.FirstName;
                            if (competitorGrid[0, currentRow] != null && competitorGrid[1, currentRow] != null && competitorGrid[2, currentRow] != null)
                                currentRow++;
                            continue;
                        }
                    }

                    if (competitorGrid[2, currentRow] == null)
                    {
                        var stageThreeForCompetitor = competitor.CompetitorStages.FirstOrDefault(x => x.Stage.StageOrder == (3));
                        if (!stageThreeForCompetitor.IsScoringComplete.Value && !SpotFilled(competitorGrid, stageThreeForCompetitor))
                        {
                            competitorGrid[2, currentRow] = stageThreeForCompetitor;
                            namegrid[2, currentRow] = stageThreeForCompetitor.Competitor.Member.FirstName;
                            if (competitorGrid[0, currentRow] != null && competitorGrid[1, currentRow] != null && competitorGrid[2, currentRow] != null)
                                currentRow++;
                            continue;
                        }
                    }
                    currentRow++;
                }
            }
            var foo = competitorGrid;
            var psdf = namegrid;

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

        private bool SpotFilled(CompetitorStage[,] myarray, CompetitorStage competitorStage)
        {
            bool returnVal = false;

            var rowLowerLimit = myarray.GetLowerBound(0);
            var rowUpperLimit = myarray.GetUpperBound(0);

            var colLowerLimit = myarray.GetLowerBound(1);
            var colUpperLimit = myarray.GetUpperBound(1);

            for (int row = rowLowerLimit; row < rowUpperLimit; row++)
            {
                for (int col = colLowerLimit; col < colUpperLimit; col++)
                {
                    if (myarray[row, col] == competitorStage)
                        return true;
                }
            }

            return returnVal;
        }

        private void BuildStateScores(int currentMatchId)
        {
            var competitors = db.Competitors.Where(x => x.Match_Id == currentMatchId).ToList();

            if (db.CompetitorStages.Count() == 0 || !db.CompetitorStages.Any(x => x.Match_Id == currentMatchId))
            {
                foreach (var competitor in competitors)
                {
                    foreach (var stage in db.Stages.Where(x => x.Match_Id == currentMatchId).ToList())
                    {
                        var competitorStage = new CompetitorStage();
                        competitorStage.Match_Id = currentMatchId;
                        competitorStage.Competitor_Id = competitor.Id;
                        competitorStage.Stage_Id = stage.Id;
                        competitorStage.IsScoringComplete = false;
                        //competitorStage.MatchStageTimes = new List<MatchStageTime>(stage.NumberOfStrings);
                        for (int i = 0; i < stage.NumberOfStrings; i++)
                        {
                            db.MatchStageTimes.Add(new MatchStageTime { CompetitorStage = competitorStage });
                        }
                        db.CompetitorStages.Add(competitorStage);
                    }
                }
                db.SaveChanges();
            }
        }
    }
}