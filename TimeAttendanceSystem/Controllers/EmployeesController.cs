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
    [Authorize]
    public class EmployeesController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private UNISEntities _context = new UNISEntities();
        
        string msg = string.Empty;
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

        public JsonResult GetEmployeeName(string term)
        {
            try
            {
                var name = _context.SP_GetEmployee(term).ToList();
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

        public ActionResult GetEmployee()
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetEmployee([Bind(Include = "Id,IsActive,MachineId,FirstName,MiddleName,LastName,Department,Designation,StaffType,Company,Shift")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var status = "N";
                    if (employee.IsActive == true)
                    {
                        status = "Y";
                    }
                    var staffType = "S";

                    var branch = 1;
                    var saveRecord = _context.SP_Update_save_EmployeeMaster(employee.Department, employee.Designation, branch, employee.Shift, staffType, status, employee.FirstName, employee.MiddleName, employee.LastName, int.Parse(employee.MachineId));

                    if (saveRecord.FirstOrDefault() == "Success!!")
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
                } //return RedirectToAction("Create");
            }

            return View(employee);
        }
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
