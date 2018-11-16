using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeAttendanceSystem.Models
{
    public class ShiftDetail
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Tolerence { get; set; }
        public string Type { get; set; }
        public string NextDay { get; set; }
    }
}