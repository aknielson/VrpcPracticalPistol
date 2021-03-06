﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class CompetitorsController : VrpcBaseController
    {

        // GET: Competitors
        public async Task<ActionResult> Index(int matchId = 0)
        {
            if (matchId == 0)
            {
                var matches = db.Matches.OrderByDescending(x => x.Date).ToList();
                return View("ChooseMatch", new ChooseMatchVM(matches,"Index","Competitors"));
            }
            CompetitorsViewModel competitorsVm = new CompetitorsViewModel();

            competitorsVm.Match = await db.Matches.FindAsync(matchId);
            competitorsVm.Competitors = await db.Competitors.Where(x => x.Match.Id.Equals(matchId)).ToListAsync();
            competitorsVm.MembersList = BuildMemberList();

            return View(competitorsVm);
        }

        // GET: Competitors/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competitor competitor = await db.Competitors.FindAsync(id);
            if (competitor == null)
            {
                return HttpNotFound();
            }
            return View(competitor);
        }

        // GET: Competitors/Create
        public async Task<ActionResult> Create(int matchId = 0)
        {
            Competitor model = new Competitor();
            model.Match_Id = (await db.Matches.FindAsync(matchId)).Id;
           
            ViewBag.MembersSelectList = BuildMemberList();
            ViewBag.CaliberSelectList = BuildCaliberList();

            return View(model);
        }

        // POST: Competitors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( Competitor competitor)
        {
            if (ModelState.IsValid)
            {
                db.Competitors.Add(competitor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index",new {matchId = competitor.Match_Id});
            }

            return View(competitor);
        }

        // GET: Competitors/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competitor competitor = await db.Competitors.FindAsync(id);
            if (competitor == null)
            {
                return HttpNotFound();
            }

            ViewBag.MembersSelectList = BuildMemberList();
            return View(competitor);
        }

        // POST: Competitors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Competitor competitor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(competitor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { matchId = competitor.Match_Id });
            }
            return View(competitor);
        }

        // GET: Competitors/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competitor competitor = await db.Competitors.FindAsync(id);
            if (competitor == null)
            {
                return HttpNotFound();
            }
            return View(competitor);
        }

        // POST: Competitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Competitor competitor = await db.Competitors.FindAsync(id);

            var competitorStageList = competitor.CompetitorStages.ToList();
            
            foreach (var competitorStage in competitorStageList)
            {
                var matchStageTimes = competitorStage.MatchStageTimes.ToList();
                foreach (var matchStageTime in matchStageTimes)
                {
                    db.MatchStageTimes.Remove(matchStageTime);
                }
            }

            foreach (var competitorStage in competitorStageList)
            {               
                db.CompetitorStages.Remove(competitorStage);
            }
            var matchId = competitor.Match_Id;
            db.Competitors.Remove(competitor);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", new { matchId = matchId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

      

    }
}