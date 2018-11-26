using JqueryTutorial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JqueryTutorial.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Index(string prefix)
        {
            List<City> cities = new List<City>() {

                new City{Id=1,CityName="Gadabuke"},
                new City{Id=2,CityName="Nasarawa"},
                new City{Id=3,CityName="kano"},
                new City{Id=4,CityName="Abuja"}
            };
            //cities.Add(new City { Id = 1, CityName = "Test" });
            var cityList = (from n in cities where n.CityName.StartsWith(prefix) select new { n.CityName });

            return Json(cityList,JsonRequestBehavior.AllowGet);
        }
        // Complete the compareTriplets function below.
        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            //List<int> a = new List<int>();
            //a.Count();


            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}