using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using bugtracker.Models;

namespace bugtracker.Controllers
{
    public class UserBugListController : Controller
    {
        //
        // GET: /UserBugList/

        public ActionResult Index()
        {
            {
                List<UserBugList> ubl = new List<UserBugList>();
                foreach (MembershipUser user in Membership.GetAllUsers())
                {
                    if (user.UserName != null)
                    {
                        List<Bug> bugs = new List<Bug>();

                        bugs.AddRange(DataController.getBugsOfUser(user.UserName));
                        ubl.Add(new UserBugList
                        {
                            User = user.UserName,
                            Bugs = bugs
                        });
                    }
                }
                return View(ubl);
            }
        }

        //
        // GET: /BugEventList/Details/5
        public ViewResult Details()
        {
            List<UserBugList> ubl = new List<UserBugList>();
            foreach (MembershipUser user in Membership.GetAllUsers())
            {
                List<Bug> bugs = DataController.getBugsOfUser(user.UserName).ToList<Bug>();
                ubl.Add(new UserBugList
                {
                    User = user.UserName,
                    Bugs = bugs
                });
            }
            return View(ubl);
        }

    }
}
