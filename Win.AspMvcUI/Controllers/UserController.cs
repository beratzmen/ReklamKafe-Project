using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Win.AspMvcUI.Helpers.Session;
using Win.AspMvcUI.Helpers.Util;
using Win.AspMvcUI.Models;
using Win.Business.Concrete;
using Win.DataAccess.Concrete.EntityFramework.Repository;
using Win.Entities;
using Win.Entities.Dto;
using Win.Entities.Model;


namespace Win.AspMvcUI.Controllers
{

    public class UserController : Controller
    {
        UserProductManager _userProductService = new UserProductManager(new EfUserProductRepository());
        UserAdressInformationManager _userAdressInformationService = new UserAdressInformationManager(new EfUserAdressInformationRepository());
        UserInfoManager _userInfoService = new UserInfoManager(new EfUserInfoRepository());
        ProductFavoriteManager _productFavoriteService = new ProductFavoriteManager(new EfProductFavoriteRepository());
        CityManager _cityService = new CityManager(new EfCityRepository());
        TownManager _townService = new TownManager(new EfTownRepository());
        DistrictManager _districtService = new DistrictManager(new EfDistrictRepository());
        NeighborhoodManager _neighborhoodService = new NeighborhoodManager(new EfNeighborhoodRepository());

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

        [Route("giris")]
        public ActionResult Login()
        {
            SessionControl.Clear();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Route("giris")]
        public ActionResult Login(UserInfo uInfo)
        {
            var user = _userInfoService.UserLogin(uInfo);
            if (user == null)
            {
                SessionControl.Clear();
                ViewBag.AlertMessage = true;
                //return RedirectToAction("Login");
                return View("Login");
            }
            else
            {
                Session["userId"] = user.Id;
                Session["nickname"] = user.NickName;
                Session["isAddressEmpty"] = Check.UserAddressInformation(_userAdressInformationService.Get(user.Id));
                Session["myFavorites"] = _productFavoriteService.GetProductsByUserId(int.Parse(Session["userId"].ToString()));
                Session["AlertMessage"] = true;
                return RedirectToAction("Index", "Home");
            }
        }
        [Route("kayit")]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Route("kayit")]
        public ActionResult Register(UserInfo uInfo, UserAdressInformation userAdressInformation)
        {
            if (_userInfoService.Add(uInfo))
            {
                userAdressInformation.UserID = uInfo.Id;
                _userAdressInformationService.Add(userAdressInformation);
                ViewBag.AlertMessageFirstLogin = true;
                return View("Login");
                //return RedirectToAction("Login");
            }
            else
            {
                ViewBag.AlertMessage = true;
                return View("Register");
            }
            //return Json("Hata: Farklı bir kullanıcı adı girin.");                
        }

        [Route("sifremiunuttum")]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("sifremiunuttum/{email}")]
        public ActionResult ForgotPassword(string email)
        {
            _userInfoService.ResetPassword(email);
            return RedirectToAction("Login");
        }

        [Route("bilgilerim")]
        public ActionResult UserInformation()
        {
            UserAdressInformationViewModel model2 = new UserAdressInformationViewModel();
            var a = _userAdressInformationService.GetUserAdressInformationViewModel(Convert.ToInt32(Session["userId"]));
            if (!SessionControl.IsActiveByUser())
                return RedirectToAction("Login");
            model2.city = a.city;
            model2.town = a.town;
            model2.district = a.district;
            model2.neighborhood = a.neighborhood;
            model2.userInfo = _userInfoService.Get(int.Parse(Session["userId"].ToString()));
            model2.adressInformatin = _userAdressInformationService.Get(int.Parse(Session["userId"].ToString()));
            model2.product = _userProductService.GetProductsByUserId(int.Parse(Session["userId"].ToString()));
            model2.myFavoriteProducts = _productFavoriteService.GetProductsByUserId(int.Parse(Session["userId"].ToString()));
            if (model2.userInfo != null)
                model2.userInfo.Password = string.Empty;
            return View(model2);
        }

        [HttpPost]
        [Route("adresim")]
        public JsonResult CityTownDistrict(int? cityID, string tip, int? townID, int? districtID)
        {

            List<SelectListItem> sonuc = new List<SelectListItem>();
            //bu işlem başarılı bir şekilde gerçekleşti mi onun kontrolunnü yapıyorum
            bool isSuccess = true;
            try
            {
                switch (tip)
                {
                    case "cityGet":
                        //veritabanımızdaki iller tablomuzdan illerimizi sonuc değişkenimze atıyoruz
                        foreach (var city in _cityService.GetAll())
                        {
                            sonuc.Add(new SelectListItem
                            {
                                Text = city.CityName,
                                Value = city.CityID.ToString()
                            });
                        }
                        break;
                    case "townGet":
                        //ilcelerimizi getireceğiz ilimizi selecten seçilen ilID sine göre 
                        foreach (var town in _townService.Get(cityID))
                        {
                            sonuc.Add(new SelectListItem
                            {
                                Text = town.TownName,
                                Value = town.TownID.ToString()
                            });
                        }
                        break;
                    case "districtGet":
                        //ilcelerimizi getireceğiz ilimizi selecten seçilen ilID sine göre 
                        foreach (var district in _districtService.Get(townID))
                        {
                            sonuc.Add(new SelectListItem
                            {
                                Text = district.DistrictName,
                                Value = district.DistrictID.ToString()
                            });
                        }
                        break;
                    case "neighborhoodGet":
                        foreach (var neighborhood in _neighborhoodService.Get(districtID))
                        {
                            sonuc.Add(new SelectListItem
                            {
                                Text = neighborhood.NeighborhoodName,
                                Value = neighborhood.NeighborhoodID.ToString()
                            });
                        }
                        break;

                    default:
                        break;

                }
            }
            catch (Exception)
            {
                //hata ile karşılaşırsak buraya düşüyor
                isSuccess = false;
                sonuc = new List<SelectListItem>();
                sonuc.Add(new SelectListItem
                {
                    Text = "Bir hata oluştu :(",
                    Value = "Default"
                });

            }
            //Oluşturduğum sonucları json olarak geriye gönderiyorum
            return Json(new { ok = isSuccess, text = sonuc });
        }


        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Route("bilgilerimikaydet")]
        public ActionResult UserAddressInformationUpdate(UserInfo userInfo, UserAdressInformation userAdressInformation)
        {
            if (!_userInfoService.Update(userInfo))
                return RedirectToAction("UserInformation");
            if (!_userAdressInformationService.Update(userAdressInformation))
                _userAdressInformationService.Add(userAdressInformation);
            Session["isAddressEmpty"] = Check.UserAddressInformation(_userAdressInformationService.Get(userAdressInformation.UserID));
            return RedirectToAction("UserInformation");
        }

        [Route("cikis")]
        public ActionResult LogOut()
        {
            SessionControl.Clear();
            return RedirectToAction("Login");
        }

        [Route("siralama")]
        public ActionResult Standings()
        {
            return View(_userInfoService.GetStandings());
        }
    }
}