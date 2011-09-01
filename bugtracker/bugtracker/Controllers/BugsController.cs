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

            int sortBy = 0;
            if (sortColumn.Equals("ID"))
                sortBy = 0;
            else if (sortColumn.Equals("Priority"))
                sortBy = 1;
            else if (sortColumn.Equals("Status"))
                sortBy = 2;
            else if (sortColumn.Equals("Criticality"))
                sortBy = 3;
            else if (sortColumn.Equals("Description"))
                sortBy = 4;
            else if (sortColumn.Equals("Title"))
                sortBy = 5;
            else if (sortColumn.Equals("Type"))
                sortBy = 6;

            List<Bug> q = DataController.OrderListByColumn(DataController.GetBugDb().Bugs.ToList(), sortBy, asc);

            ViewBag.sortColumn = sortColumn;
            ViewBag.asc = asc.Value;

            return View(q);
        }

       
        public ActionResult Filter(string Filter1)
        {
            return View(DataController.GetBugDb());
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
                e.Create(bug.ID, HttpContext.User.Identity.Name, 0, "Bug created");
                SubscriptionController s = new SubscriptionController();
                s.CreateNewSubscription(HttpContext.User.Identity.Name, bug.ID);
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
                    typeID = 4;
                    comment = "Title: "+orig.Title + " ---> " + bug.Title;
                    e.Create(bug.ID, HttpContext.User.Identity.Name, typeID, comment);
                }
                if (!orig.Description.Equals(bug.Description))
                {
                    typeID = 4;
                    comment = "Description: " + orig.Description + " ---> " + bug.Description;
                    e.Create(bug.ID, HttpContext.User.Identity.Name, typeID, comment);
                }
                if (!orig.Criticality.Equals(bug.Criticality))
                {
                    typeID = 3;
                    comment = "Criticality: " + DataController.getCriticalityTypeString(orig.Criticality) + " ---> " + DataController.getCriticalityTypeString(bug.Criticality);
                    e.Create(bug.ID, HttpContext.User.Identity.Name, typeID, comment);
                } 
                if (!orig.PriorityID.Equals(bug.PriorityID))
                {
                    typeID = 2;
                    comment = "Priority: " + orig.PriorityID + " ---> " + bug.PriorityID;
                    e.Create(bug.ID, HttpContext.User.Identity.Name, typeID, comment);
                }
                if (!orig.StatusID.Equals(bug.StatusID))
                {
                    typeID = 1;
                    comment = "Status: " + DataController.getStatusTypeString(orig.StatusID) + " ---> " + DataController.getStatusTypeString(bug.StatusID);
                    if (bug.StatusID == (6 | 7))
                        typeID = 5;
                    e.Create(bug.ID, HttpContext.User.Identity.Name, typeID, comment);
                }
                if (!orig.BugTypeID.Equals(bug.BugTypeID))
                {
                    typeID = 4;
                    comment = "Type: " + DataController.getBugTypeString(orig.BugTypeID) + " ---> " + DataController.getBugTypeString(bug.BugTypeID);
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