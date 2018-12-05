using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeAttendanceSystem.Models
{
    public class MonthWeekWise
    {
        public string Month { get; set; }
        public string Year { get; set; }
        [Display(Name = "Week")]
        public int WeekType { get; set; }
        public bool IsWeekly { get; set; }
    }
}