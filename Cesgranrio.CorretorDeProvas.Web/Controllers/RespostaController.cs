using Cesgranrio.CorretorDeProvas.DAL;
using Cesgranrio.CorretorDeProvas.DAL.Model;
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
using System.Data.Entity.Infrastructure;

namespace Cesgranrio.CorretorDeProvas.Web.Controllers
{
    [VerificarAcessoFilter]
    public class RespostaController : MainController
    {
        private IRepository<Resposta> _repository;

        const int pageSize = 5;

        public RespostaController(IRepository<Resposta> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Mostra lista de respostas para corrigir
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<ActionResult> CorrigirRespostas(int? page = 1)
        {
            //TODO: RANDOMIZAR APRESENTACAO DA LISTA
            var lista = await _repository.ListarAsync();
            
            IPagedList<Resposta> paginaComRespostas = lista.OrderBy(p => p.RespostaID).ToPagedList(page ?? 1, pageSize);

            RespostaVM vm = new RespostaVM { Lista = paginaComRespostas };

            if (Request.IsAjaxRequest())
            {
                return PartialView("_RespostasPartialView", vm);
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
            
            var resposta = await _repository.ProcurarAsync(id.Value);
            
            if (resposta == null)
                return HttpNotFound();

            var vm = new RespostaVM(resposta);
            
            
            return View(vm);
        }

        // POST: Questao/Editar/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Corrigir([Bind(Include = "RespostaID,UsuarioID,CandidatoID,QuestaoID,RespostaGradeFidelidadeAoTema,RespostaGradeOrganizacaoIdeias,RespostaGradeNivelDeLinguagem,RespostaGradeDominioDasRegras")] RespostaVM vm, byte[] respostaControleVersao)
        {
            if (vm == null || vm.RespostaID == 0) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Resposta respostaParaAtualizar = await _repository.ProcurarAsync(vm.RespostaID);

            vm.Questao = respostaParaAtualizar.Questao;
            vm.Usuario = respostaParaAtualizar.Usuario;
            vm.Candidato = respostaParaAtualizar.Candidato;

            //verificar se foi apagado
            if (respostaParaAtualizar == null)
            {
                Resposta respostaApagada = new Resposta();
                TryUpdateModel(respostaApagada);
                ModelState.AddModelError(string.Empty, "Não foi possível salvar as mudanças. A resposta foi apagada.");
                return View(vm);
            }
            
            try
            {
                if (TryUpdateModel(respostaParaAtualizar, new string[] { "RespostaID", "UsuarioID", "CandidatoID", "QuestaoID", "RespostaGradeFidelidadeAoTema", "RespostaGradeOrganizacaoIdeias", "RespostaGradeNivelDeLinguagem", "RespostaGradeDominioDasRegras", "RespostaControleVersao" }))
                {

                    await _repository.AlterarAsync(respostaParaAtualizar, respostaControleVersao);
                    return RedirectToAction("CorrigirRespostas");
                }
            }
            catch (DbUpdateConcurrencyException dbex) {
                ModelState.AddModelError(string.Empty, "Não foi possível salvar as mudanças pois outro professor acabou de modificar esta resposta! Tente mais tarde.");

            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Não foi possível salvar os dados. Entre em contato com o administrator.");
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