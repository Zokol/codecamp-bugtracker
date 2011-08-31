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
    public class BugsController : Controller
    {
        private BugDBContext db = DataController.GetBugDb();

        //
        // GET: /Bugs/

        public ViewResult Index()
        {
            return View(db.Bugs.ToList());
        }

        //
        // GET: /Bugs/Details/5

        public ViewResult Details(int id)
        {
            BugEventList bel = new BugEventList
            {
                Bug = DataController.getBugByID(id),
                Events = DataController.getEventsOfBug(id)
            };
            //Bug bug = db.Bugs.Find(id);
            return View(bel);
        }

        //
        // GET: /Bugs/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Bugs/Create

        [HttpPost]
        public ActionResult Create(Bug bug)
        {
            if (ModelState.IsValid)
            {
                db.Bugs.Add(bug);
                db.SaveChanges();
                EventController e = new EventController();
                e.Create(bug.ID, HttpContext.User.Identity.Name, 1, "Bugi luotu");
                return RedirectToAction("Index");
            }

            return View(bug);
        }
        
        //
        // GET: /Bugs/Edit/5
 
        public ActionResult Edit(int id)
        {
            Bug bug = db.Bugs.Find(id);
            return View(bug);
        }

        //
        // POST: /Bugs/Edit/5

        [HttpPost]
        public ActionResult Edit(Bug bug)
        {
            if (ModelState.IsValid)
            {
                string comment;
                comment = "Tyhjä";
                int typeID = 0;
                Bug orig = DataController.getBugByID(bug.ID);
                EventController e = new EventController();
                if (!orig.Title.Equals(bug.Title))
                {
                    typeID = 1;
                    comment = "Edellinen nimi: " + orig.Title;
                    e.Create(bug.ID, HttpContext.User.Identity.Name, typeID, comment);
                }
                if (!orig.Description.Equals(bug.Description))
                {
                    typeID = 2;
                    comment = "Edellinen kuvaus: " + orig.Description;
                    e.Create(bug.ID, HttpContext.User.Identity.Name, typeID, comment);
                }
                if (!orig.Criticality.Equals(bug.Criticality))
                {
                    typeID = 4;
                    comment = "Edellinen kriittisyys: " + orig.Criticality;
                    e.Create(bug.ID, HttpContext.User.Identity.Name, typeID, comment);
                } 
                if (!orig.Priority.Equals(bug.Priority))
                {
                    typeID = 5;
                    comment = "Edellinen prioriteetti: " + orig.Priority;
                    e.Create(bug.ID, HttpContext.User.Identity.Name, typeID, comment);
                }
                if (!orig.Status.Equals(bug.Status))
                {
                    typeID = 6;
                    comment = "Edellinen status: " + orig.Status;
                    e.Create(bug.ID, HttpContext.User.Identity.Name, typeID, comment);
                }
                if (!orig.BugTypeID.Equals(bug.BugTypeID))
                {
                    typeID = 7;
                    comment = "Edellinen tyyppi: " + orig.BugTypeID;
                    e.Create(bug.ID, HttpContext.User.Identity.Name, typeID, comment);
                }
                db.Entry(bug).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bug);
        }

        //
        // GET: /Bugs/Delete/5
 
        public ActionResult Delete(int id)
        {
            Bug bug = db.Bugs.Find(id);
            return View(bug);
        }

        //
        // POST: /Bugs/Delete/5

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