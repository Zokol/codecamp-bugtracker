﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bugtracker.Models;
using System.Data.Entity;
using System.Web.Security;

namespace bugtracker.Controllers
{
    public static class DataController
    {

        public static BugDBContext bugdb = new BugDBContext();
        public static SubscriptionDBContext subsdb = new SubscriptionDBContext();
        public static EventDBContext eventdb = new EventDBContext();

        public static BugDBContext GetBugDb()
        {
            return new BugDBContext();
        }

        public static SubscriptionDBContext GetSubscriptionDb()
        {
            return new SubscriptionDBContext();
        }

        public static EventDBContext GetEventDb()
        {
            return new EventDBContext();
        }
        
        public static Bug getBugByID(int id) /* Updated */
        {
            return GetBugDb().Bugs
                .First(b => b.ID == id);
        }

        public static IEnumerable<Subscription> getSubscriptionsOfUser(string username)
        {
            return subsdb.Subscriptions
                .Where(s => s.Username == username);
        }

        public static IEnumerable<LogEvent> getRecentChangedStatusListOfCurrentUser()
        {
            DateTime lastsignoff = UserProfile.GetProfile(Membership.GetUser().UserName).LastSignOff;
            List<LogEvent> events = GetEventDb().Events
                .Where(e => (e.CreateTime > lastsignoff & e.EventType == 6)).ToList<LogEvent>();
            return events;

            /* To get the IEnumerable<Bug> of those bugs whose status has changed:
             * public static IEnumerable<Bug> getRecentChangedBugListOfCurrentUser()
             * {
             * like it is now except;
             * return from e in events
                   join b in getAllBugs()
                   on e.BugID equals b.ID
                   select b; 
             *}
           */

        }

        public static IEnumerable<Bug> getBugsOfUser(string username)
        {
            List<LogEvent> events = GetEventDb().Events.Where(u => u.User == username).ToList<LogEvent>();
            List<Bug> bugs = getAllBugs().ToList<Bug>();
            List<Bug> result = new List<Bug>();
            foreach (LogEvent b in events)
            {
                foreach (Bug e in bugs)
                {
                    if (b.BugID == e.ID) { result.Add(e); }
                }
            }



            return result.AsEnumerable<Bug>();

            //select new {c.Name, o.OrderNumber};
            
           /* eventdb.Events
                .Where(u => u.User == username)
                .Join(*/
        }

        public static IEnumerable<Bug> getAllBugs()
        {
            return bugdb.Bugs;
        }

        public static IEnumerable<LogEvent> getEventsOfBug(int bugid)
        {
            return eventdb.Events
                .Where(e => e.BugID == bugid);
        }

        public static IEnumerable<Subscription> getSubByID(int id) 
        {
            return subsdb.Subscriptions
                .Where(s => s.SubscriptionID == id);
        }

        public static int getBugBySubID(int Sid) {
            return subsdb.Subscriptions.First(a => a.SubscriptionID == Sid).SubscriptionBugID;
        }
    }
}