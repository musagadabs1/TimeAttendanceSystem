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
    public class ErrorCorrectionsController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        UNISEntities _context = new UNISEntities();

        public ActionResult AutoErrorCorrection()
        {
            ViewBag.LastCompiledDate = TASUtility.GetLastCompiledDate();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AutoErrorCorrection([Bind(Include = "Id,PreviousDateCompiled,SelectDateToCompile")] ErrorCorrection errorCorrection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var seletedDate = TASUtility.GetStringDateFormat(errorCorrection.SelectDateToCompile);

                    ViewBag.Message = "Error Corrected Successfully for " + errorCorrection.SelectDateToCompile.ToLongDateString();

                    return PartialView("~/Views/_MessagePartialView.cshtml");
                    //_context.err
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;

                    return PartialView("~/Views/_MessagePartialView.cshtml");
                }
            }

            return View(errorCorrection);
        }
        string msg = string.Empty;
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
