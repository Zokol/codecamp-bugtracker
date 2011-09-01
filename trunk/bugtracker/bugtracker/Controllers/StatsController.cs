using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bugtracker.Models;
using System.Web.Security;

namespace bugtracker.Controllers
{
    public class StatsController : Controller
    {
        //
        // GET: /Stats/

        public ActionResult Index()
        {
            string username = Membership.GetUser().UserName;
            UserStat us = new UserStat { 
                User = UserProfile.GetProfile(username),
                LogEvents = DataController.GetEventDb().Events
                .Where(u => u.User == Membership.GetUser().UserName),
                Bugs = DataController.getBugsOfUser(username)

            };

            return View(us);
        }

        

    }
}
