using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cesgranrio.CorretorDeProvas.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Usuario", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Questoes",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Questao", action = "Listar", page = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Respostas",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Resposta", action = "CorrigirRespostas", page = UrlParameter.Optional }
            );


        }
    }
}
