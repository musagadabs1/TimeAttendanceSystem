﻿using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeAttendanceSystem.Models;

namespace TimeAttendanceSystem.Controllers
{
    public class ReportsController : Controller
    {
        private UNISEntities _context = new UNISEntities();
       private ReportDocument myReportDocument = new ReportDocument();
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        // GET: Reports/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reports/Create
        public ActionResult Create()
        {
            return View();
        }
        private void AbsenteeReportFromCrystal(string fromDate,string toDate,int deptNo,string weekend)
        {
            try
            {
                /*
                 System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                string Path = Server.MapPath(Convert.ToString(Session["ReportName"]));
                Path = Path.Substring(0, Path.LastIndexOf('\\'));
                Path = Path.Substring(0, Path.LastIndexOf('\\'));
                Path = Path + "\\ReportFiles\\" + Convert.ToString(Session["ReportName"]);
                myReportDocument.Load(Path);
                myReportDocument.SetDataSource((DataTable)Session["ReportSource"]);
                myReportDocument.SetDatabaseLogon("software", "DelFirMENA$idea");

                System.IO.Stream oStream = null;
                byte[] byteArray = null;
                oStream = myReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(byteArray);
                Response.Flush();
                Response.Close();
                 
                 
                 */




                



                    //Response.ClearContent();
                    //Response.ClearHeaders();
                    //Response.ContentType = "application/pdf";
                    //Response.BinaryWrite(byteArray);
                    //Response.Flush();
                    //Response.Close();

                
            }
            catch ( Exception ex)
            {
                throw ex;
            }
        }

        private void AbsenteeReportForEmployee(string fromDate,string toDate,int deptId,string days)
        {
            try
            {
                var result = _context.sp_AbsentiesEmpWise(fromDate, toDate, deptId, days);

                //return  result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //DateTime vDate;
        //DateTime vEDate;
        public ActionResult Absentee()
        {
            ViewBag.Departments = new SelectList(_context.GetDepartmentWithDeptId(), "deptID", "Department");
            ViewBag.Employees = new SelectList(_context.SP_GetEmployee_Names(0, ""), "id", "Employee_Name");
            var Days = new List<SelectListItem>
            {
                new SelectListItem {Value = "WA", Text = "All" },
                new SelectListItem{Value = "WD", Text = "WeekDays Only"},
                new SelectListItem{Value = "WE", Text = "WeekEnds Only"}
            };
            ViewBag.Days = Days;
            return View();
        }

        [HttpPost]
        public ActionResult Absentee(Absentee absentee)
        {
            try
            {
                int departmentId = absentee.Department;
                int employeeId = absentee.Employee;
                bool sendEmail = absentee.SendEmail;
                bool deptWise = absentee.DepartmentWise;
                bool empWise = absentee.EmployeeWise;
                string days = absentee.Days;
                //vDate = absentee.FromDate.Date;
                //vEDate = absentee.ToDate.Date;
                var fromDate = TASUtility.GetStringDateFormat(absentee.FromDate.Date);
                var toDate = TASUtility.GetStringDateFormat(absentee.ToDate.Date);
                //var days = absentee.Days;

                if (empWise)
                {
                    var result= _context.sp_AbsentiesEmpWise(fromDate, toDate, departmentId, days);
                    

                    System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                    string Path = Server.MapPath("~/Reports/AbsenteesReport.rpt");
                    
                    
                    myReportDocument.Load(Path);
                    myReportDocument.SetDataSource(result.ToList());
                    myReportDocument.SetParameterValue("@vDate", fromDate);
                    myReportDocument.SetParameterValue("@vEdate",toDate);
                    myReportDocument.SetParameterValue("@deptno", departmentId);
                    myReportDocument.SetParameterValue("@Weekend", days);
                    myReportDocument.SetDatabaseLogon("software", "DelFirMENA$idea");


                    System.IO.Stream oStream = null;
                    byte[] byteArray = null;
                    oStream = myReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    byteArray = new byte[oStream.Length];
                    oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));

                    //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    oStream.Seek(0, SeekOrigin.Begin);
                    return File(oStream, "application/pdf", string.Format("Absentees{0}.pdf", DateTime.Now.ToLongTimeString()));    
                }
                else
                {
                    //AbsenteeReportFromCrystal(fromDate, toDAte, departmentId, days);
                    System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                    string Path = Server.MapPath("~/Reports/AbsenteesReport.rpt");
                    //string Path = Server.MapPath("~/ReportFiles/AbsenteesReportnew.rpt");
                    myReportDocument.Load(Path);
                    myReportDocument.SetParameterValue("@vDate", fromDate);
                    myReportDocument.SetParameterValue("@vEdate", toDate);
                    myReportDocument.SetParameterValue("@deptno", departmentId);
                    myReportDocument.SetParameterValue("@Weekend", days);
                    myReportDocument.SetDatabaseLogon("software", "DelFirMENA$idea");


                    System.IO.Stream oStream = null;
                    byte[] byteArray = null;
                    oStream = myReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    byteArray = new byte[oStream.Length];
                    oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));

                    //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    oStream.Seek(0, SeekOrigin.Begin);
                    return File(oStream, "application/pdf", string.Format("Absentees{0}.pdf",DateTime.Now.ToLongTimeString()));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            

            
            //ViewBag.Departments = new SelectList(_context.GetDepartmentWithDeptId(), "deptID", "Department");
            //return View();
        }

        [HttpGet]
        public ActionResult PresenceReport()
        {
            ViewBag.Departments = new SelectList(_context.GetDepartmentWithDeptId(), "deptID", "Department");
            ViewBag.Employees = new SelectList(_context.SP_GetEmployee_Names(0, ""), "id", "Employee_Name");
            var Days = new List<SelectListItem>
            {
                new SelectListItem {Value = "WA", Text = "All" },
                new SelectListItem{Value = "WD", Text = "WeekDays Only"},
                new SelectListItem{Value = "WE", Text = "WeekEnds Only"}
            };
            ViewBag.Days = Days;
            var EmployeeType = new List<SelectListItem>
            {
                new SelectListItem {Value = "S", Text = "All" },
                new SelectListItem{Value = "S", Text = "Staff"},
                new SelectListItem{Value = "F", Text = "Staff"}
            };
            ViewBag.EmployeeType = EmployeeType;
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
        public ActionResult PresenceReport(Presence presence)
        {

            try
            {
                int departmentId = presence.Department;
                int employeeId = presence.Employee;
                bool sendEmail = presence.SendEmail;
                bool deptWise = presence.DepartmentWise;
                bool empWise = presence.EmployeeWise;
                bool empTypeCheck = presence.EmployeeTypeCheck;
                string empType = presence.EmployeeType;
                string days = presence.Days;
                var fromDate = TASUtility.GetStringDateFormat(presence.FromDate.Date);
                var toDate = TASUtility.GetStringDateFormat(presence.ToDate.Date);

                int fromTime = int.Parse( (presence.AfterH + presence.AfterM + "00"));
                int toTime =int.Parse( (presence.BeforeH + presence.BeforeM + "00"));
                //var days = absentee.Days;

                if (empWise)
                {
                     var result = _context.sp_DailyPresentEmpTypeWise(fromDate,toDate,empType,fromTime, toTime, days);
                    
                    System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                    string Path = Server.MapPath("~/Reports/DailyPresentReportEmpWise.rpt");


                    myReportDocument.Load(Path);
                    myReportDocument.SetDataSource(result.ToList());
                    myReportDocument.SetParameterValue("@vDate", fromDate);
                    myReportDocument.SetParameterValue("@vEdate", toDate);
                    myReportDocument.SetParameterValue("@deptno", departmentId);
                    myReportDocument.SetParameterValue("@Weekend", days);
                    myReportDocument.SetDatabaseLogon("software", "DelFirMENA$idea");


                    System.IO.Stream oStream = null;
                    byte[] byteArray = null;
                    oStream = myReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    byteArray = new byte[oStream.Length];
                    oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));

                    //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    oStream.Seek(0, SeekOrigin.Begin);
                    return File(oStream, "application/pdf", string.Format("Presence{0}.pdf", DateTime.Now.ToLongTimeString()));
                }
                else if(empTypeCheck)
                {

                    var result = _context.sp_DailyPresentEmpTypeWise(fromDate, toDate, empType, fromTime, toTime, days);
                    //AbsenteeReportFromCrystal(fromDate, toDAte, departmentId, days);
                    System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                    string Path = Server.MapPath("~/Reports/PresentReportEmpTypeWise.rpt");
                    //string Path = Server.MapPath("~/ReportFiles/AbsenteesReportnew.rpt");
                    myReportDocument.Load(Path);
                    myReportDocument.SetDataSource(result.ToList());
                    myReportDocument.SetParameterValue("@vDate", fromDate);
                    myReportDocument.SetParameterValue("@vEdate", toDate);
                    myReportDocument.SetParameterValue("@deptno", departmentId);
                    myReportDocument.SetParameterValue("@Weekend", days);
                    myReportDocument.SetDatabaseLogon("software", "DelFirMENA$idea");


                    System.IO.Stream oStream = null;
                    byte[] byteArray = null;
                    oStream = myReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    byteArray = new byte[oStream.Length];
                    oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));

                    //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    oStream.Seek(0, SeekOrigin.Begin);
                    return File(oStream, "application/pdf", string.Format("Presence{0}.pdf", DateTime.Now.ToLongTimeString()));
                }
                else
                {
                    var result = _context.sp_DailyPresent_Test_Nisha(fromDate,toDate, departmentId,fromTime, toTime, days);
                    //AbsenteeReportFromCrystal(fromDate, toDAte, departmentId, days);
                    System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                    string Path = Server.MapPath("~/Reports/DailyPresentReport001.rpt");
                    //string Path = Server.MapPath("~/ReportFiles/AbsenteesReportnew.rpt");
                    myReportDocument.Load(Path);
                    myReportDocument.SetDataSource(result);
                    myReportDocument.SetParameterValue("@vDate", fromDate);
                    myReportDocument.SetParameterValue("@vEdate", toDate);
                    myReportDocument.SetParameterValue("@deptno", departmentId);
                    myReportDocument.SetParameterValue("@Weekend", days);
                    myReportDocument.SetDatabaseLogon("software", "DelFirMENA$idea");


                    System.IO.Stream oStream = null;
                    byte[] byteArray = null;
                    oStream = myReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    byteArray = new byte[oStream.Length];
                    oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));

                    //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    oStream.Seek(0, SeekOrigin.Begin);
                    return File(oStream, "application/pdf", string.Format("Presence{0}.pdf", DateTime.Now.ToLongTimeString()));
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = ex.Message;

                throw ex;
            }


        }

        // POST: Reports/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reports/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reports/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reports/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reports/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
