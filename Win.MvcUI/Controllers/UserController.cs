using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Win.Business.Concrete;
using Win.DataAccess.Concrete.EntityFramework.Repository;
using Win.Entities;
using Win.Entities.Enum;
using Win.Entities.Model;
using Win.MvcUI.Helpers.Session;
using Win.MvcUI.Helpers.Util;

namespace Win.MvcUI.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        UserProductManager _userProductService = new UserProductManager(new EfUserProductRepository());
        UserAdressInformationManager _userAdressInformationService = new UserAdressInformationManager(new EfUserAdressInformationRepository());
        UserInfoManager _userInfoService = new UserInfoManager(new EfUserInfoRepository());

        public ActionResult Details(int id)
        {
            var user = _userInfoService.Get(id);
            if (user == null)
                return RedirectToAction("Index", "Home");
            else
            {
                user.Password = string.Empty;
                if (user.IsAdmin)
                    return RedirectToAction("Index", "Home");
                return View(user);
            }
        }
        public ActionResult Login()
        {
            SessionControl.Clear();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Add(UserInfo uInfo)
        {
            if (_userInfoService.Add(uInfo))
                return RedirectToAction("Login");
            else
                return Json("Hata: Farklı bir kullanıcı adı girin.");
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UserLogin(UserInfo uInfo)
        {
            var user = _userInfoService.UserLogin(uInfo);
            if (user == null)
            {
                SessionControl.Clear();
                return RedirectToAction("Login");
            }
            else
            {
                Session["userId"] = user.Id;
                Session["nickname"] = user.NickName;
                //Session["isAddressEmpty"] = Check.UserAddressInformation(_userAdressInformationService.Get(user.Id));
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(string email)
        {
            _userInfoService.ResetPassword(email);
            return RedirectToAction("Login");
        }
        public ActionResult UserInformation()
        {
            if (!SessionControl.IsActiveByUser())
                return RedirectToAction("Login");
            return View(new Tuple<UserInfo, UserAdressInformation, List<Product>>(_userInfoService.Get(int.Parse(Session["userId"].ToString())),
                _userAdressInformationService.Get(int.Parse(Session["userId"].ToString())),
                _userProductService.GetProductsByUserId(int.Parse(Session["userId"].ToString()))));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UserInfoUpdate(UserInfo uInfo)
        {
            _userInfoService.Update(uInfo);
            return RedirectToAction("UserInformation");
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UserAddressInformationUpdate(UserAdressInformation uAddressInformation)
        {
            if (!_userAdressInformationService.Update(uAddressInformation))
                _userAdressInformationService.Add(uAddressInformation);
            //Session["isAddressEmpty"] = Check.UserAddressInformation(_userAdressInformationService.Get(uAddressInformation.UserID));
            return RedirectToAction("UserInformation");
        }
        public ActionResult LogOut()
        {
            SessionControl.Clear();
            return RedirectToAction("Login");
        }
        public ActionResult Standings()
        {
            return View(_userInfoService.GetStandings());
        }
    }
}