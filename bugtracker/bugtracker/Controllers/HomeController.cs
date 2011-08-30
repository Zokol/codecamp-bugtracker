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
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var profile = UserProfile.GetProfile(User.Identity.Name);
                if (profile!=null) ViewBag.Message = "Tervetuloa " + profile.FirstName + " " + profile.LastName + ".";
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
