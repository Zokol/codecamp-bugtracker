﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bugtracker.Models;

namespace bugtracker.Controllers
{ 
    public class EventController : Controller
    {
        private EventDBContext db = DataController.GetEventDb();

        //
        // GET: /Event/

        public ViewResult Index()
        {
            return View(DataController.getChangedStatusListOfCurrentUserSinceCheck());
        }

        //
        // GET: /Event/Details/5

        public ViewResult Details(DateTime id)
        {
            LogEvent logevent = db.Events.Find(id);
            return View(logevent);
        }

        //
        // GET: /Event/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Event/Create

        [HttpPost]
        public ActionResult Create(LogEvent logevent)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(logevent);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(logevent);
        }


        public void Create(int bugID, string username, int typeID, string comment)
        {
            LogEvent logevent = new LogEvent();
            logevent.BugID = bugID;
            logevent.User = username;
            logevent.CreateTime = DateTime.Now;
            logevent.EventType = typeID;
            logevent.Comment = comment;

            db.Events.Add(logevent);
            db.SaveChanges();

        }
        
        
        //
        // GET: /Event/Edit/5
 
        public ActionResult Edit(DateTime id)
        {
            LogEvent logevent = db.Events.Find(id);
            return View(logevent);
        }

        //
        // POST: /Event/Edit/5

        [HttpPost]
        public ActionResult Edit(LogEvent logevent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(logevent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(logevent);
        }

        //
        // GET: /Event/Delete/5
 
        public ActionResult Delete(DateTime id)
        {
            LogEvent logevent = db.Events.Find(id);
            return View(logevent);
        }

        //
        // POST: /Event/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(DateTime id)
        {            
            LogEvent logevent = db.Events.Find(id);
            db.Events.Remove(logevent);
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