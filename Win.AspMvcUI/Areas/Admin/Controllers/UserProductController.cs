using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Win.Business.Concrete;
using Win.DataAccess.Concrete.EntityFramework.Repository;
using Win.AspMvcUI.Helpers.Permissions;
using Win.Entities.Dto;

namespace Win.AspMvcUI.Areas.Admin.Controllers
{
    [RoleControl(Roles = "Admin")]
    public class UserProductController : Controller
    {
        UserProductManager _userProductService = new UserProductManager(new EfUserProductRepository());

        // GET: Admin/UserProduct
        public ActionResult Index()
        {
            return View(_userProductService.GetUserProduct());
        }



        public ActionResult Update(int id)
        {
            return View(_userProductService.GetUserProduct(id));
        }
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Update(UserProductViewModel model)
        {
            var product = _userProductService.Get(model.Id);
            product.Status = model.Status;
            _userProductService.Update(product);
            return RedirectToAction("Index");
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