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
    using System.Collections.Generic;
    
    public partial class AttendanceInOut
    {
        public long ID { get; set; }
        public Nullable<long> EmpID { get; set; }
        public string EmpCode { get; set; }
        public string EDate { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string HostName { get; set; }
    }
}
