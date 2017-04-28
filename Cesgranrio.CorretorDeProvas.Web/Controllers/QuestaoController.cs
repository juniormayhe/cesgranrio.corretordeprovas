using Cesgranrio.CorretorDeProvas.Web.Controllers.Shared;
using Cesgranrio.CorretorDeProvas.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

        // GET: Questao/Editar/5
        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questao questao = await db.Questao.FindAsync(id);
            if (questao == null)
                return HttpNotFound();

            var vm = new QuestaoVM(questao);
            
            
            return View(vm);
        }

        // POST: Questao/Editar/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar([Bind(Include = "QuestaoID,QuestaoNumero,QuestaoEnunciado,QuestaoGradeFidelidadeAoTema,QuestaoGradeOrganizacaoIdeias,QuestaoGradeNivelDeLinguagem,QuestaoGradeDominioDasRegras")] QuestaoVM vm)
        {
            if (ModelState.IsValid)
            {
                var questao = new Questao {
                    QuestaoID = vm.QuestaoID,
                    QuestaoNumero = vm.QuestaoNumero,
                    QuestaoEnunciado = vm.QuestaoEnunciado,
                    QuestaoGradeDominioDasRegras = vm.QuestaoGradeDominioDasRegras,
                    QuestaoGradeFidelidadeAoTema = vm.QuestaoGradeFidelidadeAoTema,
                    QuestaoGradeNivelDeLinguagem = vm.QuestaoGradeNivelDeLinguagem,
                    QuestaoGradeOrganizacaoIdeias = vm.QuestaoGradeOrganizacaoIdeias,
                    Pontuacao = vm.Pontuacao
                };
                db.Entry(questao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Lista");
            }
            
            return View(vm);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}