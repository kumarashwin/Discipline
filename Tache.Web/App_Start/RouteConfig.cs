using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;

namespace Tache.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: null,
            //    url: "{year}/{month}/{day}",
            //    defaults: new { controller = "Day", action = "Index" },
            //    constraints: new { year = new RangeRouteConstraint(2000, 2999), month = new RangeRouteConstraint(1,12), day = new RangeRouteConstraint(1, 31) });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
