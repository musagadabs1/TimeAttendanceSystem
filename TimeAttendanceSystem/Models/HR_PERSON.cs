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
    
    public partial class HR_PERSON
    {
        public int PERSON_ID { get; set; }
        public string PERSON_NUMBER { get; set; }
        public string NATIONAL_ID { get; set; }
        public string TITLE { get; set; }
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string GENDER_ID { get; set; }
        public Nullable<int> AGE { get; set; }
        public Nullable<System.DateTime> DATE_OF_BIRTH { get; set; }
        public string PLACE_OF_BIRTH { get; set; }
        public string REGION_OF_BIRTH { get; set; }
        public string COUNTRY_OF_BIRTH { get; set; }
        public string STATUS { get; set; }
        public Nullable<System.DateTime> EFFECTIVE_FROM { get; set; }
        public Nullable<System.DateTime> EFFECTIVE_TO { get; set; }
        public Nullable<System.DateTime> REJOIN { get; set; }
        public string REASON_END { get; set; }
        public string MARITAL_STATUS_ID { get; set; }
        public string ATTRIBUTE1 { get; set; }
        public string ATTRIBUTE2 { get; set; }
        public string ATTRIBUTE3 { get; set; }
        public string ATTRIBUTE4 { get; set; }
        public string ATTRIBUTE5 { get; set; }
        public string ATTRIBUTE6 { get; set; }
        public string ATTRIBUTE7 { get; set; }
        public string ATTRIBUTE8 { get; set; }
        public string ATTRIBUTE9 { get; set; }
        public string ATTRIBUTE10 { get; set; }
        public string NOTICEDATE { get; set; }
        public Nullable<int> CREATED_BY_ID { get; set; }
        public Nullable<System.DateTime> CREATION_TIME { get; set; }
        public Nullable<int> UPDATED_BY_ID { get; set; }
        public Nullable<System.DateTime> UPDATION_TIME { get; set; }
        public byte[] PERSONPHOTO { get; set; }
        public string REMARKS { get; set; }
        public Nullable<System.DateTime> CREATEDON { get; set; }
        public string HOST_N { get; set; }
    }
}