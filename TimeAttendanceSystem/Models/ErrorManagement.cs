using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeAttendanceSystem.Models
{
    public class ErrorManagement
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Department Department { get; set; }
    }
}