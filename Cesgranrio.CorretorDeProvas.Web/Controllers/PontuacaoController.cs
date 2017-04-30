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


namespace Cesgranrio.CorretorDeProvas.Web.Controllers
{
    [VerificarAcessoFilter]
    public class PontuacaoController : MainController
    {
        private IRepository<Pontuacao> _repository;

        const int pageSize = 5;

        public PontuacaoController(IRepository<Pontuacao> repository)
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
            var lista = await _repository.Listar();
            
            IPagedList<Pontuacao> paginaComRespostas = lista.OrderBy(p => p.PontuacaoID).ToPagedList(page ?? 1, pageSize);

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
            var pontuacao = await _repository.Procurar(id.Value);

            if (pontuacao == null)
                return HttpNotFound();

            var vm = new PontuacaoVM(pontuacao);
            
            
            return View(vm);
        }

        // POST: Questao/Editar/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Corrigir([Bind(Include = "PontuacaoID,QuestaoID,UsuarioID,PontuacaoCPFCandidato,PontuacaoGradeDominioDasRegras,PontuacaoGradeFidelidadeAoTema,PontuacaoGradeNivelDeLinguagem,PontuacaoGradeOrganizacaoIdeias")] PontuacaoVM vm)
        {
            if (ModelState.IsValid)
            {
                var pontuacao = new Pontuacao {
                    
                    PontuacaoID = vm.PontuacaoID,
                    QuestaoID = vm.QuestaoID,
                    Questao = vm.Questao,
                    UsuarioID = vm.UsuarioID,/*id professor*/
                    Usuario = vm.Usuario,/*dados professor*/
                    PontuacaoCPFCandidato = vm.PontuacaoCPFCandidato,
                    PontuacaoDominioDasRegras = vm.PontuacaoGradeDominioDasRegras,
                    PontuacaoFidelidadeAoTema = vm.PontuacaoGradeFidelidadeAoTema,
                    PontuacaoNivelDeLinguagem = vm.PontuacaoGradeNivelDeLinguagem,
                    PontuacaoOrganizacaoDeIdeias = vm.PontuacaoGradeOrganizacaoIdeias,
                    
                };
                await _repository.Alterar(pontuacao);
                return RedirectToAction("CorrigirRespostas");
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