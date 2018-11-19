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
    public class EditEntriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EditEntries
        public ActionResult Index()
        {
            return View(db.EditEntries.ToList());
        }

        // GET: EditEntries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditEntry editEntry = db.EditEntries.Find(id);
            if (editEntry == null)
            {
                return HttpNotFound();
            }
            return View(editEntry);
        }

        // GET: EditEntries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EditEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeID,TerminalID,Mode,TimeHH,TimeMM,Date")] EditEntry editEntry)
        {
            if (ModelState.IsValid)
            {
                db.EditEntries.Add(editEntry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(editEntry);
        }

        // GET: EditEntries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditEntry editEntry = db.EditEntries.Find(id);
            if (editEntry == null)
            {
                return HttpNotFound();
            }
            return View(editEntry);
        }

        // POST: EditEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeID,TerminalID,Mode,TimeHH,TimeMM,Date")] EditEntry editEntry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(editEntry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editEntry);
        }

        // GET: EditEntries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditEntry editEntry = db.EditEntries.Find(id);
            if (editEntry == null)
            {
                return HttpNotFound();
            }
            return View(editEntry);
        }

        // POST: EditEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EditEntry editEntry = db.EditEntries.Find(id);
            db.EditEntries.Remove(editEntry);
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
