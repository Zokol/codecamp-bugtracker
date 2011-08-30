using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace bugtracker.Models
{
    public class UserList
    {
        public MembershipUserCollection Users { get; set; }
        public IEnumerable<UserProfile> UserProfiles { set; get; }
    }
}