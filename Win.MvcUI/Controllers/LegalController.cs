using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Win.MvcUI.Controllers
{
    public class LegalController : Controller
    {
        // GET: Legal
        public ActionResult Privacy()
        {
            return View();
        }
        public ActionResult Conditions()
        {
            return View();
        }
        public ActionResult SSS()
        {
            return View();
        }
    }
}