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

            var competitorList = db.Competitors.Where(x => x.Match_Id == currentMatchId).ToList();
            var competitorStageList = db.CompetitorStages.Where(x => x.Match_Id == currentMatchId).ToList();

            CompetitorStage[,] competitorStageTable = BuildTable(competitorList, competitorStageList);
            ViewBag.NameGrid = competitorStageTable;
            //competitorStageTable[0, 0].Competitor.Member.FirstName;

            return View();
        }
        private void BuildStateScores(int currentMatchId)
        {
            var competitors = db.Competitors.Where(x => x.Match_Id == currentMatchId).ToList();
            var competitorStages = db.CompetitorStages.Where(x => x.Match_Id == currentMatchId).ToList();

            if (competitorStages.Count() == 0 || !competitorStages.Any(x => x.Match_Id == currentMatchId))
            {
                foreach (var competitor in competitors)
                {
                    AddCompetitorStage(currentMatchId, competitor);
                }
                db.SaveChanges();
            } else
            {
                if (db.CompetitorStages.Count() < competitors.Count() * 4)
                {
                    foreach(var competitor in competitors)
                    {
                        if (!competitorStages.Any(x => x.Competitor_Id == competitor.Id))
                        {
                            AddCompetitorStage(currentMatchId, competitor);

                            db.SaveChanges();
                        }
                    }
                }

            }

        }

        public void AddCompetitorStage(int currentMatchId, Competitor competitor)
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


        private CompetitorStage[,] BuildTable(List<Competitor> competitorList, List<CompetitorStage> competitorStageList)
        {
            var rows = competitorList.Count();
            var cols = 3;

            var populate = new List<Competitor>();
            populate.AddRange(competitorList);
            populate.AddRange(competitorList);
            populate.AddRange(competitorList);

            var populateArray = populate.ToArray();

            var myArray = new CompetitorStage[rows, cols];

            var row = 0;

            foreach (var comp in populateArray)
            {
                //is row full... if so, mow next now
                if ((myArray[row, 0] != null) && (myArray[row, 1] != null) && (myArray[row, 2] != null))
                    row++; // Maybe build a row verification method
                //if there is an open position on current row, find a spot for this competitor
                for (var currentCol = 0; currentCol < 3; currentCol++)
                {
                    if (myArray[row, currentCol] == null)
                    {
                        var compStagePopulatedinPrevRow = false;
                        //Does currentCol have this person in a previous row
                        for (var prevRow = 0; prevRow < row; prevRow++)
                        {
                            if (myArray[prevRow, currentCol] != null && myArray[prevRow, currentCol].Competitor_Id.Equals(comp.Id))
                                compStagePopulatedinPrevRow = true;
                        }
                        if (!compStagePopulatedinPrevRow)
                        {
                            myArray[row, currentCol]  =
                                competitorStageList.FirstOrDefault(
                                    x => x.Competitor_Id == comp.Id && x.Stage.StageOrder == currentCol + 1);
                            break; //next competitor please
                        }
                        //are other two spots filled?

                        var rowSpots = new[] { 0, 1, 2 };
                        var rowColsNotThisOne = rowSpots.Where(x => x != currentCol).ToArray();
                        var hasEmptySpots = false;
                        foreach (var colf in rowColsNotThisOne)
                        {
                            if (myArray[row, colf] == null)
                            {
                                hasEmptySpots = true;
                                break;
                            }
                        }
                        if (!hasEmptySpots)
                        {                            
                            myArray[row, 2] = myArray[row, 0];
                            myArray[row, 0] = competitorStageList.FirstOrDefault(
                                x => x.Competitor_Id == comp.Id && x.Stage.StageOrder == currentCol + 1);
                        }
                    }
                }
            }


            return myArray;
        }

        public CompetitorStage[] FindMatchingCompetitorStageRecords(CompetitorStage[,] competitorStageArray, Competitor competitor)
        {
            var result = new List<CompetitorStage>();
            var rowLowerLimit = competitorStageArray.GetLowerBound(0);
            var rowUpperLimit = competitorStageArray.GetUpperBound(0);

            var colLowerLimit = competitorStageArray.GetLowerBound(1);
            var colUpperLimit = competitorStageArray.GetUpperBound(1);

            for (int row = rowLowerLimit; row < rowUpperLimit; row++)
            {
                for (int col = colLowerLimit; col < colUpperLimit; col++)
                {
                    if (competitorStageArray[row, col].Competitor.Id == competitor.Id)
                        result.Add(competitorStageArray[row, col]);

                    // you could do the search here...
                }
            }

            return result.ToArray();
        }

    }
}