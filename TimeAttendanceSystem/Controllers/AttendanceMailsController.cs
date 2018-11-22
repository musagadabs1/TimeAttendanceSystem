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
    [Authorize]
    public class AttendanceMailsController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        UNISEntities _context = new UNISEntities();

        // GET: AttendanceMails
        //public ActionResult Index()
        //{
        //    return View(db.AttendanceMails.ToList());
        //}

        // GET: AttendanceMails/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AttendanceMail attendanceMail = db.AttendanceMails.Find(id);
        //    if (attendanceMail == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(attendanceMail);
        //}

        // GET: AttendanceMails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AttendanceMails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SelectDate")] AttendanceMail attendanceMail)
        {
            if (ModelState.IsValid)
            {
                //db.AttendanceMails.Add(attendanceMail);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(attendanceMail);
        }

        // GET: AttendanceMails/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AttendanceMail attendanceMail = db.AttendanceMails.Find(id);
        //    if (attendanceMail == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(attendanceMail);
        //}

        // POST: AttendanceMails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,SelectDate")] AttendanceMail attendanceMail)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(attendanceMail).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(attendanceMail);
        //}

        // GET: AttendanceMails/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AttendanceMail attendanceMail = db.AttendanceMails.Find(id);
        //    if (attendanceMail == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(attendanceMail);
        //}

        // POST: AttendanceMails/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    AttendanceMail attendanceMail = db.AttendanceMails.Find(id);
        //    db.AttendanceMails.Remove(attendanceMail);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
