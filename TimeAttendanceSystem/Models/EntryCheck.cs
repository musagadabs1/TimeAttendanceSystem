 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeAttendanceSystem.Models
{
    public class EntryCheck
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Display(Name ="Employee")]
        public int EmployeeId { get; set; }
        public string Name { get; set; }

        [Display(Name = "Terminal ID")]
        public int TerminalID { get; set; }
        [Display(Name ="Emp Id")]
        public int EmpId { get; set; }
        [Display(Name = "Mode")]
        public int Mode { get; set; }
        [Display(Name = "Time (HH)")]
        public string TimeHH { get; set; }
        [Display(Name = "Time (MM)")]
        public string TimeMM { get; set; }
        [Display(Name = "Date"), DataType(DataType.Date)]
        public string DateEntry { get; set; }
    }
}