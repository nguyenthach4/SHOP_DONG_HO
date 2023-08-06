using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}",
              new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });
            routes.MapRoute(
             name: "Cart",
             url: "gio-hang.html",
             defaults: new { Controller = "ShoppingCart", action = "Index", id = UrlParameter.Optional },
             namespaces: new string[] { "WebApp.Controllers" }
             );
            routes.MapRoute(
            name: "Confirm Order",
            url: "xac-nhan-don-hang.html",
            defaults: new { Controller = "ShoppingCart", action = "ConfirmOder", id = UrlParameter.Optional },
            namespaces: new string[] { "WebApp.Controllers" }
            );
            routes.MapRoute(
            name: "Cancel Order",
            url: "huy-don-hang.html",
            defaults: new { Controller = "ShoppingCart", action = "CancelOrder", id = UrlParameter.Optional },
            namespaces: new string[] { "WebApp.Controllers" }
            );
            routes.MapRoute(
                name: "Search",
                url: "tim-kiem.html",
                defaults: new { Controller = "Product", action = "Search", id = UrlParameter.Optional },
                namespaces: new string[] { "WebApp.Controllers" }
                );

            routes.MapRoute(
                name: "ContactDetail",
                url: "lien-he.html",
                defaults: new { Controller = "Contact", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "WebApp.Controllers" }
                );
            routes.MapRoute(
              name: "TagList",
              url: "tag/{tagId}.html",
              defaults: new { Controller = "Product", action = "ListByTag", tagId = UrlParameter.Optional },
              namespaces: new string[] { "WebApp.Controllers" }
              );
            routes.MapRoute(
                name: "Page",
                url: "trang/{alias}.html",
                defaults: new { Controller = "Page", action = "Index", alias = UrlParameter.Optional },
                namespaces: new string[] { "WebApp.Controllers" }
                );
            routes.MapRoute(
                name: "Login",
                url: "dang-nhap.html",
                defaults: new { Controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new string[] { "WebApp.Controllers" }
                );
            routes.MapRoute(
                name: "Register",
                url: "dang-ky.html",
                defaults: new { Controller = "Account", action = "Register", id = UrlParameter.Optional },
                namespaces: new string[] { "WebApp.Controllers" }
                );
            routes.MapRoute(
            name: "Product Category",
            url: "{alias}.pc-{id}.html",
            defaults: new { Controller = "Product", action = "Category", id = UrlParameter.Optional },
              namespaces: new string[] { "WebApp.Controllers" }
            );
            routes.MapRoute(
                    name: "Product",
                    url: "{alias}.p-{productId}.html",
                    defaults: new { controller = "Product", action = "Detail", productId = UrlParameter.Optional },
                      namespaces: new string[] { "TeduShop.Web.Controllers" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
