using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BugTracker.Models
{
    public class Bug
    {
        public int ID { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int criticality { get; set; }
        public int status { get; set; }
        public string email { get; set; }


        public class BugDBContext : DbContext
        {
            public DbSet<Bug> Bugs { get; set; }
        }
    }
}