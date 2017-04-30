using Cesgranrio.CorretorDeProvas.Web.Controllers.Shared;
using Cesgranrio.CorretorDeProvas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cesgranrio.CorretorDeProvas.DAL;

namespace Cesgranrio.CorretorDeProvas.Web.Controllers
{
    /// <summary>
    /// Controller que trata erros da aplicação
    /// </summary>
    public class ErroController : MainController
    {

        //erro 403, usado em VerificarAcessoFilterAttribute
        public ActionResult AcessoNegado()
        {
            return View();
        }

        //erro 404, usado em global asax
        public ActionResult PaginaNaoEncontrada()
        {
            return View();
        }

        //erro 400, usado em global asax
        public ActionResult RequisicaoInvalida()
        {
            return View();
        }

        //erro 500, pode ser disparado por qualquer erro de aplicação (ex: NullReferenceException, ApplicationException, etc)
        public ActionResult FalhaNaAplicacao()
        {
            ErroVM vm;
            if (TempData["Erro"]!= null)
                vm = TempData["Erro"] as ErroVM;
            else
                vm = new ErroVM { Erro = RouteData.Values["Erro"] as string, Descrição = RouteData.Values["Descrição"] as string };
            
            return View(vm);
        }
    }
}