using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Cesgranrio.CorretorDeProvas.Util;
namespace Cesgranrio.CorretorDeProvas.Web
{
    /// <summary>
    /// Verifica se o usuário pode acessar controllers
    /// </summary>
    public class VerificarAcessoFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];
            
            var cpf = HttpContext.Current.Items["CPF"] as string;
            
            if (string.IsNullOrEmpty(cpf))
            {
                //redirecionar usuário para página de acesso negado
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
                    {"controller","Erro"},
                    {"action","AcessoNegado"}
                });

                //TODO: poderiamos até logar o acesso nao autorizado e o IP de origem
            }
        }
    }
}