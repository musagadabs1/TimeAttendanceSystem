using JQueryAjax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JQueryAjax.Controllers
{
    public class EmployeesController : Controller
    {
        readonly EmployeeDb db = new EmployeeDb();
        // GET: Employees
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetAllEmployee()
        {
            return Json(db.GetEmployees(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddEmployee(Employee employee)
        {
            return Json(db.AddEmployee(employee), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEmployeeById(int id)
        {
            var emp = db.GetEmployees().Find(x => x.Id.Equals(id));
            return Json(emp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateEmployee(Employee emp)
        {
            return Json(db.UpdateEmployee(emp),JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteEmployee(int id)
        {
            return Json(db.DeleteEmployee(id), JsonRequestBehavior.AllowGet);
        }
    }
}