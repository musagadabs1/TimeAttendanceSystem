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
    public class FinalizesController : Controller
    {
        private UNISEntities _context = new UNISEntities();

        // GET: Finalizes
        //public ActionResult Index()
        //{
        //    return View(db.Finalizes.ToList());
        //}

        // GET: Finalizes/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Finalize finalize = db.Finalizes.Find(id);
        //    if (finalize == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(finalize);
        //}

        // GET: Finalizes/Create
        public ActionResult Create()
        {
            ViewBag.Finalize = new SelectList(_context.GetAllFinalizedDates(), "FinalizeDate");
            var variance = new List<SelectListItem>
            {
                new SelectListItem {Value = "+", Text = "+" },
                new SelectListItem{Value = "-", Text = "-"}
            };
            ViewBag.Variance = variance;
            return View();
        }
        private List<string> GetAllFinalizedAttendance()
        {
            try
            {
                List<string> finalizes = new List<string>();
                //var atts=(from a in _con)
                finalizes = _context.GetAllFinalizedDates().ToList(); ;
                //foreach (var item in atts)
                //{
                //    finalizes.Add(item.FirstOrDefault());
                //}
                return finalizes;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // POST: Finalizes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,VarianceSetting,Hours,SendEmail,AttendanceDate")] Finalize finalize)
        {
            if (ModelState.IsValid)
            {
                //db.Finalizes.Add(finalize);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(finalize);
        }

        // GET: Finalizes/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Finalize finalize = db.Finalizes.Find(id);
        //    if (finalize == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(finalize);
        //}

        // POST: Finalizes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,VarianceSetting,Hours,SendEmail,AttendanceDate")] Finalize finalize)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(finalize).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(finalize);
        //}

        // GET: Finalizes/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Finalize finalize = db.Finalizes.Find(id);
        //    if (finalize == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(finalize);
        //}

        // POST: Finalizes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Finalize finalize = db.Finalizes.Find(id);
        //    db.Finalizes.Remove(finalize);
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
