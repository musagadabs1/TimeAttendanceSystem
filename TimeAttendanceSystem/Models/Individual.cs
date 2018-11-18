using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeAttendanceSystem.Models
{
    public class Individual
    {
        public int Id { get; set; }
        [Display(Name = "Employee ID *")]
        public string EmployeeID { get; set; }
        [Display(Name = "From Date")]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }
        [Display(Name = "To Date")]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }
    }
}