using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TimeAttendanceSystem.Models
{
    public class Finalize
    {
        public int Id { get; set; }
        [DisplayName("Variance Setting:")]
        public string VarianceSetting { get; set; }
        public int Hours { get; set; }
        [DisplayName("Send Email:")]
        public bool SendEmail { get; set; }
        [DisplayName("Att Date:")]
        public string AttendanceDate { get; set; }

    }
}