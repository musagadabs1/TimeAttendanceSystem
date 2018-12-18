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
        public ActionResult Show()
        {
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
                //db.Finalizes.Add(finalize);
                //db.SaveChanges();
                msg = "Finalized Successfully for " + finalize.FinalizeDate.ToLongDateString();
                ViewBag.Message = msg;
                return PartialView("~/Views/_MessagePartialView.cshtml");
            }
            return View(finalize);
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
