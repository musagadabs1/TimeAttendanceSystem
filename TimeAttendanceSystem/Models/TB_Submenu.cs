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
    
    public partial class TB_Submenu
    {
        public int ID { get; set; }
        public string SubCategory { get; set; }
        public Nullable<int> MainCategory { get; set; }
        public string URL { get; set; }
        public Nullable<bool> Isactive { get; set; }
        public Nullable<int> Order { get; set; }
    }
}
