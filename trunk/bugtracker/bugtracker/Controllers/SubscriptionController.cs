using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bugtracker.Models;

namespace bugtracker.Controllers
{ 
    public class SubscriptionController : Controller
    {
        private SubscriptionDBContext db = DataController.GetSubscriptionDb();

        //
        // GET: /Subscription/

        public ViewResult Index()
        {
            return View(db.Subscriptions.ToList());
        }

        //
        // GET: /Subscription/Details/5

        public ViewResult Details(int id)
        {
            Subscription subscription = db.Subscriptions.Find(id);
            return View(subscription);
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