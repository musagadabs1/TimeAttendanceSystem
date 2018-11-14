﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TimeAttendanceSystem.Models;

namespace TimeAttendanceSystem.Controllers
{
    public class RollBacksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RollBacks
        public ActionResult Index()
        {
            return View(db.RollBacks.ToList());
        }

        // GET: RollBacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RollBack rollBack = db.RollBacks.Find(id);
            if (rollBack == null)
            {
                return HttpNotFound();
            }
            return View(rollBack);
        }

        // GET: RollBacks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RollBacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LastCompiled,SelectDate")] RollBack rollBack)
        {
            if (ModelState.IsValid)
            {
                db.RollBacks.Add(rollBack);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rollBack);
        }

        // GET: RollBacks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RollBack rollBack = db.RollBacks.Find(id);
            if (rollBack == null)
            {
                return HttpNotFound();
            }
            return View(rollBack);
        }

        // POST: RollBacks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LastCompiled,SelectDate")] RollBack rollBack)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rollBack).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rollBack);
        }

        // GET: RollBacks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RollBack rollBack = db.RollBacks.Find(id);
            if (rollBack == null)
            {
                return HttpNotFound();
            }
            return View(rollBack);
        }

        // POST: RollBacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RollBack rollBack = db.RollBacks.Find(id);
            db.RollBacks.Remove(rollBack);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
