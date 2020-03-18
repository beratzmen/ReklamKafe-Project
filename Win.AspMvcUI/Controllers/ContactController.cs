using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Win.Business.Concrete;
using Win.DataAccess.Concrete.EntityFramework.Repository;
using Win.Entities.Model;

namespace Win.AspMvcUI.Controllers
{
    public class ContactController : Controller
    {
        ContactManager _contactService = new ContactManager(new EfContactRepository());

        [Route("iletisim")]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SendMail(Contact contactInformation)
        {
            _contactService.Add(contactInformation);
            return Json("");
        }
    }
}