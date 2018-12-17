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

        public ActionResult CompileAttendace()
        {
            ViewBag.LastCompiledDate = TASUtility.GetLastCompiledDate();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompileAttendace([Bind(Include = "Id,SelectDate,LastCompiled")] Compile compile)
        {
            if (ModelState.IsValid)
            {
                //string dateSelected;
                var selectedDate = compile.SelectDate.Date;
                var oDate = TASUtility.GetStringDateFormat(compile.SelectDate);
                var vDate = TASUtility.GetStringDateFormat(compile.SelectDate);

                //is the chosen date already compiled and finalized?
                if (!IsCompile(oDate))
                {
                    _context.RRcompile(oDate, vDate);
                    msg = "Compiled Successfully for " + compile.SelectDate.ToLongDateString();
                    ViewBag.Message = msg;
                }
                else
                {
                    msg = "You can not compile attendance for selected date as it is already Finalized";
                    ViewBag.Message = msg;
                }
                //ViewBag.Message = msg;
                //return RedirectToAction("Create");
                return PartialView("~/Views/_MessagePartialView.cshtml");
            }
            ViewBag.Message = "Error has occured. Check and try again.";
            return PartialView("~/Views/_MessagePartialView.cshtml");
            //return PartialView("~/Views/_MessagePartialView.cshtml");
        }
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
