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
    public class ManualEntriesController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private UNISEntities _context = new UNISEntities();
        // GET: ManualEntries/Create
        //[Authorize]
        //public ActionResult Create()
        ////{
            
            
        //}

        string msg = string.Empty;
        //readonly int[,] arr = new int[2,3];
        
        private string GetEmployeeNameByID(int id)
        {
            string empName = string.Empty;
            empName = _context.SP_GetEmployeeName(id).FirstOrDefault();
            //arr.GetLength(0);
            return empName;

        }

        public ActionResult ManualEntry()
        {
            ViewBag.Terminals = new SelectList(_context.SP_GetTerminal(), "L_id", "c_name");
            ViewBag.Employees = new SelectList(_context.SP_GetEmployee_Names(0, ""), "id", "Employee_Name");

            var mode = new List<SelectListItem>
            {
                new SelectListItem {Value = "01", Text = "DAY-IN" },
                new SelectListItem{Value = "02", Text = "DAY-OUT"}
            };
            ViewBag.Mode = mode;
            var timeHH = new List<SelectListItem>
            {
               new SelectListItem{ Value = "00",Text ="00"},
               new SelectListItem{Value = "01",Text = "01"},
               new SelectListItem{Value = "02", Text = "02"},
               new SelectListItem{Value = "03", Text = "03"},
               new SelectListItem{Value = "04", Text = "04"},
               new SelectListItem{Value = "05", Text = "05"},
               new SelectListItem{Value = "06", Text = "06"},
               new SelectListItem{Value = "07", Text = "07"},
               new SelectListItem{Value = "08", Text = "08"},
               new SelectListItem{Value = "09", Text = "09" },
               new SelectListItem{Value = "10", Text = "10"},
               new SelectListItem{Value = "11", Text = "11"},
               new SelectListItem{Value = "12", Text = "12"},
               new SelectListItem {Value = "13", Text = "13"},
                new SelectListItem{Value = "14", Text = "14"},
                new SelectListItem{Value = "15", Text = "15" },
                new SelectListItem{Value = "16", Text = "16"},
               new SelectListItem{Value = "17", Text = "17"},
               new SelectListItem{Value = "18", Text = "18"},
              new SelectListItem{Value = "19", Text = "19"},
              new SelectListItem{Value = "20", Text = "20"},
             new SelectListItem{Value = "21", Text = "21"},
            new SelectListItem{Value = "22", Text = "22"},
           new SelectListItem{Value = "23", Text = "23"}
            };
            ViewBag.TimeHH = timeHH;

            var timeMM = new List<SelectListItem>
            {
                new SelectListItem{Value = "00", Text = "00"},
                new SelectListItem{Value = "01", Text = "01"},
                new SelectListItem{Value = "02", Text = "02"},
                new SelectListItem{Value = "03", Text = "03"},
                new SelectListItem{Value = "04", Text = "04"},
                new SelectListItem{Value = "05", Text = "05"},
                new SelectListItem{Value = "06", Text = "06"},
                new SelectListItem{ Value = "07", Text="07"},
                new SelectListItem{ Value = "08", Text="08"},
                new SelectListItem{ Value = "09", Text="09"},
                new SelectListItem{ Value = "10", Text="10"},
                new SelectListItem{Value = "11", Text="11"},
                new SelectListItem{Value = "12", Text = "12"},
                new SelectListItem{Value = "13", Text="13"},
                new SelectListItem{Value = "14", Text="14"},
                new SelectListItem{Value = "15" ,Text="15"},
                new SelectListItem{Value = "16", Text="16"},
                new SelectListItem{Value = "17" ,Text="17"},
                new SelectListItem{Value = "18", Text="18"},
                new SelectListItem{Value = "19" ,Text="19"},
                new SelectListItem{Value = "20" ,Text="20"},
                new SelectListItem{Value = "21", Text="21"},
                new SelectListItem{Value = "22", Text="22"},
                new SelectListItem{Value = "23", Text="23"},
                new SelectListItem{Value = "24", Text="24"},
                new SelectListItem{Value = "25" ,Text="25"},
                new SelectListItem{Value = "26" ,Text="26"},
                new SelectListItem{Value = "27" ,Text="27"},
                new SelectListItem{Value = "28", Text="28"},
                new SelectListItem{Value = "29", Text="29"},
                new SelectListItem{Value = "30", Text="30"},
                new SelectListItem{Value = "31", Text="31"},
                new SelectListItem{Value = "32",Text="32"},
                new SelectListItem{Value = "33" ,Text="33"},
                new SelectListItem{Value = "34", Text="34"},
                new SelectListItem{ Value = "35", Text="35"},
                new SelectListItem{Value = "36", Text="36"},
                new SelectListItem{Value = "37", Text="37"},
                new SelectListItem{Value = "38", Text="38"},
                new SelectListItem{Value = "39", Text="39"},
                new SelectListItem{Value = "40", Text="40"},
                new SelectListItem{Value = "41", Text="41"},
                new SelectListItem{Value = "42", Text="42"},
                new SelectListItem{Value = "43", Text="43"},
                new SelectListItem{Value = "44", Text="44"},
                new SelectListItem{Value = "45", Text="45"},
                new SelectListItem{Value = "46", Text="46"},
                new SelectListItem{Value = "47", Text="47"},
                new SelectListItem{Value = "48", Text = "48"},
                new SelectListItem{Value = "49", Text="49"},
                new SelectListItem{Value="50", Text="50"},
                new SelectListItem{Value="51", Text="51"},
                new SelectListItem{Value="52" ,Text="52"},
                new SelectListItem{Value="53", Text="53"},
                new SelectListItem{Value="54", Text="54"},
                new SelectListItem{Value="55", Text="55"},
                new SelectListItem{Value="56", Text="56"},
                new SelectListItem{Value="57" ,Text="57"},
                new SelectListItem{Value="58", Text="58"},
                new SelectListItem{Value="59" ,Text="59"}

            };
            ViewBag.TimeMM = timeMM;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManualEntry([Bind(Include = "Id,EmployeeID,Name,TerminalID,Mode,TimeHH,TimeMM,Date,Remarks")] ManualEntry manualEntry)
        {
            if (ModelState.IsValid)
            {
                var time = manualEntry.TimeHH + manualEntry.TimeMM + "00";
                int empID = manualEntry.EmployeeID;
                var empName = _context.SP_GetEmployeeName(empID).FirstOrDefault();
                DateTime date = Convert.ToDateTime(manualEntry.Date);
                var dateString = TASUtility.GetStringDateFormat(date);
                TASUtility.DateString = dateString;
                var terminal = manualEntry.TerminalID;
                var empId = manualEntry.EmployeeID;
                var mode = manualEntry.Mode;
                var remark = manualEntry.Remarks;
                try
                {
                    _context.SP_Manual_Entry(dateString, time, terminal, empId.ToString(), empName, mode, remark, "", "Insert");
                    msg = "Manual Entry Entered Successfully.";
                    //_context.SaveChanges();
                    ViewBag.Message = msg;
                    return PartialView("~/Views/_MessagePartialView.cshtml");
                }
                catch (Exception ex)
                {

                    ViewBag.Message = ex.Message;
                }

            }
            ViewBag.Message = "Error has occured. Check and try again.";
            return PartialView("~/Views/_MessagePartialView.cshtml");
        }
        

        public JsonResult GetManualEntry()
        {
            try
            {
                List<ManualEntry> manualEntries = TASUtility.GetManuelEntry(TASUtility.DateString);
                if (manualEntries.Count>0 && manualEntries !=null)
                {
                    return Json(manualEntries, JsonRequestBehavior.AllowGet);
                }
                return null;
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
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
