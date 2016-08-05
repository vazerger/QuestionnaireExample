using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuestionnaireExample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Result()
        {
            return PartialView("_result");
        }

        public ActionResult Report()
        {
            return PartialView("_report");
        }

        public ActionResult myReport()
        {
            return PartialView("_myreport");
        }

        public ActionResult YourName()
        {
            return PartialView("_name");
        }

        public ActionResult Config()
        {
            return PartialView("_config");
        }  
        
    }
}