using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bugtracker.Models;
using System.Web.Security;
using System.Web.ApplicationServices;

namespace bugtracker.Controllers
{
    public class UserListController : Controller
    {
        //
        // GET: /UserList/

        public static List<UserProfile> GetUserProfiles()
        {
            List<UserProfile> UserProfiles = new List<UserProfile>();
            foreach (MembershipUser mu in Membership.GetAllUsers())
            {
                UserProfiles.Add(UserProfile.GetProfile(mu.UserName));
            }
            return UserProfiles;
            
        }

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
