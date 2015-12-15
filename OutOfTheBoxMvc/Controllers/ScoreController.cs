using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DataModel;
using OutOfTheBoxMvc.Models;

namespace OutOfTheBoxMvc.Controllers
{
    public class ScoreController : VrpcBaseController
    {
        // GET: Competitors
        public  ActionResult Index(int matchId = 0)
        {
            if (matchId == 0)
            {
                var model = db.Matches.ToList();
                return View("ChooseMatch", model);
            }

            return RedirectToAction("Index", "Home");

            // return View();
        }
    }
}