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
    
    public partial class tblWorkLogBatchDtl
    {
        public int WLBatchDtlID { get; set; }
        public int WLDtlID { get; set; }
        public int WLID { get; set; }
        public int BatchID { get; set; }
        public int ActualSessions { get; set; }
        public int Sessions { get; set; }
        public string PresentStatus { get; set; }
        public bool IsActive { get; set; }
    
        public virtual tblWorkLog tblWorkLog { get; set; }
        public virtual tblWorkLogDtl tblWorkLogDtl { get; set; }
    }
}
