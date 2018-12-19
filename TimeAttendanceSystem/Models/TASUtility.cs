using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace TimeAttendanceSystem.Models
{
    public static class TASUtility
    {
        private static string _constr;
        public static string ConnectionString
        {
            get
            { _constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString; return _constr;
            }
            set
            {
                _constr = value;
            }
        }
        private static UNISEntities _context = new UNISEntities();
        public static List<CommonData> GetShiftSchedule(int? ID)
        {
            List<CommonData> lst = new List<CommonData>();

            using (UNISEntities _context=new UNISEntities())
            {
                try
                {
                    var getShifts = (from s in _context.SP_GetShiftDetails_Employee(ID) select s).ToList();

                    foreach (var item in getShifts)
                    {
                        lst.Add(new CommonData
                        {
                            Code=item.Code,
                            Title=item.Title,
                            StartTime=item.StartTime,
                            EndTime=item.EndTime,
                            Tolerence=item.Tolerance,
                            Type=item.Type,
                            NextDay=item.NextDay
                        });
                    }
                    return lst;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            
            
        }
        public static string GetLastCompiledDate()
        {
            string compiledDate = string.Empty;
            var dates = _context.SP_Get_LastCompiledDate();
            if (dates != null)
            {
                compiledDate = dates.FirstOrDefault().Date;
            }
            return compiledDate;
        }
        public static string GetStringDateFormat(DateTime dateTime)
        {
            var selectedDate = dateTime.Date.ToShortDateString();
            string[] selectDateArray = selectedDate.Split('/');
            int d = int.Parse(selectDateArray[1]);
            int m = int.Parse(selectDateArray[0]);
            int yy = int.Parse(selectDateArray[2]);

            string dd = string.Empty;
            string mm = string.Empty;
            //Determine of month of the year is within 1 to 9
            // add leading zero if is within the range otherwise don't add
            if (d >= 10)
            {
                dd = d.ToString();
            }
            else
            {
                dd = "0" + d.ToString();
            }

            if (m >= 10)
            {
                mm = m.ToString();
            }
            else
            {
                mm = "0" + m.ToString();
            }
            //concatenate the date to yyyymmdd formate
            var oDate = yy.ToString() + mm.ToString() + dd.ToString();
            return oDate;
        }
        public static List<FrmError> GetLoad_Error(int id, string date,  int empId)
        {

            //var PassingDate = TASUtility.GetStringDateFormat(date.Date); //GetProperStringDate(Date);
            List<FrmError> lst = new List<FrmError>();
            try
            {
                var errors = _context.SP_Load_Error(id, date, empId).ToList();
                foreach (var item in errors)
                {
                    lst.Add(new FrmError
                    {
                        EMPID =item.EMPID,
                        Name = item.Name,
                        Count = item.Count.ToString(),

                    });
                }

                
                        
                return lst;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public static List<ManualEntry> GetManuelEntry(string vDate, string role = "HR")
        {

            List<ManualEntry> lst = new List<ManualEntry>();

            try
            {
                var manualEntries = _context.SP_GetManuelEntry(vDate, role).ToList();

                if (manualEntries.Count>0 && manualEntries != null)
                {
                    foreach (var item in manualEntries)
                    {
                        lst.Add(new ManualEntry
                        {
                            EmployeeID=item.EMPID,
                            Name = item.Name,
                            TimeHH=item.Time,
                            TerminalID=Convert.ToInt32( item.Terminal),
                            TimeMM=item.Time,
                            Date = item.Date,
                            Remarks = item.Remarks,
                            Mode =Convert.ToInt32( item.Mode),
                            User = item.USER,
                            Modified = item.Modified,
                            TerminalName = item.TerminalName,
                            ModeName = item.ModeName,
                        });



                        
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {

                throw ex;
            }
                
                    
        }
        public static string DateString { get; set; }
        public static bool IsCompiled(string date)
        {
            try
            {
                //int lid = 0;
                var compileStatus = (from c in _context.compilestatus where c.e_date == date select c).FirstOrDefault();
                if (compileStatus != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<TenterModel> GetNextEntry(DateTime Date, string EmpID)
        {
            string vDate = GetStringDateFormat(Date);

            List<TenterModel> lst = new List<TenterModel>();
            try
            {
                var getEntry = _context.SP_getNextEntry(vDate, EmpID).ToList();

                foreach (var item in getEntry)
                {
                    lst.Add(new TenterModel
                    {
                        EmpID = item.EMPID.ToString(),
                        Name = item.Name,
                        Time = item.Time,
                        Status = item.Status,
                        Terminal = item.Terminal.ToString(),
                        Mode = item.Mode.ToString()
                    });
                }
                return lst;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static bool SendEmail(string pGmailEmail, string pGmailPassword, string pTo, string pSubject, string pBody, System.Web.Mail.MailFormat pFormat, ReportDocument[] Rpt1, string[] pdfs, string cc1, string cc2, string cc3, string cc4, string cc5, string cc6, string date)
        {
            try
            {
                string fromEmail = "developer@sun.edu.ng";
                string fromPassword = "Xdirsun202034";

                //pGmailEmail = "noreply@skylineuniversity.ac.ae";
                SmtpClient SmtpServer = new SmtpClient();
                MailMessage mail = new MailMessage();
                System.Text.StringBuilder mailbody = new System.Text.StringBuilder();
                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.office365.com";
                SmtpServer.Credentials = new NetworkCredential("smtp.office365.com\\" + fromEmail, fromPassword);
                //SmtpServer.Credentials = new NetworkCredential("smtp.office365.com\\noreply@skylineuniversity.ac.ae", "Sucsky2017");
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.EnableSsl = true;


                mail.From = new MailAddress(fromEmail);
                //mail.From = new MailAddress(pGmailEmail);
                mail.To.Add(pTo);
                mail.Subject = pSubject;
                if (cc1 != "")
                    mail.CC.Add(cc1);
                mail.CC.Add(cc2);
                mail.CC.Add(cc3);
                mail.CC.Add(cc4);
                mail.CC.Add(cc5);
                mail.CC.Add(cc6);
                //mail.CC.Add(cc7);
                //mail.CC.Add(cc8);
                //mail.CC.Add(cc9);
                //mail.CC.Add(cc10);
                mail.IsBodyHtml = false;
                pBody = "Dear Sir," + Environment.NewLine + Environment.NewLine + "Kindly find the attached daily attendance report." + Environment.NewLine + Environment.NewLine + Environment.NewLine + "Thanks and Regards," + Environment.NewLine + Environment.NewLine + "Human Resource Department";

                mailbody.Append(pBody);

                mail.Body = mailbody.ToString();


                for (int i = 0; i < Rpt1.Length; i++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(pdfs[i])))
                    {

                        mail.Attachments.Add(new Attachment(@"D:\\SkylineERP\\TimeAttendanceNew\\TmpArchieve\\" + date + "\\" + pdfs[i]));

                    }
                }

                SmtpServer.Send(mail);
                return true;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public static ReportDocument SendReports(string ReportName, DataTable source)
        {
            ReportDocument cryRpt;
            cryRpt = new ReportDocument();
            try
            {
                string Path = HttpContext.Current.Server.MapPath(Convert.ToString(ReportName));
                Path = Path.Substring(0, Path.LastIndexOf('\\'));
                Path = Path.Substring(0, Path.LastIndexOf('\\'));
                Path = Path + "\\Reports\\" + Convert.ToString(ReportName);
                cryRpt.Load(Path);
                cryRpt.SetDataSource(source);
                return cryRpt;
            }
            catch (Exception ex )
            {
                throw ex;
            }
        }
        public static void PushLogDepartment(string vDate, string vNewDate)
        {


            SqlConnection cn = new SqlConnection(GetConnectionSql());
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "RRcompile";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter p = default(SqlParameter);
            p = cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@vDate", SqlDbType.VarChar, 8));
            p.Value = vDate;

            p = cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@nDate", SqlDbType.VarChar, 8));
            p.Value = vNewDate;


            cmd.CommandTimeout = 0;
            cmd.ExecuteNonQuery();

            cn.Close();
            cmd = null;
            cn = null;
        }
        public static void CreateDataTable(DataTable DT, string StrSql, bool IsStoredProcedure = false, ArrayList pa = null, ArrayList pv = null)
        {
            try
            {
                DT.Rows.Clear();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConnectionString;
                con.Open();
                SqlDataAdapter adaptor = new SqlDataAdapter(StrSql, con);

                if (IsStoredProcedure == true)
                {
                    adaptor.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adaptor.SelectCommand.CommandTimeout = 0;
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
        public static string GetConnectionSql()
        {
            return "server=LAPTOP-HSLHPTC2\\SQLEXPRESS;Initial Catalog=UNIS; User ID=software; Password=DelFirMENA$idea;";

        }
    }
}