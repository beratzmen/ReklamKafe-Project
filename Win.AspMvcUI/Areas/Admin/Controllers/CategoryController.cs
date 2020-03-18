using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Win.Business.Concrete;
using Win.DataAccess.Concrete.EntityFramework.Repository;
using Win.Entities.Model;
using Win.AspMvcUI.Helpers.Permissions;

namespace Win.AspMvcUI.Areas.Admin.Controllers
{
    [RoleControl(Roles = "Admin")]
    public class CategoryController : Controller
    {
        CategoryManager _categoryService = new CategoryManager(new EfCategoryRepository());
        public ActionResult List()
        {
            return View(_categoryService.GetAll());
        }

        public ActionResult Insert()
        {
            return View();
        }

        public ActionResult Update(int id)
        {
            return View(_categoryService.Get(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Category category)
        {
            _categoryService.Update(category);
            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Insert(Category category)
        {
            category.AuditStatus = 1;
            _categoryService.Add(category);
            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            _categoryService.Delete(id);
            return RedirectToAction("List");
        }
    }
}