using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;

namespace BugTracker.Controllers
{ 
    public class BugController : Controller
    {
        private BugDBContext db = new BugDBContext();

        //
        // GET: /Bug/

        public ViewResult Index()
        {
            return View(db.Bugs.ToList());
        }

        //
        // GET: /Bug/Details/5

        public ViewResult Details(int id)
        {
            Bug bug = db.Bugs.Find(id);
            return View(bug);
        }

        //
        // GET: /Bug/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Bug/Create

        [HttpPost]
        public ActionResult Create(Bug bug)
        {
            if (ModelState.IsValid)
            {
                db.Bugs.Add(bug);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(bug);
        }
        
        //
        // GET: /Bug/Edit/5
 
        public ActionResult Edit(int id)
        {
            Bug bug = db.Bugs.Find(id);
            return View(bug);
        }

        //
        // POST: /Bug/Edit/5

        [HttpPost]
        public ActionResult Edit(Bug bug)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bug).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bug);
        }

        //
        // GET: /Bug/Delete/5
 
        public ActionResult Delete(int id)
        {
            Bug bug = db.Bugs.Find(id);
            return View(bug);
        }

        //
        // POST: /Bug/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Bug bug = db.Bugs.Find(id);
            db.Bugs.Remove(bug);
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