using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bugtracker.Models;

namespace bugtracker.Controllers
{
    public class HomeController : Controller
    {
	
		/* Controller to check if user is logged in. Displays user name or login link on _Layout.cshtml */
	
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var profile = UserProfile.GetProfile(User.Identity.Name);
                if (profile!=null) ViewBag.Message = "Welcome " + profile.FirstName + " " + profile.LastName + ".";
            }
            else
            {
                ViewBag.Message = "Please log on.";
            }

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
