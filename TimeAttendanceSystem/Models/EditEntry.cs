﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeAttendanceSystem.Models
{
    public class EditEntry
    {
        public int Id { get; set; }
        [DataType(DataType.Date),Display(Name ="Date")]
        public DateTime ErrorDate { get; set; }
        public int Department { get; set; }

        [Display(Name = "Employee ID ")]
        public string EmployeeID { get; set; }
        [Display(Name = "Terminal ID")]
        public int TerminalID { get; set; }
        [Display(Name = "Mode")]
        public int Mode { get; set; }
        [Display(Name = "Time (HH)")]
        public string TimeHH { get; set; }
        [Display(Name = "Time (MM)")]
        public string TimeMM { get; set; }
        [Display(Name = "Date"), DataType(DataType.Date)]
        public string Date { get; set; }
        
    }
}