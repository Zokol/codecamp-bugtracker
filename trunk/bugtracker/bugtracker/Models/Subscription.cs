using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace bugtracker.Models
{
    public class Subscription
    {
        public String Username { set; get; }
        
        public int SubscriptionID { set; get; }
        public int SubscriptionBugID { set; get; }
        public int SubsTypeID { set; get; }

    }

    public class SubscriptionDBContext : DbContext
    {
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionType> SubscriptionTypes { get; set; }

    }

    public class SubscriptionType
    {
        public int ID { get; set; }
        public String Name { get; set; }
    }


}