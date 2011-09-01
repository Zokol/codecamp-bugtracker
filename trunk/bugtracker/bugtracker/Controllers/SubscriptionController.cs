using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bugtracker.Models;
using System.Web.Security;

namespace bugtracker.Controllers
{
    public class SubscriptionController : Controller
    {
        private SubscriptionDBContext db = DataController.GetSubscriptionDb();

        //
        // GET: /Subscription/

        public ViewResult Index()
        {
            return View(DataController.getSubscribedBugsOfCurrentUser());
        }


        public ActionResult Details(int id)
        {
            BugEventList bel = new BugEventList
            {
                Bug = DataController.getBugByID(id),
                Events = DataController.getEventsOfBug(id)
            };
            return PartialView(bel);
        }

        //
        // GET: /Subscription/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Subscription/Create

        [HttpPost]
        public ActionResult Create(Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                subscription.Username = HttpContext.User.Identity.Name;

                db.Subscriptions.Add(subscription);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subscription);
        }

        public ActionResult CreateNewSubscription(string Username, int SubscriptionBugID)
        {
            if (DataController.isUserSubscribedToBug(SubscriptionBugID, Username))
            {
                return RedirectToAction("Index");
            }

            else
            {
                Subscription newSub = new Subscription();
                newSub.Username = Username;
                newSub.SubscriptionBugID = SubscriptionBugID;
                newSub.SubsTypeID = 0;

                db.Subscriptions.Add(newSub);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

        }

        public ActionResult DeleteSubscription(string Username, int SubscriptionBugID)
        {

            foreach (Subscription sub in DataController.getSubscriptionsOfUser(HttpContext.User.Identity.Name))
            {
                if (sub.SubscriptionBugID == SubscriptionBugID)
                {
                    Subscription s = db.Subscriptions.Find(sub.SubscriptionID);
                    db.Subscriptions.Remove(s);
                }
                db.SaveChanges();
            }

            return RedirectToAction("Index");

        }


        //
        // GET: /Subscription/Edit/5

        public ActionResult Edit(int id)
        {
            Subscription subscription = db.Subscriptions.Find(id);
            return View(subscription);
        }

        //
        // POST: /Subscription/Edit/5

        [HttpPost]
        public ActionResult Edit(Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subscription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subscription);
        }

        //
        // GET: /Subscription/Delete/5

        public ActionResult Delete(int id)
        {
            Subscription subscription = db.Subscriptions.Find(id);
            return View(subscription);
        }

        //
        // POST: /Subscription/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Subscription subscription = db.Subscriptions.Find(id);
            db.Subscriptions.Remove(subscription);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}