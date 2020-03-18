using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Win.MvcUI.Helpers.Permissions;

namespace Win.MvcUI.Areas.Admin.Controllers
{
    [RoleControl(Roles = "Admin")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}