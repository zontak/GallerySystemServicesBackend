using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GallerySystemServices.Services
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);


            config.Routes.MapHttpRoute(
               name: "picture",
               routeTemplate: "api/picture/{action}",
               defaults: new { controller = "Picture" }
           );

            config.Routes.MapHttpRoute(
               name: "album",
               routeTemplate: "api/album/{action}",
               defaults: new { controller = "Album" }
           );

            config.Routes.MapHttpRoute(
               name: "authentication",
               routeTemplate: "api/authentication/{action}/{userId}",
               defaults: new { controller = "Authentication", userId = RouteParameter.Optional }
           );

            config.Routes.MapHttpRoute(
               name: "user",
               routeTemplate: "api/user/{action}",
               defaults: new { controller = "User" }
           );

            config.Routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: "api/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional },
               constraints: new { controller = "Home" }
           );



            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
