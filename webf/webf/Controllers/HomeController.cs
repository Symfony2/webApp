using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult UserRegistrationBlank()
        {
            RegForm _blank = new RegForm();
            ViewData.Model = _blank;
            return View();
        }
        [HttpPost]
        public ActionResult UserRegistrationBlank(RegForm obj)
        {
            RegForm _blank = new RegForm();
            ViewData.Model = _blank;
            return View();
        }
        /*public ActionResult KeyGen()
        {
            return View();
        }*/
    }
}
