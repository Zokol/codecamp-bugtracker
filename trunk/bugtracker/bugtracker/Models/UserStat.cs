using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bugtracker.Models
{
    public class UserStat
    {
        public UserProfile User { set; get; }
        public IEnumerable<LogEvent> LogEvents { set; get; }
        public IEnumerable<Bug> Bugs { set; get; }
    }
}