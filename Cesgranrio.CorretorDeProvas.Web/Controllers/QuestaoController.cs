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
    public class QuestaoController : MainController
    {
        private IRepository<Questao> _repository;

        //5 itens por página
        const int pageSize = 5;

        public QuestaoController(IRepository<Questao> repository)
        {
            _repository = repository;
        }


        public async Task<ActionResult> Listar(int? page = 1)
        {

            var lista = await _repository.Listar();

            IPagedList<Questao> paginaComQuestoes = lista.OrderBy(p => p.QuestaoNumero).ToPagedList(page ?? 1, pageSize);

            QuestaoVM vm = new QuestaoVM { Lista = paginaComQuestoes };

            if (Request.IsAjaxRequest())
            {
                return PartialView("_QuestoesPartialView", vm);
            }
            return View(vm);
        }

        // GET: Questao/Adicionar
        [VerificarAcessoFilter]
        public ActionResult Adicionar()
        {
            return View();
        }

        // POST: Questao/Adicionar
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Adicionar([Bind(Include = "QuestaoID,QuestaoNumero,QuestaoEnunciado,QuestaoGradeFidelidadeAoTema,QuestaoGradeOrganizacaoIdeias,QuestaoGradeNivelDeLinguagem,QuestaoGradeDominioDasRegras")] QuestaoVM vm)
        {
            if (ModelState.IsValid)
            {
                bool numeroExiste = await _repository.Existe(vm.QuestaoNumero);
                //caso o número já esteja em uso, o sistema pode ser mais simpático 
                //e propor um novo número ao elaborador 
                if (numeroExiste) {

                    int numeroSugerido = 0;
                    try {
                        numeroSugerido = await _repository.MaximoID();
                    }
                    catch { }
                    numeroSugerido++;
                    ModelState.AddModelError("QuestaoNumero", $"Este número de questão já existe. Tente outro como por exemplo: {numeroSugerido}");
                    return View(vm);
                }

                var questao = new Questao
                {
                    QuestaoID = vm.QuestaoID,
                    QuestaoNumero = vm.QuestaoNumero,
                    QuestaoEnunciado = vm.QuestaoEnunciado,
                    QuestaoGradeDominioDasRegras = vm.QuestaoGradeDominioDasRegras,
                    QuestaoGradeFidelidadeAoTema = vm.QuestaoGradeFidelidadeAoTema,
                    QuestaoGradeNivelDeLinguagem = vm.QuestaoGradeNivelDeLinguagem,
                    QuestaoGradeOrganizacaoIdeias = vm.QuestaoGradeOrganizacaoIdeias,
                    Pontuacao = vm.Pontuacao
                };
                await _repository.Adicionar(questao);
                
                return RedirectToAction("Listar");
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
            Questao questao = await _repository.Procurar(id.Value);
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
                await _repository.Alterar(questao);
                
                return RedirectToAction("Listar");
            }
            
            return View(vm);
        }

        // GET: Questao/Remover/5
        public async Task<ActionResult> Remover(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questao questao = await _repository.Procurar(id.Value);
            if (questao== null)
            {
                return HttpNotFound();
            }
            return View(new QuestaoVM
            {
                QuestaoID = questao.QuestaoID,
                QuestaoNumero = questao.QuestaoNumero,
                QuestaoEnunciado = questao.QuestaoEnunciado,
                QuestaoGradeDominioDasRegras = questao.QuestaoGradeDominioDasRegras,
                QuestaoGradeFidelidadeAoTema = questao.QuestaoGradeFidelidadeAoTema,
                QuestaoGradeNivelDeLinguagem = questao.QuestaoGradeNivelDeLinguagem,
                QuestaoGradeOrganizacaoIdeias = questao.QuestaoGradeOrganizacaoIdeias,
                Pontuacao = questao.Pontuacao
            });
        }

        // POST: Questao/RemocaoConcluida/5
        [HttpPost, ActionName("Remover")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemocaoConcluida(int id)
        {
            await _repository.Remover(id);
            return RedirectToAction("Listar");
        }


    }
}