using Cesgranrio.CorretorDeProvas.Web.Controllers.Shared;
using Cesgranrio.CorretorDeProvas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
namespace Cesgranrio.CorretorDeProvas.Web.Controllers
{
    [VerificarAcessoFilter]
    public class QuestaoController : MainController
    {
        //5 itens por página
        const int pageSize = 5;

        public ActionResult Lista(int? page = 1)
        {
            
            var paginaComQuestoes = db.Questao.OrderBy(p => p.QuestaoNumero).ToPagedList(page ?? 1, pageSize);
            
            QuestaoVM vm = new QuestaoVM { Lista = paginaComQuestoes };

            if (Request.IsAjaxRequest())
            {
                return PartialView("_QuestoesPartialView", vm);
            }
            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}