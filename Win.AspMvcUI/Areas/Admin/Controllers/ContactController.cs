using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Win.Business.Concrete;
using Win.DataAccess.Concrete.EntityFramework.Repository;
using Win.AspMvcUI.Helpers.Permissions;

namespace Win.AspMvcUI.Areas.Admin.Controllers
{
    [RoleControl(Roles = "Admin")]
    public class ContactController : Controller
    {
        ContactManager _contactService = new ContactManager(new EfContactRepository());

        // GET: Admin/Contact
        public ActionResult Index()
        {
            return View(_contactService.GetAll());
        }
        public ActionResult ProcessCompleted(int id)
        {
            _contactService.ProcessCompleted(id);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            _contactService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}