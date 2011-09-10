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
		/* Get database from DataController */
        private SubscriptionDBContext db = DataController.GetSubscriptionDb();

        /* View to display all bugs that current user has subscribed */
        // GET: /Subscription/

        public ViewResult Index()
        {
            return View(DataController.getSubscribedBugsOfCurrentUser());
        }


		/* Returns details of bug of selected subscription */
        public ActionResult Details(int id)
        {
            BugEventList bel = new BugEventList
            {
                Bug = DataController.getBugByID(id),
                Events = DataController.getEventsOfBug(id)
            };
            return PartialView(bel);
        }

        /* Not in use */
        // GET: /Subscription/Create

        public ActionResult Create()
        {
            return View();
        }

        /* Creation of subscription, used when user creates bug or clicks subscription link in bug details. */
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

		/* Checks if user is already subscribed to selected bug. Uses previous method to create subscription. */
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

		/* Deletes subscription, finds subscription by bugid and username and deletes it from database. */
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


        /* Not in use */
        // GET: /Subscription/Edit/5

        public ActionResult Edit(int id)
        {
            Subscription subscription = db.Subscriptions.Find(id);
            return View(subscription);
        }

        /* Not in use */
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

        /* Not in use */
        // GET: /Subscription/Delete/5

        public ActionResult Delete(int id)
        {
            Subscription subscription = db.Subscriptions.Find(id);
            return View(subscription);
        }

        /* Not in use */
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