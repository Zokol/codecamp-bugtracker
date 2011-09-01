﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bugtracker.Models;
using System.Linq.Expressions;
using System.Web.Security;

namespace bugtracker.Controllers
{ 
    public class BugsController : Controller
    {
        private BugDBContext db = DataController.GetBugDb();

        //
        // GET: /Bugs/

        /*public ViewResult Index()
        {
            return View(DataController.GetBugDb().Bugs.ToList());
        }*/

        public ActionResult Index(string sortColumn, bool? asc)
        {
            asc = asc ?? true;
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "ID";

            IEnumerable<Bug> q = null;
            if (Membership.GetUser() == null) q = new List<Bug>().AsEnumerable<Bug>();
            else
            {

                if (sortColumn.Equals("ID") && asc.Value)
                    q = DataController.GetBugDb().Bugs.OrderBy(b => b.ID);
                else if (sortColumn.Equals("ID") && !asc.Value)
                    q = DataController.GetBugDb().Bugs.OrderByDescending(b => b.ID);
                else if (sortColumn.Equals("Criticality") && asc.Value)
                    q = DataController.GetBugDb().Bugs.OrderBy(b => b.Criticality);
                else if (sortColumn.Equals("Criticality") && !asc.Value)
                    q = DataController.GetBugDb().Bugs.OrderByDescending(b => b.Criticality);
                else if (sortColumn.Equals("Priority") && asc.Value)
                    q = DataController.GetBugDb().Bugs.OrderBy(b => b.PriorityID);
                else if (sortColumn.Equals("Priority") && !asc.Value)
                    q = DataController.GetBugDb().Bugs.OrderByDescending(b => b.PriorityID);
                else if (sortColumn.Equals("Status") && asc.Value)
                    q = DataController.GetBugDb().Bugs.OrderBy(b => b.StatusID);
                else if (sortColumn.Equals("Status") && !asc.Value)
                    q = DataController.GetBugDb().Bugs.OrderByDescending(b => b.StatusID);

                else
                    q = DataController.GetBugDb().Bugs.OrderBy(b => b.ID);
            }
            ViewBag.sortColumn = sortColumn;
            ViewBag.asc = asc.Value;

            return View(q.ToList());
        }

        //
        // GET: /Bugs/Details/5

        public ActionResult Details(int id)
        {
            BugEventList bel = new BugEventList
            {
                Bug = DataController.getBugByID(id),
                Events = DataController.getEventsOfBug(id)
            };

            Bug bug = DataController.GetBugDb().Bugs.Find(id);


            if (DataController.isUserSubscribedToBug(id, HttpContext.User.Identity.Name))
            {
                ViewBag.alreadySubscribed = true;
            }
            else
            {
                ViewBag.alreadySubscribed = false;
            }

            return PartialView(bel);
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
            Bug bug = DataController.GetBugDb().Bugs.Find(id);
            return PartialView(bug);
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
                    comment = "Title: "+orig.Title + "--->" + bug.Title;
                    e.Create(bug.ID, HttpContext.User.Identity.Name, typeID, comment);
                }
                if (!orig.Description.Equals(bug.Description))
                {
                    typeID = 2;
                    comment = "Description: " + orig.Description + "--->" + bug.Description;
                    e.Create(bug.ID, HttpContext.User.Identity.Name, typeID, comment);
                }
                if (!orig.Criticality.Equals(bug.Criticality))
                {
                    typeID = 4;
                    comment = "Criticality: " + orig.Criticality + "--->" + bug.Criticality;
                    e.Create(bug.ID, HttpContext.User.Identity.Name, typeID, comment);
                } 
                if (!orig.PriorityID.Equals(bug.PriorityID))
                {
                    typeID = 5;
                    comment = "Priority: " + orig.PriorityID + "--->" + bug.PriorityID;
                    e.Create(bug.ID, HttpContext.User.Identity.Name, typeID, comment);
                }
                if (!orig.StatusID.Equals(bug.StatusID))
                {
                    typeID = 6;
                    comment = "Status: " + orig.StatusID + "--->" + bug.StatusID;
                    e.Create(bug.ID, HttpContext.User.Identity.Name, typeID, comment);
                }
                if (!orig.BugTypeID.Equals(bug.BugTypeID))
                {
                    typeID = 7;
                    comment = "Type: " + orig.BugTypeID + "--->" + bug.BugTypeID;
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
            Bug bug = DataController.GetBugDb().Bugs.Find(id);
            return PartialView(bug);
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