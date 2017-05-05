﻿using Cesgranrio.CorretorDeProvas.DAL;
using Cesgranrio.CorretorDeProvas.DAL.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cesgranrio.CorretorDeProvas.Simulador
{
    public partial class frmSimulador : Form
    {
        const int TOTAL_CANDIDATOS = 10000;
        const int TOTAL_THREADS = 100;
        const int PROFESSOR_ID = 6;

        readonly ParallelOptions opcaoParalelismo = new ParallelOptions() { MaxDegreeOfParallelism = TOTAL_THREADS };
        QuestaoRepository _questaoRepository;
        RespostaRepository _repostaRepository;
        UsuarioRepository _professorRepository;
        CandidatoRepository _candidatoRepository;
        public frmSimulador()
        {
            InitializeComponent();
            //valor máximo da barra de progresso geral
            progressBar1.Maximum = TOTAL_CANDIDATOS;
        }
        
        /// <summary>
        /// apresenta dados do processamento no listbox
        /// </summary>
        /// <param name="indice"></param>
        /// <param name="mensagem"></param>
        private void LogarSaida(int indice, string mensagem) {
            if (lsbSaida.Items.Count > 10000) {
                lsbSaida.Items.Add("Descartando entradas anteriores...");
                lsbSaida.Items.Clear();
            }
            
            Invoke(new Action(() => { 
                lsbSaida.Items.Add($"{indice} {mensagem}");
                lsbSaida.Update();
                
                //scroll
                lsbSaida.SelectedIndex = lsbSaida.Items.Count - 1;
                lsbSaida.SelectedIndex = -1;
            }));
            
        }

        /// <summary>
        /// gera imagens de respostas
        /// </summary>
        private async Task GerarImagensAsync()
        {
            Invoke(new Action(() => { btnGerar.Text = "Aguarde..."; }));


            //sempre num único contexto
            using (var _db = new CorretorDeProvasDbContext())
            {

                #region inicializa    
                _questaoRepository = new QuestaoRepository(_db);
                _repostaRepository = new RespostaRepository(_db);
                _professorRepository = new UsuarioRepository(_db);
                _candidatoRepository = new CandidatoRepository(_db);


                //descobre qual elaborador podemos pegar da base para registrar a simulação do grupo 1 elaboradores
                Usuario elaborador = _db.Usuario.FirstOrDefault(x => x.GrupoID == 1);

                if (null == elaborador)
                {
                    MessageBox.Show("Por favor cadastre um elaborador no grupo 1.\n\nPersistindo o problema entre em contato com juniormayhe@gmail.com", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var questoes = await _questaoRepository.ListarAsync(false);
                //nao existem questoes cadastradas?
                if (0 == questoes.Count())
                {
                    MessageBox.Show("Nenhuma questão foi encontrada.\n\nCrie algumas questões ou verifique se a configuração de banco de dados está correta e se o banco está acessível.\n\nPersistindo o problema entre em contato com juniormayhe@gmail.com", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion

                lock (questoes) {
                    _repostaRepository.LimparRespostasCandidatos();
                }

                //informa a barra de progresso de questoes ao valor máximo possível da barra
                int totalQuestoes = TOTAL_CANDIDATOS * questoes.Count();


                //geramos uma populacao de candidatos com cpfs unicos
                int i = 0;

                int contaResposta = 1;
                List<Task> tarefas = new List<Task>();
                for (i = 0; i < TOTAL_CANDIDATOS; i++)
                {
                    //System.Diagnostics.Trace.WriteLine($"i={i}");
                    Candidato candidato = new Candidato { CandidatoCPF = Util.GerarCPF(i), CandidatoNome = Util.GerarNomeCandidato() };
                    #region adicionamos uma nova thread
                    Task tarefa = new Task(() =>
                    {

                        //adiciona um candidato
                        _candidatoRepository.Adicionar(candidato);

                        foreach (Questao questao in questoes)
                        {
                            //gera uma imagem
                            Image img = Imagem.GerarFolha(questao.QuestaoNumero.ToString(), candidato.CandidatoNome);
                            //adiciona uma resposta com a imagem
                            Resposta resposta = CriarResposta(elaborador, questao, candidato, img);

                            _repostaRepository.Adicionar(resposta);

                            //mostra ultima imagem gerada
                            Invoke(new Action(() => { pictureBox1.Image = img; }));

                            LogarSaida(contaResposta++, $"{DateTime.Now.ToString("HH:mm:ss.fff")}, {resposta.ParaTexto()}");
                            System.Diagnostics.Trace.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}, {resposta.ParaTexto()}");

                        }

                    });
                    tarefas.Add(tarefa);
                    #endregion


                }
                //Parallel.For não é uma opção pois não se aconselha manipular dbcontext com multiplas threads
                //então temos que abrir oito ações diferentes e dispará-las de forma sincrona
                while (tarefas.Count > 0)
                {

                    int total = tarefas.Count() < 0 ? tarefas.Count() : TOTAL_THREADS;


                    var grupo = tarefas.Take(TOTAL_THREADS).ToList();

                    foreach (var item in grupo)
                    {
                        System.Diagnostics.Trace.WriteLine($"....Executando tarefa");
                        //dbcontext não é threadsafe
                        item.RunSynchronously();
                        //remove da lista o item executado
                        tarefas.Remove(item);
                        System.Diagnostics.Trace.WriteLine($"tarefas count {tarefas.Count()}");
                    }
                    System.Diagnostics.Trace.WriteLine("Aguardando o grupo de tarefas");

                    //aguardamos o processamento do lote
                    Task.WaitAll(grupo.ToArray());

                }
                AtualizaProgresso(i);

                _questaoRepository = null;
                _repostaRepository = null;
                _professorRepository = null;
                _candidatoRepository = null;

            }//using

            Invoke(new Action(() => { btnGerar.Enabled = true; btnGerar.Text = "Gerar respostas"; }));

        }

        /// <summary>
        /// atualiza progresso geral
        /// </summary>
        /// <param name="i"></param>
        private void AtualizaProgresso(int i) {
            Invoke(new Action(() => { progressBar1.Value = i; lblProgresso.Text = $"Progresso {(i * 100 / TOTAL_CANDIDATOS)}%"; }));
        }
        
        /// <summary>
        /// Cria uma resposta simulada do candidato
        /// </summary>
        /// <param name="elaborador"></param>
        /// <param name="questao"></param>
        /// <param name="candidato"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        private static Resposta CriarResposta(Usuario elaborador, Questao questao, Candidato candidato, Image img)
        {
            var resposta = new Resposta
            {
                QuestaoID = questao.QuestaoID,
                Questao = questao,

                /*dados para candidato*/
                CandidatoID = candidato.CandidatoID,
                Candidato = candidato,

                /*dados do elaborador*/
                UsuarioID = elaborador.UsuarioID,
                Usuario = elaborador,

                /*folha de redação*/
                RespostaImagem = Util.ConverterParaArray(img),
                
                //por falta de tempo vamos deixar a grade fixa "1" o ideal é ser outra tabela
                RespostaGradeEscolhida = 1,

                /*o professor é quem dará a nota*/
                RespostaNota = 0,
                RespostaNotaConcluida = false
            };
            
            
            return resposta;
        }

        private void BtnGerar_Click(object sender, EventArgs e)
        {
            GerarImagensAsync();
         
        }
    }
}
