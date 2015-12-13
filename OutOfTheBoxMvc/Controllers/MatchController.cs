﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DataModel;
using DomainClasses;
using OutOfTheBoxMvc.Models;

namespace OutOfTheBoxMvc.Controllers
{
    public class MatchController : Controller
    {
        //todo: IOC
        private VrpcPracticalPistolContext db = new VrpcPracticalPistolContext();
        // GET: Match
        [HttpGet]
        public ActionResult Create()
        {
            var model = new MatchViewModel();
            model.Match = new Match();
            model.Match.Date = DateTime.Now.Date;

            model.Stages = PopulateStages(model.Match);

            model.MembersSelectList = BuildMemberList(); 
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(MatchViewModel matchViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Matches.Add(matchViewModel.Match);
                foreach (Stage stage in matchViewModel.Stages)
                {
                    stage.Match = matchViewModel.Match;
                    db.Stages.Add(stage);
                }
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            return View(matchViewModel);

        }

        public ActionResult Edit(int id)
        {
            var model = new MatchViewModel();
            var currentMatch = db.Matches.FirstOrDefault(x => x.Id.Equals(id));
            if (currentMatch != null)
            {
                model.Match = currentMatch;
                model.Stages = currentMatch.Stages.ToList();
                model.MembersSelectList = BuildMemberList();

            }

            return View(model);
        }


        // GET: Matches/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = await db.Matches.FindAsync(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Match match = await db.Matches.FindAsync(id);
            foreach (var stage in match.Stages.ToList())
            {
                db.Stages.Remove(stage);
            }
            db.Matches.Remove(match);
            await db.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }

      

        private List<SelectListItem> BuildMemberList()
        {
            var memberList = new List<SelectListItem>();
            memberList.Add(new SelectListItem { Text = "Member List" });

            foreach (var member in db.Members.OrderBy(x => x.FirstName))
            {
                memberList.Add(new SelectListItem
                {
                    Value = member.Id.ToString(),
                    Text = member.FirstName + ' ' + member.LastName
                });
            }
            return memberList;
        }

        private List<Stage> PopulateStages(Match match)
        {
            var stages = new List<Stage>
            {
                new Stage
                {
                    StageName = "Stage One",
                    IncludeInCombinedScore = true,
                    IsVirginia = false,
                    NumberOfStrings = 1,
                    Match = match
                },
                new Stage
                {
                    StageName = "Stage Two",
                    IncludeInCombinedScore = true,
                    IsVirginia = false,
                    NumberOfStrings = 1,
                    Match = match
                },
                new Stage
                {
                    StageName = "Stage Three",
                    IncludeInCombinedScore = true,
                    IsVirginia = false,
                    NumberOfStrings = 1,
                    Match = match
                },
                new Stage
                {
                    StageName = "Stage Four",
                    IncludeInCombinedScore = false,
                    IsVirginia = false,
                    NumberOfStrings = 1,
                    Match = match
                }
            };



            return stages;
        }
    }
}