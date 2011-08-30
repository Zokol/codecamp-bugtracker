using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bugtracker.Models;
using System.Data.Entity;

namespace bugtracker.Controllers
{
    public class DataController : Controller
    {

        public static BugDBContext bugdb = new BugDBContext();
        public static SubscriptionDBContext subsdb = new SubscriptionDBContext();
        public static EventDBContext eventdb = new EventDBContext();
        

        public ActionResult Index()
        {
            return View();
            
        }

        public static Bug getBugByID(int id)
        {
            return bugdb.Bugs
                .First(b => b.ID == id);
        }

        public static IEnumerable<Subscription> getSubscriptionsOfUser(string username)
        {
            return subsdb.Subscriptions
                .Where(s => s.Username == username);
        }

        public static IEnumerable<Subscription> getBugsOfUser(string username)
        {
            return null;
        }

        public static IEnumerable<LogEvent> getEventsOfBug(Bug b)
        {
            return eventdb.Events
                .Where(e => e.BugID == b.ID);
        }

        





    }
}
