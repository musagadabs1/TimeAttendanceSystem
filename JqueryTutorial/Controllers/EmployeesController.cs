﻿//using JqueryTutorial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JqueryTutorial.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult ViewAll()
        //{
        //    return View(GetAllEmployee());
        //}
        //IEnumerable<Employee> GetAllEmployee()
        //{
           // using (TestDbEntities _context=new TestDbEntities())
            //{
               // return _context.Employees.ToList<Employee>();
            //}
       // }
        //public ActionResult AddOrEdit(int? id)
        //{
            //var emp = new Employee();
            //if (id !=0)
            //{
                //using (var _context=new TestDbEntities())
                //{
                //    emp = _context.Employees.Where(e => e.Id == id).FirstOrDefault<Employee>();
                //}
            //}
            //return View(emp);
        //}
        //[HttpPost]
        //public ActionResult AddOrEdit(Employee employee)
        //{
        //    return View(employee);
        //}

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            return View();
        }
    }
}