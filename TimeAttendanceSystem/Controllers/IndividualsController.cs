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
    public class IndividualsController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private UNISEntities _context = new UNISEntities();

        // GET: Individuals
        //public ActionResult Index()
        //{
        //    return View(db.Individuals.ToList());
        //}

        // GET: Individuals/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Individual individual = db.Individuals.Find(id);
        //    if (individual == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(individual);
        //}

        public ActionResult CompileIndividual()
        {
            ViewBag.Employees = new SelectList(_context.SP_GetEmployee_Names(0, ""), "id", "Employee_Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompileIndividual([Bind(Include = "Id,EmployeeID,FromDate,ToDate")] Individual individual)
        {
            if (ModelState.IsValid)
            {
                string msg = string.Empty;
                try
                {
                    var staffId = individual.EmployeeID;
                    var startDate = individual.FromDate;
                    var endDate = individual.ToDate;


                    if (startDate.Date != null && endDate.Date != null && startDate.Date > endDate.Date)
                    {
                        msg = "Please ensure that the End Date is greater than or equal to the Start Date.";
                        ViewBag.Message = msg;
                        return PartialView("~/Views/_MessagePartialView.cshtml");
                    }
                    var fromDate = TASUtility.GetStringDateFormat(startDate);
                    var toDate = TASUtility.GetStringDateFormat(endDate);

                    var result = _context.RRcompilebyIndividualempid(staffId, fromDate, toDate);

                    if (result.FirstOrDefault() == "Recompiled Successfully!!!!!!!!!!!!")
                    {
                        msg = "Recompiled Successfully!!!!!!!!!!!!";
                        ViewBag.Message = msg;
                    }
                    else
                    {
                        msg = "Something had happened. Please try again.";
                        ViewBag.Message = msg;
                    }


                    //db.Individuals.Add(individual);
                    //db.SaveChanges();
                    return PartialView("~/Views/_MessagePartialView.cshtml");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    throw;
                }

            }
            ViewBag.Message = "Error Occured. Check and try again.";

            return PartialView("~/Views/_MessagePartialView.cshtml");
        }

        // POST: Individuals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        

        // GET: Individuals/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Individual individual = db.Individuals.Find(id);
        //    if (individual == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(individual);
        //}

        // POST: Individuals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,EmployeeID,FromDate,ToDate")] Individual individual)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(individual).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(individual);
        //}

        // GET: Individuals/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Individual individual = db.Individuals.Find(id);
        //    if (individual == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(individual);
        //}

        // POST: Individuals/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Individual individual = db.Individuals.Find(id);
        //    db.Individuals.Remove(individual);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
