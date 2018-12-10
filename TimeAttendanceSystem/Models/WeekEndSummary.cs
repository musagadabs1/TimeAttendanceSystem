using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeAttendanceSystem.Models
{
    public class WeekEndSummary
    {
        public string Month { get; set; }
        public string Year { get; set; }
        [Display(Name ="Staff Type")]
        public int StaffType { get; set; }
    }
}