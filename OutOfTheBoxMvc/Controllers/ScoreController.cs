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
            var competitorGrid2 = new CompetitorStage[maxrows,3];
            var namegrid = new string[3, maxrows];

            var crap = db.CompetitorStages.Where(x => x.Match_Id == currentMatchId).ToList();

            foreach (var item in crap)
            {
                if (item.Stage.StageOrder < 4)
                PlaceInNextOpenSpot(ref competitorGrid2, item);
            }

            var how = competitorGrid2;

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

            ViewBag.NameGrid = namegrid;

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
                            //namegrid[0, currentRow] = stageOneForCompetitor.Competitor.Member.FirstName;
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

                            //namegrid[1, currentRow] = stageTwoForCompetitor.Competitor.Member.FirstName;
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
                           // namegrid[2, currentRow] = stageThreeForCompetitor.Competitor.Member.FirstName;
                            if (competitorGrid[0, currentRow] != null && competitorGrid[1, currentRow] != null && competitorGrid[2, currentRow] != null)
                                currentRow++;
                            continue;
                        }
                    }
                    currentRow++;
                }
            }

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

        public ActionResult ShowScoreTable()
        {

            var match = db.Matches.OrderByDescending(x => x.Date).ToList().FirstOrDefault();
            var currentMatchId = match?.Id;

            int maxrows = db.CompetitorStages.Where(x => x.Match_Id == currentMatchId).Count();
            var competitorGrid2 = new CompetitorStage[maxrows, 3];

            var nameGrid = new string[maxrows, 3];

            
            var crap = db.CompetitorStages.Where(x => x.Match_Id == currentMatchId).ToList();

            foreach (var item in crap.OrderBy(x =>x.Stage.StageOrder))
            {
                var oo = item.Competitor.Member.FirstName;
                if (item.Stage.StageOrder < 4)
                    PlaceInNextOpenSpot(ref competitorGrid2, item);
            }

            var how = competitorGrid2;
            var rowLowerLimit = competitorGrid2.GetLowerBound(0);
            var rowUpperLimit = competitorGrid2.GetUpperBound(0);

            var colLowerLimit = competitorGrid2.GetLowerBound(1);
            var colUpperLimit = competitorGrid2.GetUpperBound(1);

            for (int row = rowLowerLimit; row < rowUpperLimit; row++)
            {
                for (int col = colLowerLimit; col <= colUpperLimit; col++)
                {
                    if (competitorGrid2[row,col] != null)
                        nameGrid[row,col] = competitorGrid2[row, col].Competitor.Member.FirstName;
                }
            }
            ViewBag.NameGrid = nameGrid;
            return View();
        }

        public ActionResult PopTableFirst()
        {
            var match = db.Matches.OrderByDescending(x => x.Date).ToList().FirstOrDefault();
            var currentMatchId = match?.Id;

            var competitorArray = db.Competitors.Where(x => x.Match_Id == currentMatchId).ToArray();
            var stageOneCompetitorList = db.CompetitorStages.Where(x => x.Stage.StageOrder == 1).ToArray();
            var stageTwoCompetitorList = db.CompetitorStages.Where(x => x.Stage.StageOrder == 2).ToArray();
            var stageThreeCompetitorList = db.CompetitorStages.Where(x => x.Stage.StageOrder == 3).ToArray();



            var competitorStagesList = db.CompetitorStages.ToList();

            int maxrows = db.CompetitorStages.Where(x => x.Match_Id == currentMatchId).Count();

            var competitorGrid2 = new CompetitorStage[maxrows, 3];

            var nameGrid = new string[maxrows, 3];

            var rowLowerLimit = nameGrid.GetLowerBound(0);
            var rowUpperLimit = nameGrid.GetUpperBound(0);

            var colLowerLimit = nameGrid.GetLowerBound(1);
            var colUpperLimit = nameGrid.GetUpperBound(1);

            for (int row = rowLowerLimit; row < rowUpperLimit; row++)
            {
                //first which competitor
                foreach(var competitor in competitorArray)
                {

                    //todo: check if row full... if so then next row?
                    for (int col = colLowerLimit; col < colUpperLimit; col++)
                    {
                        nameGrid[row, col] = competitorStagesList.Where(x => x.Competitor_Id == competitor.Id).Where(x => x.Stage.StageOrder == col + 1).FirstOrDefault().Competitor.Member.FirstName;   
                    }
                }                
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

        private void PlaceInNextOpenSpot(ref CompetitorStage[,] myarray, CompetitorStage competitorStage)
        {
            bool returnVal = false;

            var rowLowerLimit = myarray.GetLowerBound(0);
            var rowUpperLimit = myarray.GetUpperBound(0);

            var colLowerLimit = myarray.GetLowerBound(1);
            var colUpperLimit = myarray.GetUpperBound(1);

            int openRow = -1;

            //is competitor already in that row?
            for (int row = rowLowerLimit; row < rowUpperLimit; row++)
            {
                bool competitorAlreadyInRow = false;
                for (int col = colLowerLimit; col < colUpperLimit; col++)
                { 
                    if (myarray[row, col]  != null)                 
                    if (myarray[row, col].Competitor_Id == competitorStage.Competitor_Id)
                        competitorAlreadyInRow = true;
                }
                if (!competitorAlreadyInRow)
                {
                    //now check to see if this competitorStage is open
                    if (myarray[row, competitorStage.Stage.StageOrder.Value - 1] == null)
                    {
                        myarray[row, competitorStage.Stage.StageOrder.Value - 1] = competitorStage;
                        row = rowUpperLimit;
                    }
                }

            }

           
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