using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DropdownTest.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmpName { get; set; }
        public double Salary { get; set; }
        public List<Employee> GetEmployees { get; set; }
        public IEnumerable<SelectListItem> EmpListItems
        {
            get
            {
                return new SelectList(GetEmployees, "EmployeeId", "EmpName");
            }
                
        }
    }
}