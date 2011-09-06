using System;
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
        /** When possible, the datacontext generated at page start can be used ... for data that demands recent changes also while pages are open, use the GetXXXXDb() -methods instead. */
        public static BugDBContext bugdb = new BugDBContext();
        public static SubscriptionDBContext subsdb = new SubscriptionDBContext();
        public static EventDBContext eventdb = new EventDBContext();

        /** These methods return the dbcontext that contains also changes made since datacontroller creation (page start) */
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
        
        /* Returns a bug by bugid */
        public static Bug getBugByID(int id)
        {
            return GetBugDb().Bugs
                .First(b => b.ID == id);
        }

        /* Returns subscriptions of an user */
        public static IEnumerable<Subscription> getSubscriptionsOfUser(string username)
        {
            return subsdb.Subscriptions
                .Where(s => s.Username == username);
        }

        /* Returns changes (logevents) made to bugs since user previous logged off from system */
        public static IEnumerable<LogEvent> getChangedStatusListOfCurrentUserSinceLogout()
        {
            DateTime lastsignoff = UserProfile.GetProfile(Membership.GetUser().UserName).LastSignOff;
            List<LogEvent> events = GetEventDb().Events
                .Where(e => (e.CreateTime > lastsignoff & e.EventType == 1)).ToList<LogEvent>();
            return events;
        }

        /* Returns changes (logevents) made to bugs since user previously checked the on-time notifications */
        /** USAGE NOT IMPLEMENTED */
        public static IEnumerable<LogEvent> getChangedStatusListOfCurrentUserSinceCheck()
        {
            if (Membership.GetUser() == null) return new List<LogEvent>();
            DateTime lastcheck = UserProfile.GetProfile(Membership.GetUser().UserName).LastNotificationCheck;
            List<LogEvent> events = GetEventDb().Events
                .Where(e => (e.CreateTime > lastcheck & e.EventType == 1)).ToList<LogEvent>();
            return events;
        }

        /* Returns bugs current user has subscribed */
        public static IEnumerable<Bug> getSubscribedBugsOfCurrentUser()
        {
            if (Membership.GetUser() == null) return new List<Bug>();
            string user = Membership.GetUser().UserName;
            List<Subscription> subs = GetSubscriptionDb().Subscriptions
                .Where(sub => sub.Username == user).ToList<Subscription>();

            List<Bug> result = new List<Bug>();
            foreach (Subscription s in subs)
            {
                result.Add(getBugByID(s.SubscriptionBugID));
            }

            return result;
        }

        /* Returns bugs user has participaged on working on */
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

        }

        /* Returns all bugs */
        public static IEnumerable<Bug> getAllBugs()
        {
            if (Membership.GetUser() == null) return new List<Bug>();
            return GetBugDb().Bugs;
        }

        /* Returns Bug Status types and ids of status types */
        public static List<StatusType> getStatusTypes()
        {
            return new List<StatusType>
            {
            new StatusType { ID = 0, Name = "Reported" },
            new StatusType { ID = 1, Name = "Confirmed" },
            new StatusType { ID = 2, Name = "Rejected" },
            new StatusType { ID = 3, Name = "Prioritized" },
            new StatusType { ID = 4, Name = "Reserved" },
            new StatusType { ID = 5, Name = "Fixed" },
            new StatusType { ID = 6, Name = "Closed - Resolved" },
            new StatusType { ID = 7, Name = "Closed - Unresolved" }
            };
        }

        /* Returns Bug types and ids of those types */
        public static List<BugType> getBugTypes()
        {
            return new List<BugType>
            {
            new BugType { ID = 0, Name = "Database" },
            new BugType { ID = 1, Name = "UI" },
            new BugType { ID = 2, Name = "Security" },
            new BugType { ID = 3, Name = "Core" },
            new BugType { ID = 4, Name = "Other" }
            };
        }

        /* Returns Bug Criticality types and ids of those types */
        public static List<CriticalityType> getCriticalityTypes()
        {
            return new List<CriticalityType>
            {
            new CriticalityType { ID = 0, Name = "Critical" },
            new CriticalityType { ID = 1, Name = "Major" },
            new CriticalityType { ID = 2, Name = "Moderate" },
            new CriticalityType { ID = 3, Name = "Minor" },
            new CriticalityType { ID = 4, Name = "None" }
            };
        }

        /* Returns Logevent types and ids of those types */
        public static List<LogEventType> getLogEventTypes()
        {
            return new List<LogEventType>
            {
            new LogEventType { ID = 0, Name = "Creation" },
            new LogEventType { ID = 1, Name = "Status change" },
            new LogEventType { ID = 2, Name = "Priority change" },
            new LogEventType { ID = 3, Name = "Criticality change" },
            new LogEventType { ID = 4, Name = "Added info" },
            new LogEventType { ID = 5, Name = "End" }
            };
        }

        /* Returns bugs of priority */
        public static IEnumerable<Bug> getBugsOfPriority(int priority)
        {
            if (Membership.GetUser() == null) return new List<Bug>();
            return GetBugDb().Bugs
                .Where(b => b.PriorityID == priority);
        }

        /* Returns bugs of equal or higher criticality */
        public static IEnumerable<Bug> getBugsofCriticality(int criticality)
        {
            if (Membership.GetUser() == null) return new List<Bug>();
            return GetBugDb().Bugs
                .Where(b => b.Criticality >= criticality);

        }

        /* Returns bugs of status */
        public static IEnumerable<Bug> getBugsofStatus(int status)
        {
            if (Membership.GetUser() == null) return new List<Bug>();
            return GetBugDb().Bugs
                .Where(b => b.StatusID == status);
        }

        /* Returns bug type string based on type ID, see getBugTypes() */
        public static String getBugTypeString(int bugtype)
        {
            return getBugTypes().First(b => b.ID == bugtype).Name;
        }

        /* Returns status type string based on type ID, see getStatusTypes() */
        public static String getStatusTypeString(int status)
        {
            return getStatusTypes().First(b => b.ID == status).Name;
        }

        /* Returns criticality type string based on type ID, see getCriticalityTypes() */
        public static String getCriticalityTypeString(int criticality)
        {
            return getCriticalityTypes().First(b => b.ID == criticality).Name;
        }

        /* Returns logevent type string based on type ID, see getLogEventTypes() */
        public static String getLogEventTypeString(int eventtype)
        {
            return getLogEventTypes().First(b => b.ID == eventtype).Name;
        }

        /* Orders the list given as argument, by the column and returns the ordered list */
        /** USAGE OF CHAINING NOT IMPLEMENTED */
        public static List<Bug> OrderListByColumn(List<Bug> bugs, int column, bool? ascending)
        {
            bool asc = ascending ?? false;
            List<Bug> result;
            switch (column)
            {
                default: { result = bugs.OrderBy(b => b.ID).ToList<Bug>(); break; }
                case 1: { result = bugs.OrderBy(b => b.PriorityID).ToList<Bug>(); break; }
                case 2: { result = bugs.OrderBy(b => b.StatusID).ToList<Bug>(); break; }
                case 3: { result = bugs.OrderBy(b => b.Criticality).ToList<Bug>(); break; }
                case 4: { result = bugs.OrderBy(b => b.Description).ToList<Bug>(); break; }
                case 5: { result = bugs.OrderBy(b => b.Title).ToList<Bug>(); break; }
                case 6: { result = bugs.OrderBy(b => b.BugTypeID).ToList<Bug>(); break; }
            }
            if (!asc) result.Reverse();
            return result;
        }

        /* Returns events of a bug */
        public static IEnumerable<LogEvent> getEventsOfBug(int bugid)
        {
            return eventdb.Events
                .Where(e => e.BugID == bugid);
        }

        /* Returns subscription by ID */
        public static IEnumerable<Subscription> getSubByID(int id) 
        {
            return subsdb.Subscriptions
                .Where(s => s.SubscriptionID == id);
        }

        /* Returns bug by subscription ID */
        public static int getBugBySubID(int Sid) {
            return subsdb.Subscriptions.First(a => a.SubscriptionID == Sid).SubscriptionBugID;
        }

        /* Returns true if user of username is subscribed to the bug of bugid */
        public static bool isUserSubscribedToBug(int bugId, string username)
        {
            foreach (Subscription sub in DataController.getSubscriptionsOfUser(username))
            {
                if (sub.SubscriptionBugID == bugId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
