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
        public ViewResult Details()
        {
            List<LogEvent> events = DataController.getEventsOfBug(2).ToList<LogEvent>();
            BugEventList bel = new BugEventList
            { 
                Bug = DataController.getBugByID(2),
                Events = events
            };

            return View(bel);
        }

    }
}
