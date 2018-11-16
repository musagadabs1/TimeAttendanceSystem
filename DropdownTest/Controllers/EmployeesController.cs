using DropdownTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DropdownTest.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        public ActionResult Index()
        {
            var empModel = new Employee
            {
                GetEmployees = GetEmployeesFromDB()
            };
            empModel.EmployeeId = empModel.GetEmployees.First().EmployeeId;
            empModel.EmpName = empModel.GetEmployees.First().EmpName;
            empModel.Salary = empModel.GetEmployees.First().Salary;
            return View(empModel);

            //return View();
        }
        public PartialViewResult GetEmpRecord(int EmployeeId)
        {
            var emp = new Employee
            {
                GetEmployees = GetEmployeesFromDB()
            };
            var em = emp.GetEmployees.Where(e => e.EmployeeId == EmployeeId).FirstOrDefault();

            // Set Default Employee Record
            emp.EmployeeId = em.EmployeeId;
            emp.EmpName = em.EmpName;
            emp.Salary = em.Salary;

            return PartialView("_EmpPartial", emp);


        }

        // GET: Employees/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Employee()
        {
            var empModel = new Employee
            {
                GetEmployees = GetEmployeesFromDB()
            };
            empModel.EmployeeId = empModel.GetEmployees.First().EmployeeId;
            empModel.EmpName = empModel.GetEmployees.First().EmpName;
            empModel.Salary = empModel.GetEmployees.First().Salary;
            return View(empModel);

        }

        [HttpPost]
        public ActionResult Employee(Employee employee)
        {
            var emp = new Employee();
            emp.GetEmployees = GetEmployeesFromDB();
            var empl = emp.GetEmployees.Where(em => em.EmployeeId == employee.EmployeeId).FirstOrDefault();
            emp.EmployeeId = empl.EmployeeId;
            emp.EmpName = empl.EmpName;
            emp.Salary = empl.Salary;

            return View(emp);
        }

        private List<Employee> GetEmployeesFromDB()
        {
            var empList = new List<Employee>();
            var emp1 = new Employee();
            emp1.EmployeeId = 1;
            emp1.EmpName = "Musa Sule";
            emp1.Salary = 600000;

            var emp2 = new Employee();
            emp2.EmployeeId = 2;
            emp2.EmpName = "Ibrahim Sule";
            emp2.Salary = 50000;

            var emp3 = new Employee();
            emp3.EmployeeId = 3;
            emp3.EmpName = "Usman Sule";
            emp3.Salary = 5000;

            empList.Add(emp1);
            empList.Add(emp2);
            empList.Add(emp3);


            return empList;

        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employees/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
