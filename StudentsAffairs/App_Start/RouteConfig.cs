using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StudentsAffairs
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

          

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{sort}/",
                defaults: new { controller = "Students", action = "Index", sort = UrlParameter.Optional }
            );
            
            
        }
    }
}
