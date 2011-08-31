using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bugtracker.Models
{
    public class BugEventList
    {
        /*public IEnumerable<Bug> Bugs { get; set; }
        public int ID { get; set; }*/
        public Bug Bug { get; set; }
        public IEnumerable<LogEvent> Events { get; set; }
        
    }
}