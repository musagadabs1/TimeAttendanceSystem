using System;
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
    public class ManualEntriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ManualEntries
        public ActionResult Index()
        {
            return View(db.ManualEntries.ToList());
        }

        // GET: ManualEntries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManualEntry manualEntry = db.ManualEntries.Find(id);
            if (manualEntry == null)
            {
                return HttpNotFound();
            }
            return View(manualEntry);
        }

        // GET: ManualEntries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManualEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeID,Name,TerminalID,Mode,TimeHH,TimeMM,Date,Remarks")] ManualEntry manualEntry)
        {
            if (ModelState.IsValid)
            {
                db.ManualEntries.Add(manualEntry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(manualEntry);
        }

        // GET: ManualEntries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManualEntry manualEntry = db.ManualEntries.Find(id);
            if (manualEntry == null)
            {
                return HttpNotFound();
            }
            return View(manualEntry);
        }

        // POST: ManualEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeID,Name,TerminalID,Mode,TimeHH,TimeMM,Date,Remarks")] ManualEntry manualEntry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manualEntry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manualEntry);
        }

        // GET: ManualEntries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManualEntry manualEntry = db.ManualEntries.Find(id);
            if (manualEntry == null)
            {
                return HttpNotFound();
            }
            return View(manualEntry);
        }

        // POST: ManualEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ManualEntry manualEntry = db.ManualEntries.Find(id);
            db.ManualEntries.Remove(manualEntry);
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
