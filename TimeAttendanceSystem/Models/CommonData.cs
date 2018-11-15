using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeAttendanceSystem.Models
{
    public class CommonData
    {
            public long id { get; set; }
            public string Employee_Department { get; set; }
            public string Employee_Designation { get; set; }
            public string Employee_Company { get; set; }
            public string Employee_Shift { get; set; }
            public string Code { get; set; }
            public string Title { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public string Tolerence { get; set; }
            public string Type { get; set; }
            public string NextDay { get; set; }
            public string L_UID { get; set; }
            public string First_Name { get; set; }
            public string Middle_Name { get; set; }
            public string Last_Name { get; set; }
            public string Shift_Id { get; set; }
            public string Branch_Id { get; set; }
            public string E_DES_ID { get; set; }
            public string E_DEPT_ID { get; set; }
            public string E_EType { get; set; }
            public string E_STATUS { get; set; }
            public string Remove_Finalize_Status { get; set; }
            public string Employee_Type { get; set; }
            public string EType { get; set; }
            public string Employee_Name { get; set; }
            public string Employee_Terminal { get; set; }
    }
}
