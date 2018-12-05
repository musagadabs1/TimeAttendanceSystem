using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeAttendanceSystem.Models
{
    public class TenterModel
    {
        public string EmpID { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
        public string  Status { get; set; }
        public string Terminal { get; set; }
        public string Mode { get; set; }
    }
}