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
    
    public partial class Tb_EmployeeMaster
    {
        public int E_ID { get; set; }
        public Nullable<int> E_BRANCH_ID { get; set; }
        public Nullable<int> E_DEPT_ID { get; set; }
        public Nullable<int> E_DES_ID { get; set; }
        public Nullable<int> L_UID { get; set; }
        public Nullable<int> E_SHIFT_ID { get; set; }
        public string E_CODE { get; set; }
        public string E_NATIONAL_ID { get; set; }
        public string E_TITLE { get; set; }
        public string E_FIRST_NAME { get; set; }
        public string E_MIDDLE_NAME { get; set; }
        public string E_LAST_NAME { get; set; }
        public string E_GENDER_ID { get; set; }
        public Nullable<int> E_AGE { get; set; }
        public Nullable<System.DateTime> E_DATE_OF_BIRTH { get; set; }
        public string E_PLACE_OF_BIRTH { get; set; }
        public string E_REGION_OF_BIRTH { get; set; }
        public string E_COUNTRY_OF_BIRTH { get; set; }
        public string E_STATUS { get; set; }
        public Nullable<System.DateTime> E_EFFECTIVE_FROM { get; set; }
        public Nullable<System.DateTime> E_EFFECTIVE_TO { get; set; }
        public Nullable<System.DateTime> E_REJOIN { get; set; }
        public string E_REASON_END { get; set; }
        public string E_MARITAL_STATUS_ID { get; set; }
        public string E_EType { get; set; }
        public string ATTRIBUTE2 { get; set; }
        public string ATTRIBUTE3 { get; set; }
        public string ATTRIBUTE4 { get; set; }
        public string ATTRIBUTE5 { get; set; }
        public string ATTRIBUTE6 { get; set; }
        public string ATTRIBUTE7 { get; set; }
        public string ATTRIBUTE8 { get; set; }
        public string ATTRIBUTE9 { get; set; }
        public string ATTRIBUTE10 { get; set; }
        public string E_NOTICEDATE { get; set; }
        public Nullable<int> E_CREATED_BY_ID { get; set; }
        public Nullable<System.DateTime> E_CREATION_TIME { get; set; }
        public Nullable<int> E_UPDATED_BY_ID { get; set; }
        public Nullable<System.DateTime> E_UPDATION_TIME { get; set; }
        public byte[] E_PERSONPHOTO { get; set; }
        public string E_REMARKS { get; set; }
        public Nullable<System.DateTime> E_CREATEDON { get; set; }
        public string E_HOST_N { get; set; }
        public Nullable<bool> E_ALLOWTOREGISTER { get; set; }
    }
}
