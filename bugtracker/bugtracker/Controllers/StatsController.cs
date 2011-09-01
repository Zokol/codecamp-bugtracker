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

        public ActionResult Index(string username)
        {
            if (username==null) username = Membership.GetUser().UserName;

            UserStat us = new UserStat { 
                User = UserProfile.GetProfile(username),

                LogEvents = DataController.GetEventDb().Events
                .Where(u => u.User == username),

                Bugs = DataController.getBugsOfUser(username)

            };
            ViewBag.BugsReported = (int)((us.LogEvents
                .Where(l => l.EventType==1)).Count());

            ViewBag.BugsChanged = (int)((us.LogEvents
                .Where(l => l.EventType!=1 & l.EventType!=5)).Count());

            ViewBag.BugsClosed = (int)((us.LogEvents
                .Where(l => l.EventType == 7)).Count());

            return View(us);
        }

        

    }
}
