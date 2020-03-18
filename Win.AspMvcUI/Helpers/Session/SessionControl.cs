using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Win.Entities.Model;

namespace Win.AspMvcUI.Helpers.Session
{
    public class SessionControl
    {
        public static bool IsActiveByUser()
        {
            try
            {
                if (HttpContext.Current.Session["userId"] == null || string.IsNullOrEmpty(HttpContext.Current.Session["userId"].ToString()) || string.IsNullOrWhiteSpace(HttpContext.Current.Session["userId"].ToString()))
                    return false;
                else
                    return true;
            }
            catch (Exception)
            {
                HttpContext.Current.Response.Redirect("~/User/Login");
                return false;
            }
        }
        public static bool ExistFavoriteByProductId(int productId)
        {
            try
            {
                return (HttpContext.Current.Session["myFavorites"] as List<Product>).Any(p => p.Id == productId);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool Clear()
        {
            try
            {
                //HttpContext.Current.Session.Abandon();
                HttpContext.Current.Session.Clear();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}