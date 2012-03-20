using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using Hostal_El_Eden.Models;

namespace Hostal_El_Eden
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "CategoriesToRooms",
                "habitaciones/{action}/{id}",
                new { controller = "Reservation", action = "Index", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                "Contact",
                "contacto/",
                new { controller = "Home", action = "Contact" }
                );

            routes.MapRoute(
                "Banos",
                "banos/",
                new { controller = "Home", action = "Banos" }
                );

            routes.MapRoute(
                "Restaurant",
                "restaurante/",
                new { controller = "Home", action = "Restaurant" }
                );

            routes.MapRoute(
                "Gallery",
                "galeria/",
                new { controller = "Home", action = "Gallery" }
                );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            /* doing this for new properties in EF */
            Database.SetInitializer<HotelContext>(new HotelDBInitializer());
        }
    }
}