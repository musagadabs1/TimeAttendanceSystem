using DropdownTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DropdownTest.Controllers
{
    public class FruitsController : Controller
    {
        private FruitDBEntities _context = new FruitDBEntities();
        // GET: Fruits
        public ActionResult Index()
        {

            List<Fruit> fruits = PopulateFruits();
            return View(fruits);
        }
        public ActionResult Submit(FormCollection form)
        {
            ViewBag.Message = "Fruit Name: " + form["FruitName"];
            ViewBag.Message += "\\nFruit Id: " + form["FruitId"];

            return RedirectToAction("Index");
        }

        private List<Fruit> PopulateFruits()
        {
            return _context.Fruits.ToList();
        }

        // GET: Fruits/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Fruits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fruits/Create
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

        // GET: Fruits/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Fruits/Edit/5
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

        // GET: Fruits/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Fruits/Delete/5
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
