using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Win.AspMvcUI.Helpers.Session;
using Win.AspMvcUI.Helpers.Util;
using Win.AspMvcUI.Models;
using Win.Business;
using Win.Business.Concrete;
using Win.DataAccess;
using Win.DataAccess.Concrete.EntityFramework.Repository;
using Win.Entities.Dto;
using Win.Entities.Model;
using Win.Helpers.Util;

namespace Win.AspMvcUI.Controllers
{

    public class ProductController : Controller
    {
        ProductManager _productService = new ProductManager(new EfProductRepository());
        UserAdressInformationManager _userAdressInformationService = new UserAdressInformationManager(new EfUserAdressInformationRepository());
        UserProductManager _userProductService = new UserProductManager(new EfUserProductRepository());
        CategoryManager _categoryService = new CategoryManager(new EfCategoryRepository());
        UserInfoManager _userInfoService = new UserInfoManager(new EfUserInfoRepository());
        CommentManager _commentService = new CommentManager(new EfCommentRepository());
        ProductFavoriteManager _productFavoriteService = new ProductFavoriteManager(new EfProductFavoriteRepository());

        [Route("urunler")]
        public ActionResult Index()
        {
            ProductCategoryListViewModel model = new ProductCategoryListViewModel();
            model.Product = _productService.GetAll().OrderByDescending(x => x.CreatedDate).ToList();
            model.Category = _categoryService.GetAll();
            return View(model);
        }

        [HttpPost]
        [Route("bul/{search}")]
        public ActionResult Search(string search)
        {
            ProductCategoryListViewModel model = new ProductCategoryListViewModel();
            model.Product = _productService.GetAll().OrderByDescending(x => x.CreatedDate).ToList();
            model.SearchProduct = _productService.Search(search);
            model.Category = _categoryService.GetAll();
            return View("Index", model);
        }

        [Route("urun/{url}")]
        public ActionResult Details(int id)
        {
            var purchasers = _userProductService.GetUsersByProductId(id);
            _productService.DisplayCounter(id);
            //var userInfo = _userInfoService.Get(Convert.ToInt32(Session["userId"]));

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
            return View(new Tuple<Product, List<string>, List<ProductCommentsViewModel>, UserInfo>
                (_productService.Get(id), purchasers, _commentService.GetAll(id), _userInfoService.Get(Convert.ToInt32(Session["userId"]))));
        }
        [HttpPost]
        public ActionResult CommentAdd(Comment entity)
        {
            _commentService.Add(entity);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        [HttpPost]
        public ActionResult GetProductByFiltering(ProductFilteringViewModel filters)
        {
            List<Product> products = null;
            if (filters == null)
                products = _productService.GetAll().OrderByDescending(x => x.CreatedDate).ToList();
            else
            {
                if (filters.categoryId <= 0)
                    products = _productService.GetProductByPrice(filters.minPrice, filters.maxPrice, filters.sortingValue);
                else
                    products = _productService.GetProductByFiltering(new List<int>() { filters.categoryId }, filters.minPrice, filters.maxPrice, filters.sortingValue);
            }
            return PartialView("/Views/Partial/_ProductList.cshtml", products);
        }

        [Route("sipariskontrol/{id}")]
        public ActionResult Checkout(int id)
        {
            if (!SessionControl.IsActiveByUser())
                return RedirectToAction("Login", "User");
            var currentProduct = _productService.Get(id);
            if (currentProduct == null)
                return RedirectToAction("Index", "Product");
            if (!currentProduct.IsActive || currentProduct.Amount <= 0)
                return RedirectToAction("Details", "Product", new { id = id });
            return View(new ConfirmationListViewModel()
            {
                Product = currentProduct,
                UserAdressInformation = _userAdressInformationService.Get(int.Parse(Session["userId"].ToString())),
                UserAdress = _userAdressInformationService.GetUserAdressInformationViewModel(int.Parse(Session["userId"].ToString())),
                UserInfo = _userInfoService.Get(Convert.ToInt32(Session["userId"]))
            });
        }

        [Route("siparistakip/{id}")]
        public ActionResult OrderTracking(int id)
        {
            ConfirmationListViewModel model = new ConfirmationListViewModel();
            model.UserAdressInformation = _userAdressInformationService.Get(Convert.ToInt32(Session["userId"]));
            model.UserAdress = _userAdressInformationService.GetUserAdressInformationViewModel(Convert.ToInt32(Session["userId"]));
            model.UserProductViewModel = _userProductService.GetUserByProductId(id, Convert.ToInt32(Session["userId"]));
            model.Product = _productService.Get(model.UserProductViewModel.ProductId);
            return View(model);
        }

        public ActionResult Confirmation(string id)
        {
            if (!SessionControl.IsActiveByUser())
                return RedirectToAction("Login", "User");
            ConfirmationListViewModel model = new ConfirmationListViewModel();
            model.Product = _productService.Get(int.Parse(Encrypt.Md5Decrypt(id)));
            model.UserAdressInformation = _userAdressInformationService.Get(int.Parse(Session["userId"].ToString()));
            model.UserAdress = _userAdressInformationService.GetUserAdressInformationViewModel(int.Parse(Session["userId"].ToString()));
            model.UserProductViewModel = _userProductService.GetUserByProductId(model.Product.Id, int.Parse(Session["userId"].ToString()));
            return View(model);
        }
        [HttpPost]
        public JsonResult Confirmation(UserAdressInformation userAdressInformation, int productId)
        {
            ConfirmationListViewModel model = new ConfirmationListViewModel();
            userAdressInformation.UserID = int.Parse(Session["userId"].ToString());
            var uProduct = new UserProduct();
            uProduct.ProductID = productId;
            uProduct.UserID = int.Parse(Session["userId"].ToString());
            model.Product = _productService.Get(uProduct.ProductID);
            model.UserInfo = _userInfoService.Get(uProduct.UserID);
            if (model.UserInfo.Point < model.Product.Price)
                return Json("index");
            var address = _userAdressInformationService.GetUserAdressInformationViewModel(userAdressInformation.UserID);
            if (address != null)
            {
                userAdressInformation.CityId = address.cityId;
                userAdressInformation.TownId = address.townId;
                userAdressInformation.DistrictId = address.districtId;
                userAdressInformation.NeighborhoodId = address.neighborhoodId;
            }
            if (_userAdressInformationService.Get(userAdressInformation.UserID) == null)
                _userAdressInformationService.Add(userAdressInformation);
            else
                _userAdressInformationService.Update(userAdressInformation);
            Session["isAddressEmpty"] = Check.UserAddressInformation(_userAdressInformationService.Get(uProduct.UserID));
            if (_userProductService.Add(uProduct) > 0)
            {
                _productService.UpdateAmount(model.Product.Id, 1);
                _userInfoService.UpdatePoint(model.UserInfo.Id, model.Product.Price);
            }
            return Json(Encrypt.Md5(uProduct.ProductID.ToString() + "RKCONFIRM2019"));
        }
        [HttpPost]
        public JsonResult FavoriteAdd(int productId)
        {
            int status = 0;
            if (!SessionControl.IsActiveByUser())
                return Json(new { result = status, productId = productId });
            var result = _productFavoriteService.Add(new ProductFavorite() { UserID = int.Parse(Session["userId"].ToString()), ProductID = productId });
            Session["myFavorites"] = _productFavoriteService.GetProductsByUserId(int.Parse(Session["userId"].ToString()));
            if (result < 1)
                status = -1;
            else
                status = result;
            return Json(new { result = status, productId = productId });
        }
    }
}