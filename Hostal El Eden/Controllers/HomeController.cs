using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hostal_El_Eden.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View("Index", "_Layout");
        }

        public ActionResult Gallery()
        {
            return View("Gallery", "_NonHomePage");
        }
        public ActionResult Contact()
        {
            return View("Contact", "_NonHomePage");
        }

        public ActionResult Banos()
        {
            return View("Banos", "_NonHomePage");
        }

        public ActionResult Restaurant()
        {
            return View("Restaurant", "_NonHomePage");
        }
    }
}
