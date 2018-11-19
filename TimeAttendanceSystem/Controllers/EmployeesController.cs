using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TimeAttendanceSystem.Models;

namespace TimeAttendanceSystem.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UNISEntities _UnisContext = new UNISEntities();
        private PayrollEntities _PayRollContext = new PayrollEntities();

        // GET: Employees
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.Departments = new SelectList(_UnisContext.SP_GetDepartment(), "Id", "Department");
            ViewBag.Designations = new SelectList(_UnisContext.SP_Designation(), "Id", "Designation");
            ViewBag.Companies = new SelectList(_UnisContext.SP_CompanyDetails(), "company_id", "company_name");
            ViewBag.Shifts = new SelectList(_UnisContext.SP_Shift(), "Shift_ID", "Shift_Type");
            return View();
        }
        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IsActive,MachineId,FirstName,MiddleName,LastName,Department,Designation,StaffType,Company,Shift")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View(employee);
        }
        public PartialViewResult GetShiftDetail(int? shiftId)
        {
            //var tasUtility = new TASUtility();
            List<ShiftDetail> shfDetails = new List<ShiftDetail>();
            var shiftDetails = TASUtility.GetShiftSchedule(shiftId);

            foreach (var item in shiftDetails)
            {
                shfDetails.Add(new ShiftDetail
                {
                    Code = item.Code,
                    Title=item.Title,
                    StartTime=item.StartTime,
                    EndTime=item.EndTime,
                    Tolerence=item.Tolerence,
                    Type=item.Type,
                    NextDay=item.NextDay
                });
            }

            ViewBag.ShiftDetails = shfDetails;

            return PartialView("_ShiftDetailsPartialView.cshtml", shfDetails);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IsActive,MachineId,FirstName,MiddleName,LastName,Department,Designation,StaffType,Company,Shift")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
