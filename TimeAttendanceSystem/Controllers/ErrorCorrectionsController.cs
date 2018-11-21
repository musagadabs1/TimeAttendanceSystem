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
    //[Authorize]
    public class ErrorCorrectionsController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ErrorCorrections
        //public ActionResult Index()
        //{
        //    return View(db.ErrorCorrections.ToList());
        //}

        // GET: ErrorCorrections/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ErrorCorrection errorCorrection = db.ErrorCorrections.Find(id);
        //    if (errorCorrection == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(errorCorrection);
        //}

        // GET: ErrorCorrections/Create
        public ActionResult Create()
        {
            ViewBag.LastCompiledDate = TASUtility.GetLastCompiledDate();
            return View();
        }

        // POST: ErrorCorrections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PreviousDateCompiled,SelectDateToCompile")] ErrorCorrection errorCorrection)
        {
            if (ModelState.IsValid)
            {
                //db.ErrorCorrections.Add(errorCorrection);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(errorCorrection);
        }

        // GET: ErrorCorrections/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ErrorCorrection errorCorrection = db.ErrorCorrections.Find(id);
        //    if (errorCorrection == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(errorCorrection);
        //}

        // POST: ErrorCorrections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,PreviousDateCompiled,SelectDateToCompile")] ErrorCorrection errorCorrection)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(errorCorrection).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(errorCorrection);
        //}

        // GET: ErrorCorrections/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ErrorCorrection errorCorrection = db.ErrorCorrections.Find(id);
        //    if (errorCorrection == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(errorCorrection);
        //}

        // POST: ErrorCorrections/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ErrorCorrection errorCorrection = db.ErrorCorrections.Find(id);
        //    db.ErrorCorrections.Remove(errorCorrection);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
