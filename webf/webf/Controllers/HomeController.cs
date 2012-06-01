using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects.DataClasses;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using webf.Models;
using webf.Models.EntityModel;

namespace webf.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            
            return View();
        }
        [HttpGet]
        public ActionResult List()
        {
            
            Days weekDay = new Days();
            weekDay.CurrentDay = 5;

            return View(weekDay);
        }
        [HttpPost]
        public ActionResult List(FormCollection collection)
        {

            var value = collection["CurrentDay"];

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Orbitr")]
        public ActionResult About()
        {
            return View();
        }

        
        /*public ActionResult KeyGen()
        {
            return View();
        }*/
    }
}
