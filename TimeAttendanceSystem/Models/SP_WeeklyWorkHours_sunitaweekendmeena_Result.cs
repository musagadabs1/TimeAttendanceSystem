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
    
    public partial class SP_WeeklyWorkHours_sunitaweekendmeena_Result
    {
        public string employeetype { get; set; }
        public int EmpNumber { get; set; }
        public string NAME { get; set; }
        public Nullable<System.DateTime> StarDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string WeekNumber { get; set; }
        public string ActualHrs { get; set; }
        public string WorkHrs { get; set; }
        public Nullable<int> Variance { get; set; }
        public Nullable<int> sw { get; set; }
        public string HolidayHrs { get; set; }
        public string LeaveHrs { get; set; }
        public Nullable<int> ActualMinuts { get; set; }
        public Nullable<int> WorkedMinuts { get; set; }
        public Nullable<int> HolidaysMinuts { get; set; }
        public Nullable<int> LeaveMinuts { get; set; }
        public Nullable<int> OutDoorMinuts { get; set; }
    }
}