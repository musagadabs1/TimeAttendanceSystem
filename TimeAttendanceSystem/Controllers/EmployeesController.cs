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
    //[Authorize]
    public class EmployeesController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private UNISEntities _context = new UNISEntities();
        //private PayrollEntities _PayRollContext = new PayrollEntities();

        //GET: Employees
        public ActionResult Index()
        {

            return View();
        }
        //private long aVeryBigSum(long[] ar)
        //{
        //    long result = 0;
        //    for (var i = 0; i < ar.Length; i++)
        //    {
        //        result += ar[i];
        //    }
        //    return result;

        //}

        // GET: Employees/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Employee employee = db.Employees.Find(id);
        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employee);
        //}
        string msg = string.Empty;
        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.Departments = new SelectList(_context.GetDepartmentWithDeptId(), "deptID", "Department");
            ViewBag.Designations = new SelectList(_context.SP_Designation(), "Id", "Designation");
            ViewBag.Companies = new SelectList(_context.SP_CompanyDetails(), "company_id", "company_name");
            ViewBag.Shifts = new SelectList(_context.SP_Shift(), "Shift_ID", "Shift_Type");
            //ViewBag.EmployeeNames = new SelectList(_context.SP_GetEmployeeNameAndMachineAndEmpNumber(), "MachineCode", "EmpData");
            ViewBag.Employees = new SelectList(_context.SP_GetEmployeeNameAndMachineAndEmpNumber(), "Id", "EmpData");
            var staffType = new List<SelectListItem>
            {
                new SelectListItem {Value = "Staff", Text = "Staff" },
                new SelectListItem{Value = "Faculty", Text = "Faculty"},
                new SelectListItem{Value = "Other", Text = "Other"}
            };
            ViewBag.StaffType = staffType;
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
                try
                {
                    var status = "N";
                    if (employee.IsActive==true)
                    {
                        status = "Y";
                    }
                    var staffType = "S";
                    
                    var branch = 1;
                    var saveRecord = _context.SP_Update_save_EmployeeMaster(employee.Department, employee.Designation,branch, employee.Shift, staffType, status, employee.FirstName, employee.MiddleName, employee.LastName,  int.Parse(employee.MachineId));

                    if (saveRecord.FirstOrDefault()== "Success!!")
                    {
                        msg = "Employee Record Updated Successfully.";
                        //_context.SaveChanges();
                        ViewBag.Message = msg;
                        return PartialView("~/Views/_MessagePartialView.cshtml");
                    }
                    else
                    {
                        ViewBag.Message = "Error has occured. Check and try again.";
                        return PartialView("~/Views/_MessagePartialView.cshtml");
                    }

        
                }
                catch (Exception ex)
                {

                    ViewBag.Message = ex.Message;
                    return PartialView("~/Views/_MessagePartialView.cshtml");
                }


                //db.Employees.Add(employee);
                //db.SaveChanges();
                //return RedirectToAction("Create");
            }

            return View(employee);
        }
        public JsonResult GetShiftDetail(int? shiftId)
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

            return Json(shfDetails,JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmployeeName(string prefix)
        {
            try
            {
                var name = _context.SP_GetEmployee(prefix).FirstOrDefault();
                if (name !=null && name.Count()>0)
                {
                    return Json(name, JsonRequestBehavior.AllowGet);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public JsonResult GetEmployeeDetailsByMachineCode(int machineCode)
        {
            try
            {
                EmployeeViewModel employee = new EmployeeViewModel();

                var getEmployee = _context.SP_EmployeeEdit_Complete_Deatils(machineCode).FirstOrDefault();
                if (getEmployee !=null)
                {
                    employee.BranchId =int.Parse( getEmployee.Branch_Id.ToString());
                    employee.DepartmentId = int.Parse(getEmployee.E_DEPT_ID.ToString());
                    employee.DesignationId = int.Parse(getEmployee.E_DES_ID.ToString());
                    employee.EmployeeType = getEmployee.E_EType;
                    employee.FirstName = getEmployee.First_Name;
                    employee.LastName = getEmployee.Last_Name;
                    employee.MiddleName = getEmployee.Middle_Name;
                    employee.ShiftId=int.Parse(getEmployee.Shift_Id.ToString());
                    employee.Status = getEmployee.E_STATUS;
                    employee.MachineCode = int.Parse(getEmployee.L_UID.ToString());
                }
                return Json(employee, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public JsonResult GetDesignationByDeptId(int deptId)
        {
            try
            {
                List<DesignationViewModel> designationViews = new List<DesignationViewModel>();
                var designations = _context.GetDesignationByDeptId(deptId).ToList();
                if (designations != null && designations.Count() > 0)
                {
                    foreach (var item in designations)
                    {
                        designationViews.Add(new DesignationViewModel
                        {
                            id=item.id,
                            Designation=item.Designation
                        });
                    }

                    return Json(designationViews, JsonRequestBehavior.AllowGet);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // GET: Employees/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Employee employee = db.Employees.Find(id);
        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employee);
        //}

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,IsActive,MachineId,FirstName,MiddleName,LastName,Department,Designation,StaffType,Company,Shift")] Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(employee).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(employee);
        //}

        // GET: Employees/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Employee employee = db.Employees.Find(id);
        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employee);
        //}

        // POST: Employees/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Employee employee = db.Employees.Find(id);
        //    db.Employees.Remove(employee);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
