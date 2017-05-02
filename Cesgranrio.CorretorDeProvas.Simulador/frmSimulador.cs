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

namespace Cesgranrio.CorretorDeProvas.Simulador
{
    public partial class frmSimulador : Form
    {
        const int TOTAL_CANDIDATOS = 10;
        const int TOTAL_THREADS = 8;
        const int PROFESSOR_ID = 6;

        readonly ParallelOptions opcaoParalelismo = new ParallelOptions() { MaxDegreeOfParallelism = TOTAL_THREADS };
        QuestaoRepository _questaoRepository;
        RespostaRepository _pontuacaoRepository;
        UsuarioRepository _professorRepository;
        CandidatoRepository _candidatoRepository;
        static string cpf;

        public frmSimulador()
        {
            InitializeComponent();
            //valor máximo da barra de progresso geral
            progressBar1.Maximum = TOTAL_CANDIDATOS;
            


        }
        

        private void logarSaida(string s) {
            if (lsbSaida.Items.Count > 10000) {
                lsbSaida.Items.Add("Descartando entradas anteriores...");
                lsbSaida.Items.Clear();
            }
            
            Invoke(new Action(() => { 
                lsbSaida.Items.Add($"{lsbSaida.Items.Count+1} {s}");
                lsbSaida.Update();
                
                //scroll
                lsbSaida.SelectedIndex = lsbSaida.Items.Count - 1;
                lsbSaida.SelectedIndex = -1;
            }));
            
        }

        private void gerar()
        {
            Invoke(new Action(() => { btnGerar.Text = "Aguarde..."; }));

            Object o = new Object();
            //sempre num único contexto
            using (var _db = new CorretorDeProvasDbContext("name=CorretorDeProvasDbContext"))
            {

                #region inicializa

                _questaoRepository = new QuestaoRepository(_db);
                _pontuacaoRepository = new RespostaRepository(_db);
                _professorRepository = new UsuarioRepository(_db);
                _candidatoRepository = new CandidatoRepository(_db);


                //desativa botão da UI
                Invoke(new Action(() => { btnGerar.Enabled = false; btnGerar.Text = "Conectando.."; }));


                //descobre qual elaborador podemos pegar da base para registrar a simulação do grupo 1 elaboradores
                Usuario elaborador = _db.Usuario.FirstOrDefault(x => x.GrupoID == 1);

                if (null == elaborador)
                {
                    MessageBox.Show("Por favor cadastre um elaborador no grupo 1.\n\nPersistindo o problema entre em contato com juniormayhe@gmail.com", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var questoes = _db.Questao.ToList();
                //nao existem questoes cadastradas?
                if (0 == questoes.Count())
                {
                    MessageBox.Show("Nenhuma questão foi encontrada.\n\nCrie algumas questões ou verifique se a configuração de banco de dados está correta e se o banco está acessível.\n\nPersistindo o problema entre em contato com juniormayhe@gmail.com", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion


                Task t = _pontuacaoRepository.LimparResposta();
                Task.WaitAny();
                //informa a barra de progresso de questoes ao valor máximo possível da barra
                int totalQuestoes = TOTAL_CANDIDATOS * questoes.Count();
                Invoke(new Action(() => { progressBar2.Maximum = totalQuestoes; }));

                //geramos uma populacao de candidatos com cpfs unicos
                int i = 0;
                int contaThread = 0;
                List<Task> tarefas = new List<Task>();
                for (i = 0; i < TOTAL_CANDIDATOS; i++)
                {
                    System.Diagnostics.Trace.WriteLine($"i={i}");
                    Candidato candidato = new Candidato { CandidatoCPF = Util.GerarCPF(i), CandidatoNome = Util.GerarPalavras() };
                    Task tarefa = new Task(() =>
                    {

                        
                        _candidatoRepository.Adicionar(candidato);

                        foreach (Questao questao in questoes)
                        {
                            //cria a pontuacao simulada
                            Image img = Imagem.GerarFolha(questao.QuestaoNumero.ToString(), candidato.CandidatoCPF);
                            Resposta pontuacao = criarResposta(elaborador, questao, candidato, img);
                            _pontuacaoRepository.Adicionar(pontuacao);
                            Invoke(new Action(() => { pictureBox1.Image = img; }));

                            logarSaida($"{DateTime.Now.ToString("HH:mm:ss.fff")}, {pontuacao.ParaTexto()}");
                            System.Diagnostics.Trace.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}, {pontuacao.ParaTexto()}");
                        }
                    });
                    tarefas.Add(tarefa);
                    contaThread++;
                    if (contaThread == 8 || tarefas.Count()<8)
                    {
                        foreach (var acao in tarefas)
                            acao.RunSynchronously();
                        
                        //Task.WaitAll(tarefas.ToArray());
                        contaThread = 0;
                        tarefas.Clear();
                    }

                    atualizaProgresso(i);
                }
                atualizaProgresso(i);

            }//using

            Invoke(new Action(() => { btnGerar.Enabled = true; btnGerar.Text = "Gerar respostas"; }));
        }

        private void atualizaProgresso(int i) {
            Invoke(new Action(() => { progressBar1.Value = i; lblProgresso.Text = $"Progresso {(i * 100 / TOTAL_CANDIDATOS)}%"; }));
        }
        
        
        private static Resposta criarResposta(Usuario elaborador, Questao questao, Candidato candidato, Image img)
        {
            var resposta = new Resposta
            {
                QuestaoID = 2,
                Questao = questao,

                /*dados para candidato*/
                CandidatoID = candidato.CandidatoID,
                Candidato = candidato,

                /*dados do elaborador*/
                UsuarioID = elaborador.UsuarioID,
                Usuario = elaborador,

                /*pontuacao dada pelo professor*/
                RespostaDominioDasRegras = Convert.ToDecimal(Util.GeraNota(0.01, 11.0)),
                RespostaFidelidadeAoTema = Convert.ToDecimal(Util.GeraNota(0.01, 11.0)),
                RespostaNivelDeLinguagem = Convert.ToDecimal(Util.GeraNota(0.01, 11.0)),
                RespostaOrganizacaoDeIdeias = Convert.ToDecimal(Util.GeraNota(0.01, 11.0)),
                RespostaImagem = Util.ConverterParaArray(img)
            };
            return resposta;
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {

            
            gerar();
            
            


        }
    }
}
