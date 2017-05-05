using Cesgranrio.CorretorDeProvas.DAL;
using Cesgranrio.CorretorDeProvas.DAL.Model;
using Cesgranrio.CorretorDeProvas.Web.Controllers.Shared;
using Cesgranrio.CorretorDeProvas.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace Cesgranrio.CorretorDeProvas.Web.Controllers
{
    [VerificarAcessoFilter]
    [SomenteElaboradorFilter]
    public class QuestaoController : MainController
    {
        private IQuestaoRepository _repository;

        //5 itens por página
        const int pageSize = 5;

        public QuestaoController(IQuestaoRepository repository)
        {
            _repository = repository; 
        }


        public async Task<ActionResult> Listar(int? page = 1)
        {
            throw new ApplicationException("Falha de comunicação");
            var lista = await _repository.ListarAsync();

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

            bool numeroExiste = await _repository.ExisteNumeroAsync(vm.QuestaoNumero);
            //caso o número já esteja em uso, o sistema pode ser mais simpático 
            //e propor um novo número ao elaborador 
            if (numeroExiste)
            {

                int numeroSugerido = 0;
                try
                {
                    numeroSugerido = await _repository.MaximoNumeroAsync();
                }
                catch { }
                numeroSugerido++;
                ModelState.AddModelError("QuestaoNumero", $"Este número de questão já existe. Tente outro como por exemplo: {numeroSugerido}");
                return View(vm);
            }


            if (ModelState.IsValid)
            {
                
                var questao = new Questao
                {
                    QuestaoID = vm.QuestaoID,
                    QuestaoNumero = vm.QuestaoNumero,
                    QuestaoEnunciado = vm.QuestaoEnunciado,
                    QuestaoGradeDominioDasRegras = vm.QuestaoGradeDominioDasRegras,
                    QuestaoGradeFidelidadeAoTema = vm.QuestaoGradeFidelidadeAoTema,
                    QuestaoGradeNivelDeLinguagem = vm.QuestaoGradeNivelDeLinguagem,
                    QuestaoGradeOrganizacaoIdeias = vm.QuestaoGradeOrganizacaoIdeias,
                    Resposta = vm.Resposta
                };
                await _repository.AdicionarAsync(questao);
                
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
            Questao questao = await _repository.ProcurarAsync(id.Value);
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
        public async Task<ActionResult> Editar([Bind(Include = "QuestaoID,QuestaoNumero,QuestaoEnunciado,QuestaoGradeFidelidadeAoTema,QuestaoGradeOrganizacaoIdeias,QuestaoGradeNivelDeLinguagem,QuestaoGradeDominioDasRegras")] QuestaoVM vm, byte[] questaoControleVersao)
        {
            if (vm == null || vm.QuestaoID == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Questao questaoParaAtualizar = await _repository.ProcurarAsync(vm.QuestaoID);
            vm.QuestaoControleVersao = questaoControleVersao;
            
            #region verificar se foi apagado
            if (questaoParaAtualizar == null)
            {
                Questao questaoApagada = new Questao();
                TryUpdateModel(questaoApagada);
                ModelState.AddModelError(string.Empty, "Não foi possível salvar as mudanças. A questão foi apagada.");
                return View(vm);
            }
            #endregion

            try
            {
                if (TryUpdateModel(questaoParaAtualizar, new string[] { "QuestaoID", "QuestaoNumero", "QuestaoEnunciado", "QuestaoGradeFidelidadeAoTema", "QuestaoGradeOrganizacaoIdeias", "QuestaoGradeNivelDeLinguagem", "QuestaoGradeDominioDasRegras", "QuestaoControleVersao" }))
                {

                    if (ModelState.IsValid)
                    {
                        await _repository.AlterarAsync(questaoParaAtualizar, questaoControleVersao);
                        _repository.Recarregar(questaoParaAtualizar);
                        return RedirectToAction("Listar");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Não foi possível salvar as mudanças. Por favor verifique os dados informados.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Não foi possível salvar as mudanças. Por favor verifique os dados informados.");
                }
            }
            catch (DbUpdateConcurrencyException /*dbex*/)
            {
                ModelState.AddModelError(string.Empty, "Não foi possível salvar as mudanças pois outro elaborador acabou de modificar esta questão! Tente mais tarde.");
            }
            catch (RetryLimitExceededException /*dex*/)
            {
                ModelState.AddModelError("", "Não foi possível salvar os dados. Entre em contato com o administrator.");
            }
            
            return View(vm);
        }

        // GET: Questao/Remover/5
        public async Task<ActionResult> Remover(int? id, bool? erroDeConcorrencia)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questao questao = await _repository.ProcurarAsync(id.Value);
            if (questao== null)
            {
                if (erroDeConcorrencia.GetValueOrDefault())
                {
                    return RedirectToAction("Listar");
                }
                return HttpNotFound();
            }
            _repository.Recarregar(questao);
            if (erroDeConcorrencia.GetValueOrDefault())
            {
                
                ModelState.AddModelError(string.Empty, "O registro que você tentou remover já foi alterado por outro elaborador");
            }
            return View(questao);
            //return View(new QuestaoVM
            //{
            //    QuestaoID = questao.QuestaoID,
            //    QuestaoNumero = questao.QuestaoNumero,
            //    QuestaoEnunciado = questao.QuestaoEnunciado,
            //    QuestaoGradeDominioDasRegras = questao.QuestaoGradeDominioDasRegras,
            //    QuestaoGradeFidelidadeAoTema = questao.QuestaoGradeFidelidadeAoTema,
            //    QuestaoGradeNivelDeLinguagem = questao.QuestaoGradeNivelDeLinguagem,
            //    QuestaoGradeOrganizacaoIdeias = questao.QuestaoGradeOrganizacaoIdeias,
            //    QuestaoControleVersao = questao.QuestaoControleVersao,
            //    Resposta = questao.Resposta
            //});
        }

        // POST: Questao/RemocaoConcluida/5
        [HttpPost, ActionName("Remover")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemocaoConcluida(Questao questao)
        {
            try
            {
                Questao questaoAtual = await _repository.ProcurarAsync(questao.QuestaoID);
                _repository.Recarregar(questaoAtual);

                if (Convert.ToBase64String(questaoAtual.QuestaoControleVersao) != Convert.ToBase64String(questao.QuestaoControleVersao)) { 
                    ModelState.AddModelError(string.Empty, "Este registro acabade ter sido modificado por outro elaborador. Caso deseje realmente remover o registro, clique novamente em Remover.");
                    return View(questaoAtual);
                }
                await _repository.RemoverAsync(questaoAtual);
                return RedirectToAction("Listar");
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError(string.Empty, "Não foi possível remover este registro pois ele pode ter sido modificado por outro elaborador.");
                return RedirectToAction("Remover", new { id = questao.QuestaoID, erroDeConcorrencia = true });
            }
            catch (DataException) {
                ModelState.AddModelError(string.Empty, "Não foi possível remover este registro. Tente novamente.");
                return RedirectToAction("Remover", new { id = questao.QuestaoID, erroDeConcorrencia = true });
            }
        }


    }
}