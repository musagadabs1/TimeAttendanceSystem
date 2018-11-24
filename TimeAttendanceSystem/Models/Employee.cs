using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeAttendanceSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Display(Name="Is Active ?")]
        public bool IsActive { get; set; }
        [Required(ErrorMessage ="Machine Code is Required.")]
        [Display(Name ="Machine Code")]
        public string MachineId { get; set; }
        [Required(ErrorMessage ="First Name is Required.")]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Middle Name is Required.")]
        [Display(Name ="Middle Name")]
        public string MiddleName { get; set; }
        [Display(Name ="Last Name")]
        [Required(ErrorMessage = "Last Name is Required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Department is Required.")]
        public int Department { get; set; }
        [Required(ErrorMessage = "Designation is Required.")]
        public int Designation { get; set; }
        [Display(Name ="Type")]
        public string StaffType { get; set; }
        public string Company { get; set; }
        public int Shift { get; set; }

    }
}