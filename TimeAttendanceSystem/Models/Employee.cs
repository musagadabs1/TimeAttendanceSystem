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
        [Display(Name ="Machine Id")]
        public int MachineId { get; set; }
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Display(Name ="Middle Name")]
        public string MiddleName { get; set; }
        [Display(Name ="Last Name")]
        public string LastName { get; set; }
        public Department Department { get; set; }
        public Designation Designation { get; set; }
        [Display(Name ="Type")]
        public Type StaffType { get; set; }
        public Company Company { get; set; }
        public Shift Shift { get; set; }

    }
    public enum Department
    {
        Finance,IT
    }
    public enum Designation
    {
        Developer,Programmer
    }
    public enum Company
    {
        SkylineUniversityNigeria,SkylineCollege
    }
    public enum Shift
    {
        Straight, Split
    }
    public enum Type
    {
        Staff,Faculty,Other
    }

}