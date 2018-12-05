using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeAttendanceSystem.Models
{
    public static class TASUtility
    {
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
        public static bool isCompiled(string date)
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
    }
}