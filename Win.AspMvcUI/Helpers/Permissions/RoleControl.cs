using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Win.Business.Concrete;
using Win.DataAccess.Concrete.EntityFramework.Repository;

namespace Win.AspMvcUI.Helpers.Permissions
{
    public class RoleControl : AuthorizeAttribute
    {
        /// <summary>
        /// Giriş yapan kullanıcının User, Admin, Editor gibi sahip olduğu rol kontrolü burada yapılır.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["userId"] == null || string.IsNullOrEmpty(httpContext.Session["userId"].ToString()))
            {
                httpContext.Response.Redirect("~/User/Login");
                return false;
            }

            UserInfoManager _userInfoService = new UserInfoManager(new EfUserInfoRepository());
            var user = _userInfoService.Get(int.Parse(httpContext.Session["userId"].ToString()));
            var roles = Roles.Split(',');
            if (user.IsAdmin)
            {
                if (roles.Contains("Admin"))
                    return true;
            }
            //else if (user.IsEditor)
            //{
            //    //...
            //}
            else
                return false;
            return base.AuthorizeCore(httpContext);
        }
    }
}