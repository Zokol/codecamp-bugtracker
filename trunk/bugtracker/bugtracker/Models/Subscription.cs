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
        public Bug SubscriptionBug { set; get; }
        public SType SubscriptionType { set; get; }

    }

    public class SubscriptionDBContext : DbContext
    {
        public DbSet<Subscription> Subscriptions { get; set; }
    }

    public enum SType : int { None, Creator, Tester, Interest }



}