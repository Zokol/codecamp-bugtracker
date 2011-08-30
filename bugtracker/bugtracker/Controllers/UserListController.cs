using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bugtracker.Models;
using System.Web.Security;
using System.Web.ApplicationServices;

namespace MvcApplication2.Controllers
{
    public class UserListController : Controller
    {
        //
        // GET: /UserList/

        public ActionResult Index()
        {
            MembershipUserCollection Users = Membership.GetAllUsers();
            List<UserProfile> UserProfiles = new List<UserProfile>();
            foreach (MembershipUser mu in Users)
            {
                UserProfiles.Add(UserProfile.GetProfile(mu.UserName));
            }
            
            UserList list = new UserList
            {
                Users = Users,
                UserProfiles = UserProfiles
            };

            return View(list);
        }

    }
}
