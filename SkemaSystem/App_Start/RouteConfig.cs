using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SkemaSystem
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*
            routes.MapRoute(
                name: "Education",
                url: "{education}/{controller}/{action}/{id}",
                defaults: new { controller = "Education", action = "Details", id = 0 },
                constraints: new { education = @"\w+" }
            );
            */

            routes.MapRoute(
                name: "Education",
                url: "{name}/{action}",
                defaults: new { controller = "Education", action = "Details", id = 0 },
                constraints: new { name = @"\w+" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
