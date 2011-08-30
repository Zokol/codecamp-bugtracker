using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace bugtracker.Models
{
    public class Bug
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Criticality { get; set; }
        public int Priority { get; set; }
        public int Status { get; set; }
        public int BugTypeID { get; set; }
    }
    public class BugDBContext : DbContext
    {
        public DbSet<Bug> Bugs { get; set; }
        public DbSet<BugType> BugTypes { get; set; }
    }

    public class BugType
    {
        public int ID { get; set; }
        public String Name { get; set; }
    }

    /*public class WrapperModel1 : Bug, IEnumerable<Bug>, IList<Bug> { }
    public class WrapperModel2 : LogEvent, IEnumerable<Bug>, IList<Bug> { }

    public class WrapperLayout
    {
        WrapperModel1 wm1 = new WrapperModel1();
        WrapperModel2 wm2 = new WrapperModel2();

        public WrapperLayout()
        {

        }
    }*/



}