using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace bugtracker.Models
{
    public class UserBugList
    {
        public string User { get; set; }
        public IEnumerable<Bug> Bugs { set; get; }
    }
}