using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeAttendanceSystem.Models
{
    public class ErrorCorrection
    {
        public int Id { get; set; }
        [Display(Name ="Previous Date Compiled")]
        [DataType(DataType.Date)]
        public DateTime PreviousDateCompiled { get; set; }
        [Display(Name = "Select Date to Compile")]
        [DataType(DataType.Date)]
        public DateTime SelectDateToCompile { get; set; }
    }
}