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
    
    public partial class SP_GetAllCompiledAttendanceForADate_Result
    {
        public string EMPID { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string TimeIn { get; set; }
        public string InDate { get; set; }
        public string TimeOut { get; set; }
        public string OutDate { get; set; }
        public Nullable<int> Worked_Hrs_ { get; set; }
        public Nullable<int> Worked_min_ { get; set; }
    }
}