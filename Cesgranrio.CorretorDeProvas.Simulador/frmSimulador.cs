using Cesgranrio.CorretorDeProvas.DAL;
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
using Cesgranrio.CorretorDeProvas.DAL.Database;
using System.Diagnostics;

namespace Cesgranrio.CorretorDeProvas.Simulador
{
    public partial class frmSimulador : Form
    {
        const int TOTAL_CANDIDATOS = 100;
        const int TOTAL_THREADS = 8;//numero de nucleos para o parallel
        
        static object o = new object();
        readonly ParallelOptions opcaoParalelismo = new ParallelOptions() { MaxDegreeOfParallelism = TOTAL_THREADS };
        
        public frmSimulador()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// apresenta dados do processamento no listbox
        /// </summary>
        /// <param name="indice"></param>
        /// <param name="mensagem"></param>
        private void LogarSaida(string mensagem) {
            
            Invoke(new Action(() => {
                //removemos itens para não sobrecarregar o listbox
                if (lsbSaida.Items.Count > 100) {
                    //Descartando entradas anteriores...
                    lsbSaida.Items.Clear();
                }
            
                lsbSaida.Items.Add(mensagem);
                lsbSaida.Update();
                
                //scrollamos
                lsbSaida.SelectedIndex = lsbSaida.Items.Count - 1;
                lsbSaida.SelectedIndex = -1;
            }));
            
        }

        /// <summary>
        /// gera imagens de respostas
        /// </summary>
        private void GerarImagens()
        {
            Invoke(new Action(() => { btnGerar.Text = "Aguarde..."; lsbSaida.Items.Clear(); }));
            Stopwatch sw = new Stopwatch();
            sw.Start();


            #region inicializa    
            int itens = 0;
            SimuladorRepository s = new SimuladorRepository(new DatabaseFactory());
            s.LimparRespostasECandidatos();

            //sempre num único contexto
            int totalQuestoes = 0;
            using (var _db = new CorretorDeProvasDbContext())
            {
                totalQuestoes = _db.Questao.Count();
            }
            //nao existem questoes cadastradas?
            if (0 == totalQuestoes)
            {
                MessageBox.Show("Nenhuma questão foi encontrada.\n\nCrie algumas questões ou verifique se a configuração de banco de dados está correta e se o banco está acessível.\n\nPersistindo o problema entre em contato com juniormayhe@gmail.com", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion


            //informa a barra de progresso de questoes ao valor máximo possível da barra
            totalQuestoes = TOTAL_CANDIDATOS * totalQuestoes;
            string formato = "N2";
            if (totalQuestoes > 10000)
            {
                formato = "N5";
            }
            else if (totalQuestoes > 3000)
            {
                formato = "N4";
            }
            else if (totalQuestoes > 300)
            {
                formato = "N3";
            }

            //a barra de progresso é todo o universo de usuarios x numero de questoes
            Invoke(new Action(() => { progressBar1.Maximum = totalQuestoes; }));

            //gera resposta
            //System.Diagnostics.Trace.WriteLine($"Decorrido {sw.Elapsed.Hours}:{sw.Elapsed.Minutes}:{sw.Elapsed.Seconds}:{sw.Elapsed.Milliseconds}");
            List<Task> tarefas = new List<Task>();

            ParallelLoopResult resultado = Parallel.For(0, TOTAL_CANDIDATOS, opcaoParalelismo, i =>
            {
                tarefas.Add(Task.Run(() => {
                    //geramos uma populacao de candidatos com cpfs unicos
                    Candidato candidato = new Candidato { CandidatoCPF = Util.GerarCPF(i), CandidatoNome = Util.GerarNomeCandidato() };

                    #region adicionamos uma nova thread
                    //novo contexto
                    using (var _db2 = new CorretorDeProvasDbContext())
                    {

                        //adiciona um candidato
                        var _candidatoRepository = new CandidatoRepository(_db2);

                        _candidatoRepository.Adicionar(candidato);
                        candidato = _candidatoRepository.Procurar(candidato.CandidatoID);

                        foreach (Questao questao in _db2.Questao.ToList())
                        {
                            //algum usuario do grupo elaboradores
                            Usuario elaborador = _db2.Usuario.ToList().FirstOrDefault(u => u.GrupoID == 1);

                            Task.Run(async () =>
                            {

                                //adiciona uma resposta da questao para candidato com a imagem
                                var resposta = MontarResposta(elaborador, questao, candidato, Imagem.GerarFolha(questao.QuestaoNumero.ToString(), candidato.CandidatoNome));
                                //evitamos problema de dbcontext que não é thread safe fazendo chamadas diretas ao banco de dados
                                await s.CriarResposta(resposta);

                                //imprime progresso
                                Invoke(new Action(() =>
                                {
                                    progressBar1.Value = itens;
                                    string progresso = (itens * 100.00 / totalQuestoes).ToString(formato);
                                    lblProgresso.Text = $"Progresso {progresso}%";
                                    LogarSaida($"{DateTime.Now.ToString("HH:mm:ss.fff")}, {resposta.ParaTexto()}");
                                    //System.Diagnostics.Trace.WriteLine($"Progresso {progresso}% - Decorrido {sw.Elapsed.Hours}:{sw.Elapsed.Minutes}:{sw.Elapsed.Seconds}:{sw.Elapsed.Milliseconds}");
                                }));
                            });//.ContinueWith(t=> Interlocked.Increment(ref itens));
                            Interlocked.Increment(ref itens);
                        }

                    }
                    #endregion

                    Invoke(new Action(() => {
                        progressBar1.Value = itens;
                        string progresso = (itens * 100.00 / totalQuestoes).ToString(formato);
                        lblProgresso.Text = $"Progresso {progresso}%";
                    }));
                }));
            });//for prepara tarefas

            //quando todas as tarefas terminarem atualizamos a interface
            Task.WhenAll(tarefas.Where(x=>x!=null).ToArray()).ContinueWith(t =>
            {
                
                /*todas as tarefas foram concluidas*/
                Invoke(new Action(() =>
                {
                    System.Diagnostics.Trace.WriteLine($"Tempo decorrido {sw.Elapsed.ToString(@"hh\:mm\:ss")}");
                    progressBar1.Value = itens;
                    string progresso = (itens * 100.00 / totalQuestoes).ToString(formato);
                    lblProgresso.Text = $"Progresso {progresso}%";
                    btnGerar.Enabled = true; btnGerar.Text = "Gerar respostas";
                }));
            });
        }
        
        /// <summary>
        /// Cria uma resposta simulada do candidato
        /// </summary>
        /// <param name="elaborador"></param>
        /// <param name="questao"></param>
        /// <param name="candidato"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        private static Resposta MontarResposta(Usuario elaborador, Questao questao, Candidato candidato, Image img)
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
            //liberamos a interface
            Task.Run(()=>GerarImagens());
        }
    }
}
