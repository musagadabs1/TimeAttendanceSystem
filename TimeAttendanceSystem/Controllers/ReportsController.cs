using CrystalDecisions.CrystalReports.Engine;
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
        public ActionResult PresentReport()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PresentReport(FormCollection form)
        {
            return View();
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
