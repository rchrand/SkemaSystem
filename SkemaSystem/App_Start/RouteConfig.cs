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

            routes.MapMvcAttributeRoutes();

            AreaRegistration.RegisterAllAreas();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            ///*routes.MapRoute(
            //    name: "Account",
            //    url: "Account/{action}/{id}",
            //    defaults: new { controller = "Account", action = "Index", id = UrlParameter.Optional }
            //);*/

            //routes.MapRoute(
            //    name: "Education",
            //    url: "{education}/{controller}/{action}/{id}",
            //    defaults: new { controller = "Education", action = "Details", id = UrlParameter.Optional },
            //    constraints: new { education = @"\w+" }
            //);
            
            ///*
            //routes.MapRoute(
            //    name: "Education",
            //    url: "{education}/{action}",
            //    defaults: new { controller = "Education", action = "Details", id = UrlParameter.Optional },
            //    constraints: new { education = @"\w+" }
            //);
            //*/
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
