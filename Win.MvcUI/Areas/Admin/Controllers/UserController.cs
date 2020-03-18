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
    public class UserController : Controller
    {
        UserInfoManager _userInfoService = new UserInfoManager(new EfUserInfoRepository());

        // GET: Admin/User
        public ActionResult Index()
        {
            return View(_userInfoService.GetAll());
        }
        public ActionResult Delete(int id)
        {
            _userInfoService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}