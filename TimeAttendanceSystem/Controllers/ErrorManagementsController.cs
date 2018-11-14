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
    public class ErrorManagementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ErrorManagements
        public ActionResult Index()
        {
            return View(db.ErrorManagements.ToList());
        }

        // GET: ErrorManagements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ErrorManagement errorManagement = db.ErrorManagements.Find(id);
            if (errorManagement == null)
            {
                return HttpNotFound();
            }
            return View(errorManagement);
        }

        // GET: ErrorManagements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ErrorManagements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Department")] ErrorManagement errorManagement)
        {
            if (ModelState.IsValid)
            {
                db.ErrorManagements.Add(errorManagement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(errorManagement);
        }

        // GET: ErrorManagements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ErrorManagement errorManagement = db.ErrorManagements.Find(id);
            if (errorManagement == null)
            {
                return HttpNotFound();
            }
            return View(errorManagement);
        }

        // POST: ErrorManagements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Department")] ErrorManagement errorManagement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(errorManagement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(errorManagement);
        }

        // GET: ErrorManagements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ErrorManagement errorManagement = db.ErrorManagements.Find(id);
            if (errorManagement == null)
            {
                return HttpNotFound();
            }
            return View(errorManagement);
        }

        // POST: ErrorManagements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ErrorManagement errorManagement = db.ErrorManagements.Find(id);
            db.ErrorManagements.Remove(errorManagement);
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
