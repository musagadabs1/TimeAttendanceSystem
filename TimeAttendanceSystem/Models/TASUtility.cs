using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeAttendanceSystem.Models
{
    public class TASUtility
    {
        public List<CommonData> GetShiftSchedule(int ID)
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
    }
}