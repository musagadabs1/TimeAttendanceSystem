using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeAttendanceSystem.Models
{
    public class DesignationDrop
    {
        public int Id { get; set; }
        public int desigId { get; set; }
        public string Designation { get; set; }
        public int DeptId { get; set; }
    }
}