using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
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
        ReportDocument[] Rptdocuments = new ReportDocument[5];
        ReportDocument[] Rptdocuments001 = new ReportDocument[5];
        ReportDocument CR_reportDocument = new ReportDocument();

        int errorflag = 0;
        string errormess;

        string[] pdfs = new string[5];
        string[] pdfs001 = new string[5];

        private List<AttendanceCompile> GetAttendanceCompiles(string date)
        {
            try
            {
                List<AttendanceCompile> attendances = new List<AttendanceCompile>();

                var attendancesFromDb = _context.SP_GetAllCompiledAttendanceForADate(date).ToList();

                foreach (var item in attendancesFromDb)
                {
                    attendances.Add(new AttendanceCompile
                    {
                        EmpId = item.EMPID,
                        Name = item.Name,
                        Department = item.Department,
                        TimeIn = item.TimeIn,
                        TimeOut = item.TimeOut,
                        InDate = item.InDate,
                        OutDate = item.OutDate,
                        WorkedH = item.Worked_Hrs_,
                        WorkedM = item.Worked_min_

                    });
                }
                return attendances;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private UNISEntities _context = new UNISEntities();
        [HttpPost]
        public ActionResult Show(Finalize finalize)
        {
            try
            {
                //string date=TASUtility.GetStringDateFormat(finalize.AttendanceDate),

                
                ViewBag.Attendance = GetAttendanceCompiles(finalize.AttendanceDate);
                //return View("Show.cshtml");
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
            //if (ModelState.IsValid)
            //{

            //  string  errorMsg = "";
            //    string vdate = TASUtility.GetStringDateFormat(finalize.FinalizeDate);
            //   var oDate = System.DateTime.Now;
            //   var tdate = oDate.ToString("yyyyMMdd");

            //    if (string.Compare(vdate, tdate) >= 0)
            //    {
            //        errorMsg = "You can not finalize today`s attendance.";
            //    }
            //    else if (!TASUtility.isCompiled(vdate))
            //    {
            //        errorMsg = "You can not finalize data for this date as it is not compiled yet.";
            //        ViewBag.Message = errorMsg;
            //        return PartialView("~/Views/_MessagePartialView.cshtml");
            //    }
            //    try
            //    {
            //        msg = "Finalized Successfully for " + finalize.FinalizeDate.ToLongDateString();
            //    }
            //    catch (Exception ex)
            //    {

            //        throw ex;
            //    }
            //    ViewBag.Message = msg;
            //    return PartialView("~/Views/_MessagePartialView.cshtml");
            //}
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
        [HttpPost]
        public ActionResult CompileFinalize(Finalize finalize)
        {
            //if (ModelState.IsValid)
            //{
            string errorMsg = "";
            string vdate = TASUtility.GetStringDateFormat(finalize.FinalizeDate);
            var oDate = System.DateTime.Now;
            var tdate = oDate.ToString("yyyyMMdd");
            var varHour = 0;
            //bool sendMail;

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
                if ((finalize.VarianceSetting !=null) || (finalize.VarianceSetting == "+"))
                {
                    varHour = Convert.ToInt32(finalize.Hours);
                }
                else
                {
                    varHour = Convert.ToInt32(finalize.Hours) * (-1);
                }
                //if (finalize.SendEmail)
                //{
                //    sendMail
                //}
                    PushLogAttendanceFinalize(vdate, tdate, varHour);
                    ViewBag.Attendance = GetAttendanceCompiles(vdate);
                    msg = "Attendance Finalized Successfully for " + finalize.FinalizeDate.ToLongDateString();
                    ViewBag.Message = msg;
                    NewCompile(vdate);
                SendConsolidatedReports(finalize.FinalizeDate.ToShortDateString(), finalize.SendEmail);
                msg = "Attendance Finalized Successfully for " + finalize.FinalizeDate.ToLongDateString();
                ViewBag.Message = msg;
                //return PartialView("~/Views/_MessagePartialView.cshtml");
                return View();
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return PartialView("~/Views/_MessagePartialView.cshtml");

                    //throw ex;
                }
            //}
            
        }
        public void SendConsolidatedReports(string date,bool sendMail)
        {
            DataTable dt = new DataTable();
            var vDate = Convert.ToDateTime(date);
            try
            {
                ArrayList pa = new ArrayList();
                ArrayList pv = new ArrayList();
                pa.Add("@Date");
                pv.Add(date);

                CreateDataTable_Payroll(dt, "SP_DAILYINOUT_SHIFTWISE", true, pa, pv);
                Rptdocuments001[0] = TASUtility.SendReports("DailyINOUTShiftWise_NEW.rpt", dt);
                //pdfs001[0] =  dtpick_finalizedate.CalendarDate.DayOfWeek.ToString() + " DailyINOUTShiftWise_NEW.pdf";
                pdfs001[0] = vDate.DayOfWeek.ToString() + " DailyINOUTShiftWise_NEW.pdf";
                CreateReportNew("DailyINOUTShiftWise_NEW.rpt", pdfs001[0], Rptdocuments001[0], 0, "0",date);



                string nextdate =vDate.AddDays(1).ToString("yyyyMMdd");
                string oNewDate = vDate.AddDays(2).ToString("yyyyMMdd");

                //Compiling todays attendance........
                //CDummyCompile oDummyCompile = new CDummyCompile();
                TASUtility.PushLogDepartment(nextdate, oNewDate);

                pv[0] = nextdate;
                TASUtility.CreateDataTable(dt, "SP_PRESENTREPORT", true, pa, pv);
                Rptdocuments001[1] = TASUtility.SendReports("DailyPresentReport002.rpt", dt);
                pdfs001[1] = vDate.AddDays(1).DayOfWeek.ToString() + " PresentReport.pdf";
                CreateReportNew("DailyPresentReport002.rpt", pdfs001[1], Rptdocuments001[1], 1, "0",date);

                dt = FillDataWithProcedureNew("sp_compiledReport11", date, date, 0, "WA", "-040000");
                Rptdocuments001[2] = TASUtility.SendReports("DailyInOutReport.rpt", dt);
                pdfs001[2] = vDate.DayOfWeek.ToString() + " In Out Report (Nigeria).pdf";
                CreateReportNew("DailyInOutReport.rpt", pdfs001[2], Rptdocuments001[2], 1, "0",date);

                pdfs001[3] = vDate.AddDays(1).DayOfWeek.ToString() + " Absentees Report.pdf";
                CreateReportNewAbsent("AbsenteesReport.rpt", pdfs001[3], nextdate,date);
                pdfs001[4] = vDate.DayOfWeek.ToString() + " Absentees Report.pdf";

                CreateReportNewAbsent("AbsenteesReport.rpt", pdfs001[4], date,date);
                //errorMsg = "Finalized successfully! ";
                if (sendMail)
                {
                    //Main Mail Sending Line -15/09/2018
                    TASUtility.SendEmail("developer@sun.edu.ng", "", "head.hr@sun.edu.ng", "Reports - Automatic", "", System.Web.Mail.MailFormat.Text, Rptdocuments001, pdfs001, "hr@sun.edu.ng", "head.it@sun.edu.ng", "software@sun.edu.ng", "vc@sun.edu.ng", "director.operations@sun.edu.ng", "prochancellor@sun.edu.ng", date);
                }
                //  ErrorMsg.Text = "Mail sent successfully!";
            }
            finally
            {
                dt.Dispose();
            }
        }
        private void PushLogAttendanceFinalize(string date,string nDate,int hour)
        {
            try
            {
                _context.cfinalizemodified(date, nDate, hour);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void NewCompile(string date)
        {
            ArrayList pa = new ArrayList();
            ArrayList pv = new ArrayList();
            DataTable Dtb = new DataTable();
            string vdate = date;// dtpick_finalizedate.CalendarDate.ToString("dd/MMM/yyyy");
            pa.Add("@DDate");
            pv.Add(vdate);
            CreateDataTable_Payroll(Dtb, "SP_COMPILELOG", true, pa, pv);
        }
        public void CreateDataTable_Payroll(DataTable DT, string StrSql, bool IsStoredProcedure = false, ArrayList pa = null, ArrayList pv = null)
        {
            try
            {
                DT.Rows.Clear();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["payroll"].ToString();
                con.Open();
                SqlDataAdapter adaptor = new SqlDataAdapter(StrSql, con);
                adaptor.SelectCommand.CommandTimeout = 6000;
                if (IsStoredProcedure == true)
                {
                    adaptor.SelectCommand.CommandType = CommandType.StoredProcedure;
                    for (int count = 0; count < pa.Count; count++)
                    {
                        // pa[count] = pv[count];
                        adaptor.SelectCommand.Parameters.AddWithValue(pa[count].ToString(), pv[count]);
                    }
                }
                adaptor.Fill(DT);
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CreateReportNewAbsent(string reprotname, string pdf, string dateparam,string date)
        {
            try
            {
                string date1 = date; //dtpick_finalizedate.CalendarDate.ToString("yyyyMMdd");
                string path = @"D:\\SkylineERP\\TimeAttendanceNew\\TmpArchieve\\" + date1;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string reportnamerpt = reprotname;

                string reprotnamepdf = pdf;

                if (System.IO.Directory.Exists(@"D:\\SkylineERP\\TimeAttendanceNew\\TmpArchieve\\" + date1 + "\\" + reprotnamepdf))
                {
                    try
                    {
                        System.IO.Directory.Delete(@"D:\\SkylineERP\\TimeAttendanceNew\\TmpArchieve\\" + date1 + "\\" + reprotnamepdf, true);
                    }
                    catch (Exception E)
                    {
                        Console.WriteLine(E.Message);
                    }
                }

                if (reprotname.Contains("Absent"))
                {
                    CR_reportDocument.Load(Server.MapPath("../ReportFiles/AbsenteesReport.rpt"));
                    CR_reportDocument.SetDatabaseLogon("software", "DelFirMENA$idea");

                    CR_reportDocument.SetParameterValue("@vDate", dateparam);
                    CR_reportDocument.SetParameterValue("@vEdate", dateparam);
                    CR_reportDocument.SetParameterValue("@deptno", "0");
                    CR_reportDocument.SetParameterValue("@Weekend", "WA");
                    CR_reportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, @"D:\\SkylineERP\\TimeAttendanceNew\\TmpArchieve\\" + date1 + "\\" + reprotnamepdf);

                }

            }


            catch (Exception Ex)
            {
                //ErrorMsg.Text = "Try again";
                errorflag = 1;
                errormess = reprotname + Ex.ToString();
                return;
            }

        }
        public void CreateReportNew(string reprotname, string pdf, ReportDocument myReportDocument, int orderno, string dateparam,string date)
        {
            try
            {
                string date1 = date;// dtpick_finalizedate.CalendarDate.ToString("yyyyMMdd");

                string path = @"D:\\SkylineERP\\TimeAttendanceNew\\TmpArchieve\\" + date1;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string reportnamerpt = reprotname;

                string reprotnamepdf = pdf;

                System.IO.MemoryStream stream1 = new System.IO.MemoryStream();

                System.IO.Stream oStream = null;



                if (reprotname.Contains("Absent"))
                {
                    myReportDocument.SetParameterValue("@vDate", dateparam);
                    myReportDocument.SetParameterValue("@vEdate", dateparam);
                    myReportDocument.SetParameterValue("@deptno", 0);
                    myReportDocument.SetParameterValue("@Weekend", "WA");

                }


                if (orderno == 1)
                    myReportDocument.SetDatabaseLogon("unisuser", "unisamho");
                else
                    myReportDocument.SetDatabaseLogon("software", "DelFirMENA$idea");

                byte[] byteArray = null;
                oStream = myReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);


                byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                byte[] content = byteArray;



                if (System.IO.Directory.Exists(@"D:\\SkylineERP\\TimeAttendanceNew\\TmpArchieve\\" + date1 + "\\" + reprotnamepdf))
                {
                    try
                    {
                        System.IO.Directory.Delete(@"D:\\SkylineERP\\TimeAttendanceNew\\TmpArchieve\\" + date1 + "\\" + reprotnamepdf, true);
                    }
                    catch (Exception E)
                    {
                        Console.WriteLine(E.Message);
                    }
                }
                // Write out PDF from memory stream. 
                using (FileStream fs =System.IO.File.Create(@"D:\\SkylineERP\\TimeAttendanceNew\\TmpArchieve\\" + date1 + "\\" + reprotnamepdf))
                {
                    fs.Write(content, 0, content.Length);
                }


            }


            catch (Exception Ex)
            {
                //ErrorMsg.Text = "Try again";
                errorflag = 1;
                errormess = reprotname + Ex.ToString();
                return;
            }

        }
        public static DataTable FillDataWithProcedureNew(string ProcedureName, string todate, string fromdate, int deptid, string days, string Country)
        {

            SqlConnection con = new SqlConnection(TASUtility.GetConnectionSql());
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand(ProcedureName, con);
                cmd.Parameters.Add(new SqlParameter("@vDate", fromdate));
                cmd.Parameters.Add(new SqlParameter("@vEdate", todate));
                cmd.Parameters.Add(new SqlParameter("@deptno", deptid));
                cmd.Parameters.Add(new SqlParameter("@Weekend", days));
                cmd.Parameters.Add(new SqlParameter("@Country", Country));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);
                return dt;
            }

            catch (Exception)
            {
                return null;
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
