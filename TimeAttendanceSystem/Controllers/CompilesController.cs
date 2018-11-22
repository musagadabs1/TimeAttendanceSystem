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
    public class CompilesController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private UNISEntities _context = new UNISEntities();

        // GET: Compiles
        //public ActionResult Index()
        //{
        //    return View(db.Compiles.ToList());
        //}

        // GET: Compiles/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Compile compile = db.Compiles.Find(id);
        //    if (compile == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(compile);
        //}

        
        string msg = string.Empty;
        private bool IsCompile(string vDate)
        {
           var result = _context.SP_Is_Fianalized(vDate);
            if (result.FirstOrDefault() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        // GET: Compiles/Create
        public ActionResult Create()
        {
            ViewBag.LastCompiledDate = TASUtility.GetLastCompiledDate();
            return View();
        }

        // POST: Compiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SelectDate,LastCompiled")] Compile compile)
        {
            if (ModelState.IsValid)
            {
                //string dateSelected;
                var selectedDate = compile.SelectDate.Date;
                var oDate =TASUtility.GetStringDateFormat(compile.SelectDate);
                var vDate = TASUtility.GetStringDateFormat(compile.SelectDate);

                //is the chosen date already compiled and finalized?
                if (!IsCompile(oDate))
                {
                    _context.RRcompile(oDate, vDate);
                    msg = "Compiled Successfully";
                    ViewBag.Message = msg;
                }
                else
                {
                    msg = "You can not compile attendance for selected date as it is already Finalized";
                    ViewBag.Message = msg;
                }
                //ViewBag.Message = msg;
                //return RedirectToAction("Create");
                return PartialView("~/Views_MessagePartialView.cshtml");
            }
            ViewBag.Message = "Error has occured. Check and try again.";
            return PartialView("~/Views_MessagePartialView.cshtml");
        }

        // GET: Compiles/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Compile compile = db.Compiles.Find(id);
        //    if (compile == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(compile);
        //}

        // POST: Compiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,SelectDate,LastCompiled")] Compile compile)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(compile).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(compile);
        //}

        // GET: Compiles/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Compile compile = db.Compiles.Find(id);
        //    if (compile == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(compile);
        //}

        // POST: Compiles/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Compile compile = db.Compiles.Find(id);
        //    db.Compiles.Remove(compile);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        //db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
