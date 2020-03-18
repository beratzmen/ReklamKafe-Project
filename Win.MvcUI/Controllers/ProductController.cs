using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Win.Business;
using Win.Business.Concrete;
using Win.DataAccess.Concrete.EntityFramework.Repository;
using Win.Entities.Model;
using Win.MvcUI.Helpers.Util;
using Win.MvcUI.Models;

namespace Win.MvcUI.Controllers
{
    public class ProductController : Controller
    {
        ProductManager _productService = new ProductManager(new EfProductRepository());
        UserAdressInformationManager _userAdressInformationService = new UserAdressInformationManager(new EfUserAdressInformationRepository());
        UserProductManager _userProductService = new UserProductManager(new EfUserProductRepository());
        CategoryManager _categoryService = new CategoryManager(new EfCategoryRepository());
        UserInfoManager _userInfoService = new UserInfoManager(new EfUserInfoRepository());


        public ActionResult Index()
        {
            ProductCategoryListViewModel model = new ProductCategoryListViewModel();
            model.Product = _productService.GetAll().OrderByDescending(x => x.CreatedDate).ToList();
            model.Category = _categoryService.GetAll();
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var purchasers = _userProductService.GetUsersByProductId(id);
            if (purchasers != null)
            {
                for (int i = 0; i < purchasers.ToList().Count; i++)
                {
                    int count = purchasers.Count(p => p.Equals(purchasers[i]));
                    if (count > 1)
                    {
                        string key = purchasers[i];
                        purchasers.RemoveAll(p => p.Equals(purchasers[i]));
                        purchasers.Add(key + " (" + count + " adet)");
                    }
                }
            }
            return View(new Tuple<Product, List<string>>(_productService.Get(id), purchasers));
        }

        /// <summary>
        /// Ürün için adres bilgilerini alıp onaylatma işlemi.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Confirmation(int id)
        {
            ConfirmationListViewModel model = new ConfirmationListViewModel();
            if (Session["userId"] == null)
                return RedirectToAction("Login", "User");
            else
                model.UserAdressInformation = _userAdressInformationService.Get(Convert.ToInt32(Session["userId"]));
            model.Product = _productService.Get(id);
            if (!model.Product.IsActive || model.Product.Amount <= 0)
                return RedirectToAction("Details", new { id = id });
            model.UserInfo = _userInfoService.Get(Convert.ToInt32(Session["userId"]));
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirmation(UserAdressInformation userAdressInformation, UserProduct userProduct)
        {
            ConfirmationListViewModel model = new ConfirmationListViewModel();
            model.Product = _productService.Get(userProduct.ProductID);
            model.UserInfo = _userInfoService.Get(userProduct.UserID);
            if (model.UserInfo.Point < model.Product.Price)
                return RedirectToAction("Details", new { id = userProduct.ProductID });
            if (_userAdressInformationService.Get(userAdressInformation.UserID) == null)
                _userAdressInformationService.Add(userAdressInformation);
            else
                _userAdressInformationService.Update(userAdressInformation);
            //Session["isAddressEmpty"] = Check.UserAddressInformation(_userAdressInformationService.Get(userProduct.UserID));
            if (_userProductService.Add(userProduct) > 0)
            {
                _productService.UpdateAmount(model.Product.Id, 1);
                _userInfoService.UpdatePoint(model.UserInfo.Id, model.Product.Price);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult GetProductByFiltering(ProductFilteringViewModel filters)
        {
            List<Product> products = null;
            //hataveriyordukapattım
            //if (filters == null)
            //    products = _productService.GetAll().OrderByDescending(x => x.CreatedDate).ToList();
            //else
            //{
            //    if (filters.categoryList == null || filters.categoryList.Count <= 0)
            //        products = _productService.GetProductByPrice(filters.minPrice, filters.maxPrice);
            //    else
            //        products = _productService.GetProductByFiltering(filters.categoryList, filters.minPrice, filters.maxPrice);
            //}
            return PartialView("/Views/Partial/_ProductList.cshtml", products);
        }
    }
}