﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Cesgranrio.CorretorDeProvas.Util;
using Cesgranrio.CorretorDeProvas.DAL.Model;

namespace Cesgranrio.CorretorDeProvas.Web
{
    /// <summary>
    /// Verifica se o usuário pode acessar controllers
    /// </summary>
    public class VerificarAcessoFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            bool permiteAnonimo = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), false)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), false);

            if (permiteAnonimo)
            {
                return;
            }

            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];
            
            var usuario = HttpContext.Current.Session["USUARIO"] as Usuario;

            if (usuario == null)
            {
                //redirecionar usuário para página de acesso negado
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
                    {"controller","Erro"},
                    {"action","AcessoNegado"}
                });
            }
            
        }
    }
}