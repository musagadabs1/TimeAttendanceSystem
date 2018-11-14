using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeAttendanceSystem.Models
{
    public class ErrorManagement
    {
        public int Id { get; set; }
        [Display(Name="Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Display(Name ="Department")]
        public Department Department { get; set; }

    }
}