//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TimeAttendanceSystem.Models
{
    using System;
    
    public partial class SP_WeeklyWorkHours001_Result
    {
        public int EmpNumber { get; set; }
        public string NAME { get; set; }
        public Nullable<System.DateTime> StarDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string WeekNumber { get; set; }
        public string ActualHrs { get; set; }
        public string WorkHrs { get; set; }
        public string Variance { get; set; }
        public string HolidayHrs { get; set; }
        public string LeaveHrs { get; set; }
    }
}
