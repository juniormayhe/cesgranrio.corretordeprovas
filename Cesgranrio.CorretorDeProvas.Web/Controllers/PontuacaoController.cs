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
    public class PontuacaoController : MainController
    {
        const int pageSize = 5;
        /// <summary>
        /// Mostra lista de respostas para corrigir
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult CorrigirRespostas(int? page = 1)
        {
            //TODO: RANDOMIZAR APRESENTACAO DA LISTA
            var paginaComRespostas = db.Pontuacao.OrderBy(p => p.PontuacaoID).ToPagedList(page ?? 1, pageSize);

            PontuacaoVM vm = new PontuacaoVM { Lista = paginaComRespostas };

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PontuacoesPartialView", vm);
            }
            return View(vm);
        }
        
        // GET: Questao/Editar/5
        public async Task<ActionResult> Corrigir(int? id)
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
        public async Task<ActionResult> Corrigir([Bind(Include = "QuestaoID,QuestaoNumero,QuestaoEnunciado,QuestaoGradeFidelidadeAoTema,QuestaoGradeOrganizacaoIdeias,QuestaoGradeNivelDeLinguagem,QuestaoGradeDominioDasRegras")] QuestaoVM vm)
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

        /// <summary>
        /// Mostra página para baixar programa de simulação de respostas
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult SimularRespostas()
        {
            ViewBag.Message = "Execute o simulador para realizar a carga de respostas de candidatos.";

            return View();
        }
    }
}