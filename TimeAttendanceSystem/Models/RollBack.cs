﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeAttendanceSystem.Models
{
    public class RollBack
    {
        public int Id { get; set; }
        [Display(Name = "Last Compiled")]
        [DataType(DataType.Date)]
        public DateTime LastCompiled { get; set; }
        [Display(Name = "Select Date")]
        [DataType(DataType.Date)]
        public DateTime SelectDate { get; set; }
    }
}