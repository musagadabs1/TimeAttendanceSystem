﻿using System;
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
    public class EntryChecksController : Controller
    {
        private UNISEntities _context = new UNISEntities();

        // GET: EntryChecks
        //public ActionResult Index()
        //{
        //    return View(db.EntryChecks.ToList());
        //}

        // GET: EntryChecks/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    EntryCheck entryCheck = db.EntryChecks.Find(id);
        //    if (entryCheck == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(entryCheck);
        //}

        // GET: EntryChecks/Create
        public ActionResult Create()
        {
            ViewBag.Terminals = new SelectList(_context.SP_GetTerminal(), "L_id", "c_name");
            ViewBag.Employees = new SelectList(_context.SP_GetEmployee_Names(0, ""), "id", "Employee_Name");

            var mode = new List<SelectListItem>
            {
                new SelectListItem {Value = "01", Text = "DAY-IN" },
                new SelectListItem{Value = "02", Text = "DAY-OUT"}
            };
            ViewBag.Mode = mode;
            var timeHH = new List<SelectListItem>
            {
               new SelectListItem{ Value = "00",Text ="00"},
               new SelectListItem{Value = "01",Text = "01"},
               new SelectListItem{Value = "02", Text = "02"},
               new SelectListItem{Value = "03", Text = "03"},
               new SelectListItem{Value = "04", Text = "04"},
               new SelectListItem{Value = "05", Text = "05"},
               new SelectListItem{Value = "06", Text = "06"},
               new SelectListItem{Value = "07", Text = "07"},
               new SelectListItem{Value = "08", Text = "08"},
               new SelectListItem{Value = "09", Text = "09" },
               new SelectListItem{Value = "10", Text = "10"},
               new SelectListItem{Value = "11", Text = "11"},
               new SelectListItem{Value = "12", Text = "12"},
               new SelectListItem {Value = "13", Text = "13"},
                new SelectListItem{Value = "14", Text = "14"},
                new SelectListItem{Value = "15", Text = "15" },
                new SelectListItem{Value = "16", Text = "16"},
               new SelectListItem{Value = "17", Text = "17"},
               new SelectListItem{Value = "18", Text = "18"},
              new SelectListItem{Value = "19", Text = "19"},
              new SelectListItem{Value = "20", Text = "20"},
             new SelectListItem{Value = "21", Text = "21"},
            new SelectListItem{Value = "22", Text = "22"},
           new SelectListItem{Value = "23", Text = "23"}
            };
            ViewBag.TimeHH = timeHH;

            var timeMM = new List<SelectListItem>
            {
                new SelectListItem{Value = "00", Text = "00"},
                new SelectListItem{Value = "01", Text = "01"},
                new SelectListItem{Value = "02", Text = "02"},
                new SelectListItem{Value = "03", Text = "03"},
                new SelectListItem{Value = "04", Text = "04"},
                new SelectListItem{Value = "05", Text = "05"},
                new SelectListItem{Value = "06", Text = "06"},
                new SelectListItem{ Value = "07", Text="07"},
                new SelectListItem{ Value = "08", Text="08"},
                new SelectListItem{ Value = "09", Text="09"},
                new SelectListItem{ Value = "10", Text="10"},
                new SelectListItem{Value = "11", Text="11"},
                new SelectListItem{Value = "12", Text = "12"},
                new SelectListItem{Value = "13", Text="13"},
                new SelectListItem{Value = "14", Text="14"},
                new SelectListItem{Value = "15" ,Text="15"},
                new SelectListItem{Value = "16", Text="16"},
                new SelectListItem{Value = "17" ,Text="17"},
                new SelectListItem{Value = "18", Text="18"},
                new SelectListItem{Value = "19" ,Text="19"},
                new SelectListItem{Value = "20" ,Text="20"},
                new SelectListItem{Value = "21", Text="21"},
                new SelectListItem{Value = "22", Text="22"},
                new SelectListItem{Value = "23", Text="23"},
                new SelectListItem{Value = "24", Text="24"},
                new SelectListItem{Value = "25" ,Text="25"},
                new SelectListItem{Value = "26" ,Text="26"},
                new SelectListItem{Value = "27" ,Text="27"},
                new SelectListItem{Value = "28", Text="28"},
                new SelectListItem{Value = "29", Text="29"},
                new SelectListItem{Value = "30", Text="30"},
                new SelectListItem{Value = "31", Text="31"},
                new SelectListItem{Value = "32",Text="32"},
                new SelectListItem{Value = "33" ,Text="33"},
                new SelectListItem{Value = "34", Text="34"},
                new SelectListItem{ Value = "35", Text="35"},
                new SelectListItem{Value = "36", Text="36"},
                new SelectListItem{Value = "37", Text="37"},
                new SelectListItem{Value = "38", Text="38"},
                new SelectListItem{Value = "39", Text="39"},
                new SelectListItem{Value = "40", Text="40"},
                new SelectListItem{Value = "41", Text="41"},
                new SelectListItem{Value = "42", Text="42"},
                new SelectListItem{Value = "43", Text="43"},
                new SelectListItem{Value = "44", Text="44"},
                new SelectListItem{Value = "45", Text="45"},
                new SelectListItem{Value = "46", Text="46"},
                new SelectListItem{Value = "47", Text="47"},
                new SelectListItem{Value = "48", Text = "48"},
                new SelectListItem{Value = "49", Text="49"},
                new SelectListItem{Value="50", Text="50"},
                new SelectListItem{Value="51", Text="51"},
                new SelectListItem{Value="52" ,Text="52"},
                new SelectListItem{Value="53", Text="53"},
                new SelectListItem{Value="54", Text="54"},
                new SelectListItem{Value="55", Text="55"},
                new SelectListItem{Value="56", Text="56"},
                new SelectListItem{Value="57" ,Text="57"},
                new SelectListItem{Value="58", Text="58"},
                new SelectListItem{Value="59" ,Text="59"}

            };
            ViewBag.TimeMM = timeMM;
            return View();
        }

        // POST: EntryChecks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,EmployeeId,TerminalID,Mode,TimeHH,TimeMM,DateEntry")] EntryCheck entryCheck)
        {
            if (ModelState.IsValid)
            {
                //db.EntryChecks.Add(entryCheck);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(entryCheck);
        }

        // GET: EntryChecks/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    EntryCheck entryCheck = db.EntryChecks.Find(id);
        //    if (entryCheck == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(entryCheck);
        //}

        // POST: EntryChecks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Date,EmployeeId,TerminalID,Mode,TimeHH,TimeMM,DateEntry")] EntryCheck entryCheck)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(entryCheck).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(entryCheck);
        //}

        // GET: EntryChecks/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    EntryCheck entryCheck = db.EntryChecks.Find(id);
        //    if (entryCheck == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(entryCheck);
        //}

        // POST: EntryChecks/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    EntryCheck entryCheck = db.EntryChecks.Find(id);
        //    db.EntryChecks.Remove(entryCheck);
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