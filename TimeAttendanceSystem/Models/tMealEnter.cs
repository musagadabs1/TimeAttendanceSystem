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
    
    public partial class tMealEnter
    {
        public string C_Date { get; set; }
        public string C_Time { get; set; }
        public int L_TID { get; set; }
        public int L_UID { get; set; }
        public Nullable<int> L_MealType { get; set; }
        public string C_Menu { get; set; }
        public Nullable<int> L_MealPay { get; set; }
        public string C_Reason { get; set; }
        public string C_Upmode { get; set; }
        public Nullable<int> L_MealCount { get; set; }
        public string C_StatsDate { get; set; }
    }
}
