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
    
    public partial class TB_MainMenu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_MainMenu()
        {
            this.HR_AccessMaster = new HashSet<HR_AccessMaster>();
        }
    
        public int ID { get; set; }
        public string Category { get; set; }
        public string URL { get; set; }
        public Nullable<bool> Isactive { get; set; }
        public Nullable<int> Order { get; set; }
        public string Class { get; set; }
        public string CategoryDesc { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedIP { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedIP { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HR_AccessMaster> HR_AccessMaster { get; set; }
    }
}
