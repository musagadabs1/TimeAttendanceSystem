using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeAttendanceSystem.Models
{
    public class AttendanceCompile
    {
        public string EmpId { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string TimeIn { get; set; }
        public string InDate { get; set; }
        public string TimeOut { get; set; }
        public string OutDate { get; set; }
        [Display(Name = "Worked (Hrs)")]
        public int? WorkedH { get; set; }
        [Display(Name="Worked (Min)")]
        public int? WorkedM { get; set; }
    }
}