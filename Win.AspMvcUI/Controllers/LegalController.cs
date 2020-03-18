using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Win.AspMvcUI.Controllers
{
    public class LegalController : Controller
    {
        // GET: Legal
        [Route("gizlilik")]
        public ActionResult Privacy()
        {
            return View();
        }
        [Route("sartlarvekosullar")]
        public ActionResult Conditions()
        {
            return View();
        }
        [Route("sss")]
        public ActionResult SSS()
        {
            return View();
        }
    }
}