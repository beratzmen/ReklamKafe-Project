using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Win.Business;
using Win.DataAccess.Concrete.EntityFramework.Repository;

namespace Win.MvcUI.Controllers
{
    public class HomeController : Controller
    {
        ProductManager _productService = new ProductManager(new EfProductRepository());
        public ActionResult Index()
        {
            //if (Session["userId"] == null || string.IsNullOrEmpty(Session["userId"].ToString()))
            //    return RedirectToAction("Login", "User");
            //else
            return View(_productService.GetMainPageSliderItems(3, true));
        }

    }
}