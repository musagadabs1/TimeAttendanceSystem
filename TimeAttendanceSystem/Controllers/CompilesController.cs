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
    public class CompilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UNISEntities _context = new UNISEntities();

        // GET: Compiles
        public ActionResult Index()
        {
            return View(db.Compiles.ToList());
        }

        // GET: Compiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compile compile = db.Compiles.Find(id);
            if (compile == null)
            {
                return HttpNotFound();
            }
            return View(compile);
        }

        private string GetLastCompiledDate()
        {
            string compiledDate = string.Empty;
            var dates = _context.SP_Get_LastCompiledDate();
            if (dates != null)
            {
                compiledDate = dates.FirstOrDefault().Date;
            }
            return compiledDate;
        }
        string errorMgs = string.Empty;
        private bool IsCompile(string date)
        {
            bool complied = true;
            var result = _context.SP_Is_Fianalized(date);
            if (result.FirstOrDefault() == 1)
            {
                errorMgs = "Can't compiled a Finalized Attendance";
                return complied;

            }
            else
            {

            }

            return complied;
        }

        // GET: Compiles/Create
        public ActionResult Create()
        {
            ViewBag.LastCompiledDate = GetLastCompiledDate();
            return View();
        }

        // POST: Compiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SelectDate,LastCompiled")] Compile compile)
        {
            if (ModelState.IsValid)
            {
                //string dateSelected;
                string selectedDate = compile.SelectDate.Date.ToShortDateString();
                string[] selectDateArray = selectedDate.Split('/');
                int d = int.Parse(selectDateArray[1]);
                int m = int.Parse(selectDateArray[0]);
                int yy = int.Parse(selectDateArray[2]);

                string dd = string.Empty;
                string mm = string.Empty;

                if (d>=10)
                {
                    dd = d.ToString();
                }
                else
                {
                    dd = "0" + d.ToString();
                }

                if (m>=10)
                {
                    mm = m.ToString();
                }
                else
                {
                    mm = "0" + m.ToString();
                }
                var oDate = yy.ToString()+mm.ToString()+dd.ToString();
                var vDate= yy.ToString() + mm.ToString() + dd.ToString();
          

                /*
                 
                 function IsCompile(MethodName, id, ODate) {


    $.ajax({
        type: "Get",
        Contenttype: "application/json",
        url: base_url +"Handler.ashx?method=" + MethodName + "&id=" + id + "",
        cache: false,
        success: function (data) {
            if (data.status == true) {
                if (data.response[0].IsCompiled = "0") {
                   
                    ValidateEarlyDateAttendance(id, ODate)
                    
                }
                else {
                    $('#loading').hide();
                    alert("You can not compile attendance for selected date as it is already Finalized");
                    return false;
                }
            }
        }
    });
}
function ValidateEarlyDateAttendance(VDate, ODate) {

    if (VDate > ODate) {
        alert("You can not compile today`s or of later date attendance.");
        return false;
    }
    else {
       
        $.ajax({
            type: "Get",
            Contenttype: "application/json",
            url: base_url +"Handler.ashx?method=RRcompile&VDate="+VDate+"&ODate="+ODate,
            cache: false,
            success: function (data) {
                if (data.status == true) {
                    $('#loading').hide();
                    alert("Compiled Successfully");
                }
                else {
                    alert(data.d);

                }
            }
        });
    }
}
                 
                 
                 
                 
                 */



                db.Compiles.Add(compile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(compile);
        }

        // GET: Compiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compile compile = db.Compiles.Find(id);
            if (compile == null)
            {
                return HttpNotFound();
            }
            return View(compile);
        }

        // POST: Compiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SelectDate,LastCompiled")] Compile compile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(compile);
        }

        // GET: Compiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compile compile = db.Compiles.Find(id);
            if (compile == null)
            {
                return HttpNotFound();
            }
            return View(compile);
        }

        // POST: Compiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compile compile = db.Compiles.Find(id);
            db.Compiles.Remove(compile);
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
