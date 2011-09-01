using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace bugtracker.Models
{
    public class LogEvent
    {
        [Key, Column(Order = 0)]
        public DateTime CreateTime { get; set; }
        [Key, Column(Order = 1)]
        public int BugID { get; set; }
        [Key, Column(Order = 2)]
        public string User { get; set; }
        public int EventType { set; get; }
        public string Comment { get; set; }
    }

    public class EventDBContext : DbContext
    {
        public DbSet<LogEvent> Events { get; set; }
    }

    public class LogEventType
    {
        public int ID { get; set; }
        public String Name { get; set; }
    }
}