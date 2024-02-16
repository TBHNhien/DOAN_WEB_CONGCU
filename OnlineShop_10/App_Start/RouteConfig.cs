using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop_10
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "Product Category",
            url: "san-pham/{MetaTitle}-{id}",
            defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
            namespaces: new[] { "OnlineShop_10.Controllers" } // Thêm không gian tên của controller chính
            );

            routes.MapRoute(
            name: "Product Detail",
            url: "chi-tiet/{MetaTitle}-{id}",
            defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
            namespaces: new[] { "OnlineShop_10.Controllers" } // Thêm không gian tên của controller chính
            );

            routes.MapRoute(
            name: "About",
            url: "gioi-thieu",
            defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "OnlineShop_10.Controllers" } // Thêm không gian tên của controller chính
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop_10.Controllers" } // Thêm không gian tên của controller chính
            );


        }
    }
}
