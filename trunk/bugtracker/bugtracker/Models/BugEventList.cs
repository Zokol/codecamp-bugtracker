using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bugtracker.Models
{
    public class BugEventList
    {
        public Bug Bug { get; set; }
        public IEnumerable<LogEvent> Events { get; set; }


    }
}