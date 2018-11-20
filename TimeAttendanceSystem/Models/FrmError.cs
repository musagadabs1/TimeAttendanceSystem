using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeAttendanceSystem.Models
{
    public class FrmError
    {
        public string EMPID { get; set; }
        public String Name { get; set; }
        public String Count { get; set; }
        public String Terminal { get; set; }
        public String Time { get; set; }
        public String Mode { get; set; }
        public String Status { get; set; }
        public int L_id { get; set; }
        public String c_name { get; set; }
        public String VDATE { get; set; }
        public String Remarks { get; set; }
        public String LoginUser { get; set; }
        public String Operation { get; set; }
        public String SendMail { get; set; }
    }
}