using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Win.AspMvcUI.Models;
using Win.Business;
using Win.Business.Concrete;
using Win.DataAccess.Concrete.EntityFramework.Repository;
using Win.Entities.Model;

namespace Win.AspMvcUI.Controllers
{

    public class HomeController : Controller
    {
        ProductManager _productService = new ProductManager(new EfProductRepository());
        BulletinManager _bulletinService = new BulletinManager(new EfBulletinRepository());
        UserInfoManager _userInfoService = new UserInfoManager(new EfUserInfoRepository());
        AdsManagementManager _adsManagementService = new AdsManagementManager(new EfAdsManagementRepository());

        [Route("")]
        [Route("anasayfa")]
        public ActionResult Index()
        {
            return View(new MainPageViewModel() { products = _productService.GetMainPageSliderItems(8, true), users = _userInfoService.TopUsers(10) });
        }


        [HttpPost]
        public ActionResult BulletinAdd(Bulletin bulletin)
        {
            _bulletinService.Add(bulletin);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ReklamTest()
        {
            if (Helpers.Session.SessionControl.IsActiveByUser())
                return Json(_adsManagementService.ClickAds(int.Parse(Session["userId"].ToString())));
            else
                return Json(false);
        }
    }
}