using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace weatherpro
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "registers",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "registers", action = "home", id = UrlParameter.Optional }
            );
        }
    }
}
