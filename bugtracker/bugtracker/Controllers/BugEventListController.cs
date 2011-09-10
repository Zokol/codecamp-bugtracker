using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using bugtracker.Models;

namespace bugtracker.Controllers
{
	/* Controller to create list of events for certain bug */
    public class BugEventListController : Controller
    {
        /* Displays all bugs */
        // GET: /BugEventList/bugid

        public ActionResult Index()
        {
            return View(DataController.getAllBugs());
        }

        /* Lists all events for certain bug. Return contais list of events and bug id. After creating list, method checks if user has subscribed to this bug. */
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
