using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Win.Business;
using Win.Business.Concrete;
using Win.DataAccess.Concrete.EntityFramework.Repository;
using Win.Entities;
using Win.Entities.Enum;
using Win.Entities.Model;
using Win.MvcUI.Areas.Admin.Models;
using Win.MvcUI.Helpers.Permissions;
using Win.MvcUI.Models;

namespace Win.MvcUI.Areas.Admin.Controllers
{
    [RoleControl(Roles = "Admin")]
    public class ProductController : Controller
    {
        ProductManager _productService = new ProductManager(new EfProductRepository());
        CategoryManager _categoryService = new CategoryManager(new EfCategoryRepository());
        ProductCategoryManager _productCategoryService = new ProductCategoryManager(new EfProductCategoryRepository());
        public ActionResult List()
        {
            ProductListViewModel model = new ProductListViewModel();
            model.Product = _productService.GetAll();
            return View(model);
        }

        public ActionResult Insert()
        {
            return View(_categoryService.GetAll());
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Insert(HttpPostedFileBase upload1, HttpPostedFileBase upload2, HttpPostedFileBase upload3, ProductAddViewModel productVM)
        {
            if (upload1 != null)
            {
                WebImage img = new WebImage(upload1.InputStream);
                FileInfo fotoInfo = new FileInfo(upload1.FileName);
                string newFoto = Guid.NewGuid().ToString() + fotoInfo.Extension;
                img.Resize(1920, 1080);
                img.Save("~/Uploads/Product/" + newFoto);
                productVM.product.Upload1 = "/Uploads/Product/" + newFoto;
            }
            if (upload2 != null)
            {
                WebImage img = new WebImage(upload2.InputStream);
                FileInfo fotoInfo = new FileInfo(upload2.FileName);
                string newFoto = Guid.NewGuid().ToString() + fotoInfo.Extension;
                img.Resize(1920, 1080);
                img.Save("~/Uploads/Product/" + newFoto);
                productVM.product.Upload2 = "/Uploads/Product/" + newFoto;
            }

            if (upload3 != null)
            {
                WebImage img = new WebImage(upload3.InputStream);
                FileInfo fotoInfo = new FileInfo(upload3.FileName);
                string newFoto = Guid.NewGuid().ToString() + fotoInfo.Extension;
                img.Resize(1920, 1080);
                img.Save("~/Uploads/Product/" + newFoto);
                productVM.product.Upload3 = "/Uploads/Product/" + newFoto;
            }
            var productId = _productService.Add(productVM.product);
            if (productId > 0)
                _productCategoryService.Add(new ProductCategory() { ProductID = productId, CategoryID = productVM.categoryId });
            return View(_categoryService.GetAll());
        }

        public ActionResult Update(int id)
        {
            var model = _productService.Get(id);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Update(HttpPostedFileBase upload1, HttpPostedFileBase upload2, HttpPostedFileBase upload3, Product product)
        {
            var dbProduct = _productService.Get(product.Id);
            if (dbProduct != null)
            {
                if (upload1 != null)
                {
                    string filepath = Path.GetFileName(dbProduct.Upload1);
                    var location = Path.Combine("/Uploads/Product/" + filepath);
                    upload1.SaveAs(Server.MapPath("~" + location));
                    product.Upload1 = location;
                }
                if (upload2 != null)
                {
                    string filepath = Path.GetFileName(dbProduct.Upload2);
                    var location = Path.Combine("/Uploads/Product/" + filepath);
                    upload2.SaveAs(Server.MapPath("~" + location));
                    product.Upload2 = location;
                }
                if (upload3 != null)
                {
                    string filepath = Path.GetFileName(dbProduct.Upload3);
                    var location = Path.Combine("/Uploads/Product/" + filepath);
                    upload3.SaveAs(Server.MapPath("~" + location));
                    product.Upload3 = location;
                }
            }
            _productService.Update(product);
            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            _productService.Delete(id);
            return RedirectToAction("List");
        }
    }
}