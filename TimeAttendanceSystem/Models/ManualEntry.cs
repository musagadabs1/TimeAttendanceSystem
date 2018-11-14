using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeAttendanceSystem.Models
{
    public class ManualEntry
    {
        public int Id { get; set; }
        [Display(Name= "Employee ID *")]
        public EmployeeID EmployeeID { get; set; }
        [Display(Name="Name *")]
        public string Name { get; set; }
        [Display(Name ="Terminal ID")]
        public TerminalID TerminalID { get; set; }
        [Display(Name ="Mode")]
        public Mode Mode { get; set; }
        [Display(Name ="Time (HH)")]
        public TimeHH TimeHH { get; set; }
        [Display(Name = "Time (MM)")]
        public TimeMM TimeMM { get; set; }
        [Display(Name ="Date"),DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Display(Name ="Remarks")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }
    }
    public enum EmployeeID
    {
        MusaSule10000065,IbrahimBAuwal10000003,RufaiYusufZakari10000027,SuleimanAbdulrahman10000001
    }
    public enum TerminalID
    {
        Reception1,Library2,ComputerLab3
    }
    public enum Mode
    {
        DAYIN,DAYOUT
    }
    public enum TimeHH
    {
        one,two,three
    }
    public enum TimeMM
    {
        one, two, three
    }
}