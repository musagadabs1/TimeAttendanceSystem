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
    public class FinalizesController : Controller
    {
        private UNISEntities _context = new UNISEntities();
        [HttpPost]
        public ActionResult Show(Finalize finalize)
        {
            try
            {
                //string date=TASUtility.GetStringDateFormat(finalize.AttendanceDate),

                List<AttendanceCompile> attendances = new List<AttendanceCompile>();

                var attendancesFromDb = _context.SP_GetAllCompiledAttendanceForADate(finalize.AttendanceDate).ToList();

                foreach (var item in attendancesFromDb)
                {
                    attendances.Add(new AttendanceCompile
                    {
                        EmpId=item.EMPID,
                        Name=item.Name,
                        Department=item.Department,
                        TimeIn=item.TimeIn,
                        TimeOut=item.TimeOut,
                        InDate=item.InDate,
                        OutDate=item.OutDate,
                        WorkedH=item.Worked_Hrs_,
                        WorkedM=item.Worked_min_

                    });
                }
                ViewBag.Attendance = attendances;
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return PartialView("~/Views/_MessagePartialView.cshtml");

                //throw ex;
            }
            
            return View();
        }
        // GET: Finalizes/Create
       string msg = string.Empty;
        public ActionResult Finalize()
        {
            ViewBag.Finalize = new SelectList(_context.GetAllFinalizedDates(), "FinalizeDate");
            var variance = new List<SelectListItem>
            {
                new SelectListItem {Value = "+", Text = "+" },
                new SelectListItem{Value = "-", Text = "-"}
            };
            ViewBag.Variance = variance;
            return View();
            //return View();
        }
        [HttpPost]
        public ActionResult Finalize(Finalize finalize)
        {
            if (ModelState.IsValid)
            {
              string  errorMsg = "";
                string vdate = TASUtility.GetStringDateFormat(finalize.FinalizeDate);
               var oDate = System.DateTime.Now;
               var tdate = oDate.ToString("yyyyMMdd");

                if (string.Compare(vdate, tdate) >= 0)
                {
                    errorMsg = "You can not finalize today`s attendance.";
                }
                else if (!TASUtility.isCompiled(vdate))
                {
                    errorMsg = "You can not finalize data for this date as it is not compiled yet.";
                    ViewBag.Message = errorMsg;
                    return PartialView("~/Views/_MessagePartialView.cshtml");
                }
                try
                {
                    msg = "Finalized Successfully for " + finalize.FinalizeDate.ToLongDateString();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                ViewBag.Message = msg;
                return PartialView("~/Views/_MessagePartialView.cshtml");
            }
            return View("CompileFinalize.cshtml");
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
        [HttpPost]
        public ActionResult CompileFinalize(Finalize finalize)
        {
            if (ModelState.IsValid)
            {
                string errorMsg = "";
                string vdate = TASUtility.GetStringDateFormat(finalize.FinalizeDate);
                var oDate = System.DateTime.Now;
                var tdate = oDate.ToString("yyyyMMdd");

                if (string.Compare(vdate, tdate) >= 0)
                {
                    errorMsg = "You can not finalize today`s attendance.";
                }
                else if (!TASUtility.isCompiled(vdate))
                {
                    errorMsg = "You can not finalize data for this date as it is not compiled yet.";
                    ViewBag.Message = errorMsg;
                    return PartialView("~/Views/_MessagePartialView.cshtml");
                }
                try
                {
                    msg = "Finalized Successfully for " + finalize.FinalizeDate.ToLongDateString();
                    ViewBag.Message = msg;
                    return PartialView("~/Views/_MessagePartialView.cshtml");
                }
                catch (Exception ex)
                {

                    throw ex;
                }


                //db.Finalizes.Add(finalize);
                //db.SaveChanges();

                
            }
            ViewBag.Message = "Error has occurred. Check and continue.";
            return PartialView("~/Views/_MessagePartialView.cshtml");
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
