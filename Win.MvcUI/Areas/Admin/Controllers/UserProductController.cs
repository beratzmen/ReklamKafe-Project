using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Win.Business.Concrete;
using Win.DataAccess.Concrete.EntityFramework.Repository;
using Win.MvcUI.Helpers.Permissions;

namespace Win.MvcUI.Areas.Admin.Controllers
{
    [RoleControl(Roles = "Admin")]
    public class UserProductController : Controller
    {
        UserProductManager _userProductService = new UserProductManager(new EfUserProductRepository());

        // GET: Admin/UserProduct
        public ActionResult Index()
        {
            return View(_userProductService.GetAll());
        }
        public ActionResult ProcessCompleted(int id)
        {
            _userProductService.ProcessCompleted(id);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            _userProductService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}