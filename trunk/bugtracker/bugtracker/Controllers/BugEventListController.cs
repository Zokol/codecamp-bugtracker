using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using bugtracker.Models;

namespace bugtracker.Controllers
{
    public class BugEventListController : Controller
    {
        //
        // GET: /BugEventList/bugid

        public ActionResult Index()
        {
            return View(DataController.getAllBugs());
        }

        //
        // GET: /BugEventList/Details/5
        public ViewResult Details(int id)
        {
            List<LogEvent> events = DataController.getEventsOfBug(id).ToList<LogEvent>();
            BugEventList bel = new BugEventList
            { 
                Bug = DataController.getBugByID(id),
                Events = events
            };

            if ( DataController.isUserSubscribedToBug(id, HttpContext.User.Identity.Name)) {
                ViewBag.alreadySubscribed = true;
            }
            else {
                ViewBag.alreadySubscribed = false;
            }
            return View(bel);
        }

    }
}
