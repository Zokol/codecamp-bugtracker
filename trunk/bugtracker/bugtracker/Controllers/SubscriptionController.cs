using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bugtracker.Models;

namespace bugtracker.Controllers
{ 
    public class SubscriptionController : Controller
    {
        private SubscriptionDBContext db = DataController.GetSubscriptionDb();

        //
        // GET: /Subscription/

        public ViewResult Index()
        {
            return View(DataController.getRecentChangedStatusListOfCurrentUser());
        }

       

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}