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
    
    public partial class tblContact
    {
        public int ContactId { get; set; }
        public int PersonId { get; set; }
        public string PrstFlatNo { get; set; }
        public string PrstBuilding { get; set; }
        public string PrstPOBox { get; set; }
        public int PrstCountry { get; set; }
        public string PrstCity { get; set; }
        public string PrstEmail { get; set; }
        public string PrstPhone { get; set; }
        public string PrstMobile { get; set; }
        public string PrstAddress { get; set; }
        public string PrstWebsite { get; set; }
        public string PrmtFlatNo { get; set; }
        public string PrmtBuilding { get; set; }
        public string PrmtPOBox { get; set; }
        public Nullable<int> PrmtCountry { get; set; }
        public string PrmtCity { get; set; }
        public string PrmtEmail { get; set; }
        public string PrmtPhone { get; set; }
        public string PrmtMobile { get; set; }
        public string PrmtAddress { get; set; }
        public string PrmtWebsite { get; set; }
        public string EmgyName { get; set; }
        public string EmgyPhone { get; set; }
        public int EmgyRelation { get; set; }
        public string EmgyMobile { get; set; }
        public string Attr1 { get; set; }
        public string Attr2 { get; set; }
        public string Attr3 { get; set; }
        public string Attr4 { get; set; }
        public string Attr5 { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CreatedHost { get; set; }
        public Nullable<bool> Status { get; set; }
        public string EmgyName2 { get; set; }
        public string EmgyPhone2 { get; set; }
        public Nullable<int> EmgyRelation2 { get; set; }
        public string EmgyMobile2 { get; set; }
    
        public virtual tblPerson tblPerson { get; set; }
    }
}