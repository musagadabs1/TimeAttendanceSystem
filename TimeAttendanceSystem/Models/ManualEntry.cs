using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeAttendanceSystem.Models
{
    public class ManualEntry
    {
        //private DateTime _date = DateTime.Now.Date;
        public int Id { get; set; }
        [Display(Name= "Employee ID *")]
        public string EmployeeID { get; set; }
        //[Display(Name="Name *")]
        //public string Name { get; set; }
        [Display(Name ="Terminal ID")]
        public int TerminalID { get; set; }
        [Display(Name ="Mode")]
        public int Mode { get; set; }
        [Display(Name ="Time (HH)")]
        public string TimeHH { get; set; }
        [Display(Name = "Time (MM)")]
        public string TimeMM { get; set; }
        [Display(Name = "Date"), DataType(DataType.Date)]
        public string Date { get; set; }
        [Display(Name ="Remarks")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }
    }
}