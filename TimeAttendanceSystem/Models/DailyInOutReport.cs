using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeAttendanceSystem.Models
{
    public class DailyInOutReport
    {
        public int Id { get; set; }
        [DisplayName("Department Wise")]
        //[Required(ErrorMessage ="")]
        public bool DepartmentWise { get; set; }
        [DisplayName("Employee Wise")]
        public bool EmployeeWise { get; set; }
        public int Department { get; set; }
        public int Employee { get; set; }
        public string Days { get; set; }
        [Display(Name = "From Date")]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }
        [Display(Name = "To Date")]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }
        [Display(Name = "Send Email ?")]
        public bool SendEmail { get; set; }
    }
}