using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeAttendanceSystem.Models
{
    public class EmployeeViewModel
    {
        public int MachineCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int ShiftId { get; set; }
        public int BranchId { get; set; }
        public int DesignationId { get; set; }
        public int DepartmentId { get; set; }
        public string EmployeeType { get; set; }
        public string Status { get; set; }
    }
}