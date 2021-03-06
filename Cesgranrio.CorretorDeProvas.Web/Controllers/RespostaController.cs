﻿using Cesgranrio.CorretorDeProvas.DAL;
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
using Microsoft.Extensions.Logging;
using Cesgranrio.CorretorDeProvas.Util;

namespace Cesgranrio.CorretorDeProvas.Web.Controllers
{
    [SomenteProfessorFilter]
    [VerificarAcessoFilter]
    public class RespostaController : MainController
    {
        private IRespostaRepository _repository;
        static ILogger _logger;

        const int pageSize = 5;

        public RespostaController(IRespostaRepository repository)
        {
            _repository = repository;


            ILoggerFactory loggerFactory = new LoggerFactory()
                .AddConsole()
                .AddDebug();
            _logger = loggerFactory.CreateLogger<RespostaController>();
            
            
            
        }

        /// <summary>
        /// Mostra lista de respostas para corrigir
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<ActionResult> CorrigirRespostas(int? page = 1)
        {
            _logger.LogInformation($"****** ");
            _logger.LogInformation($"{Session.Ler<Usuario>("USUARIO").UsuarioCPF} está visualizando a página {page}");
            _logger.LogInformation($"****** ");

            //TODO: OUT OF MEMORY! TALVEZ TENHA QUE ABRIR A PAGINA DE CORRECAO DIRETAMENTE
            var lista = await _repository.ListarAsync();
            
            IPagedList<Resposta> paginaComRespostas = await lista.ToPagedListAsync(page ?? 1, pageSize);
            

            //IPagedList<Resposta> paginaComRespostas = lista.OrderBy(p => p.RespostaID).ToPagedList(page ?? 1, pageSize);

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
            _logger.LogInformation($"****** ");
            _logger.LogInformation($"{Session.Ler<Usuario>("USUARIO").UsuarioCPF} está reservando para correção a resposta #{id}");
            _logger.LogInformation($"****** ");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var resposta = await _repository.ProcurarAsync(id.Value);
            
            if (resposta == null)
                return HttpNotFound();

            var vm = RespostaVM.CriarRespostaVM(resposta);
            return View(vm);
        }

        // POST: Questao/Editar/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Corrigir([Bind(Include = "RespostaID,UsuarioID,CandidatoID,QuestaoID,GradeEscolhida,RespostaGradeEscolhida,RespostaNota")] RespostaVM vm, byte[] RespostaControleVersao)
        {
            _logger.LogInformation($"****** ");
            _logger.LogInformation($"{Session.Ler<Usuario>("USUARIO").UsuarioCPF} está tentando atualizar a resposta #{vm.RespostaID}");
            _logger.LogInformation($"****** ");
            if (vm == null || vm.RespostaID == 0) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //O corretor deverá poder selecionar apenas uma das notas na grade de correção. Após salvar o documento atual, o sistema deve retornar um outro documento aleatoriamente para o corretor.
            Resposta respostaParaAtualizar = await _repository.ProcurarAsync(vm.RespostaID);
            vm.Questao = respostaParaAtualizar.Questao;
            vm.Usuario = respostaParaAtualizar.Usuario;
            vm.Candidato = respostaParaAtualizar.Candidato;
            vm.RespostaControleVersao = RespostaControleVersao;
            vm.RespostaImagem = respostaParaAtualizar.RespostaImagem;
            
            #region verificar se foi apagado
            if (respostaParaAtualizar == null)
            {
                Resposta respostaApagada = new Resposta();
                TryUpdateModel(respostaApagada);
                _logger.LogInformation($"****** ");
                _logger.LogInformation($"****** {Session.Ler<Usuario>("USUARIO").UsuarioCPF} tentou corrigir a resposta #{vm.RespostaID} com nota {respostaParaAtualizar.RespostaNota} mas já havia sido apagada anteriormente.");
                _logger.LogInformation($"****** ");
                ModelState.AddModelError(string.Empty, "Não foi possível salvar as mudanças. A resposta foi apagada.");
                return View(vm);
            }
            #endregion

            #region validação de negócio
            if (!(vm.RespostaGradeEscolhida >= 1 && vm.RespostaGradeEscolhida <= 4)) {
                ModelState.AddModelError(string.Empty, $"Selecione a grade desejada");
                return View(vm);
            }
            if (vm.RespostaGradeEscolhida == 1 && !(vm.RespostaNota >= 0 && vm.RespostaNota <= vm.Questao.QuestaoGradeFidelidadeAoTema))
            {
                ModelState.AddModelError(string.Empty, $"O intervalo válido para Fidelidade Ao Tema é {0,00} a {vm.Questao.QuestaoGradeFidelidadeAoTema}");
                return View(vm);
            }
            else if (vm.RespostaGradeEscolhida == 2 && !(vm.RespostaNota >= 0 && vm.RespostaNota <= vm.Questao.QuestaoGradeOrganizacaoIdeias))
            {
                ModelState.AddModelError(string.Empty, $"O intervalo válido para Organização de ideias é {0,00} a {vm.Questao.QuestaoGradeOrganizacaoIdeias}");
                return View(vm);
            }
            else if (vm.RespostaGradeEscolhida == 3 && !(vm.RespostaNota >= 0 && vm.RespostaNota <= vm.Questao.QuestaoGradeNivelDeLinguagem))
            {
                ModelState.AddModelError(string.Empty, $"O intervalo válido para Nível de linguagem é {0,00} a {vm.Questao.QuestaoGradeNivelDeLinguagem}");
                return View(vm);
            }
            else if (vm.RespostaGradeEscolhida == 4 && !(vm.RespostaNota >= 0 && vm.RespostaNota <= vm.Questao.QuestaoGradeDominioDasRegras))
            {
                ModelState.AddModelError(string.Empty, $"O intervalo válido para Domínio das regras é {0,00} a {vm.Questao.QuestaoGradeDominioDasRegras}");
                return View(vm);
            }

            if (vm.RespostaNotaConcluida.HasValue && vm.RespostaNotaConcluida.Value) {
                _logger.LogInformation($"****** ");
                _logger.LogInformation($"****** {Session.Ler<Usuario>("USUARIO").UsuarioCPF} tentou corrigir a resposta #{vm.RespostaID} com nota {respostaParaAtualizar.RespostaNota} mas uma nota já havia sido definida anteriormente.");
                _logger.LogInformation($"****** ");
                ModelState.AddModelError(string.Empty, "Uma nota já foi atribuída a esta avaliação.");
                return View(vm);
            }

            #endregion

            try
            {
                if (TryUpdateModel(respostaParaAtualizar, new string[] { "RespostaID", "UsuarioID", "CandidatoID", "QuestaoID", "RespostaGradeEscolhida", "RespostaNota", "RespostaControleVersao" }))
                {

                    if (ModelState.IsValid)
                    {
                        respostaParaAtualizar.RespostaNotaConcluida = true;
                        await _repository.AlterarAsync(respostaParaAtualizar, RespostaControleVersao);
                        _repository.Recarregar(respostaParaAtualizar);
                        _logger.LogInformation($"****** ");
                        _logger.LogInformation($"****** {Session.Ler<Usuario>("USUARIO").UsuarioCPF} corrigiu a resposta #{vm.RespostaID} com nota {respostaParaAtualizar.RespostaNota}");
                        _logger.LogInformation($"****** ");
                        //levamos professor para corrigir outra prova de modo aleatorio
                        try
                        {
                            Resposta r = await _repository.GetRandom();   
                            return RedirectToAction("Corrigir", new { id = r.RespostaID });
                        }
                        catch (Exception /*ex*/) {
                            //se random falhar, podem ter acabado as respostas para corrigir
                            return RedirectToAction("CorrigirRespostas");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Não foi possível salvar as mudanças. Por favor verifique os dados informados.");
                    }
                }
                else {
                    ModelState.AddModelError(string.Empty, "Não foi possível salvar as mudanças. Por favor verifique os dados informados.");
                }
            }
            catch (DbUpdateConcurrencyException /*dbex*/) {
                ModelState.AddModelError(string.Empty, "Não foi possível salvar as mudanças pois outro professor acabou de modificar esta resposta! Tente mais tarde.");
            }
            catch (RetryLimitExceededException /*dex*/)
            {
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