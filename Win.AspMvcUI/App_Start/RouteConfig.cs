using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Win.AspMvcUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
           name: "ProductConfirmation",
           url: "onay/{id}",
           defaults: new { controller = "Product", action = "Confirmation", id = UrlParameter.Optional },
           namespaces: new[] { "Win.AspMvcUI.Controllers" }
    );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Win.AspMvcUI.Controllers" }
            );
        }
    }
}
