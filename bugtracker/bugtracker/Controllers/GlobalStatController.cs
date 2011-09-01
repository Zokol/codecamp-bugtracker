using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bugtracker.Models;

namespace bugtracker.Controllers
{
    public class GlobalStatController : Controller
    {
        //
        // GET: /GlobalStat/

        public ActionResult Index()
        {
            List<UserStat> userstats = new List<UserStat>();
            foreach (UserProfile up in UserListController.GetUserProfiles())
            {
                UserStat us = new UserStat
                    {
                        User = up,

                        LogEvents = DataController.GetEventDb().Events
                        .Where(u => u.User == up.UserName),

                        Bugs = DataController.getBugsOfUser(up.UserName)
                    };
            }
            return View(userstats);
        }

    }
}
