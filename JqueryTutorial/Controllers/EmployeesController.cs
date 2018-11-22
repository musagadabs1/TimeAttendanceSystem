using JqueryTutorial.Models;
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
        public ActionResult ViewAll()
        {
            return View();
        }
        public ActionResult AddOrEdit(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddOrEdit(Employee employee)
        {
            return View(employee);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}