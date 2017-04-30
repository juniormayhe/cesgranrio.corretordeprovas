using Cesgranrio.CorretorDeProvas.DAL.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using X.PagedList;
using X.PagedList.Mvc;

namespace Cesgranrio.CorretorDeProvas.Web.Models
{
    
    /// <summary>
    /// Transporta dados das pontuacoes
    /// </summary>
    public class PontuacaoVM
    {
        
        public PontuacaoVM()
        {
            
        }

        public PontuacaoVM(Pontuacao pontuacao)
        {
            this.PontuacaoID = pontuacao.PontuacaoID;
            this.QuestaoID = pontuacao.QuestaoID;

            this.PontuacaoCPFCandidato = pontuacao.Usuario.UsuarioCPF;
            this.PontuacaoRespostaCandidato = pontuacao.PontuacaoRespostaCandidato;
            this.PontuacaoGradeDominioDasRegras = pontuacao.PontuacaoDominioDasRegras;
            this.PontuacaoGradeFidelidadeAoTema = pontuacao.PontuacaoFidelidadeAoTema;
            this.PontuacaoGradeNivelDeLinguagem = pontuacao.PontuacaoNivelDeLinguagem;
            this.PontuacaoGradeOrganizacaoIdeias = pontuacao.PontuacaoOrganizacaoDeIdeias;
            this.Questao = pontuacao.Questao;
            this.Usuario = pontuacao.Usuario;
        }

        [Key]
        [Display(Name = "ID")]
        public int PontuacaoID { get; set; }

        [Display(Name = "Questão")]
        public int QuestaoID { get; set; }

        [Display(Name = "ID do professor")]
        public int UsuarioID { get; set; }
        

        [Display(Name = "CPF do Candidato")]
        public string PontuacaoCPFCandidato { get; set; }
        
        [Display(Name = "Resposta do candidato")]
        public string PontuacaoRespostaCandidato { get; set; }

        [Required(ErrorMessage = "Por favor informe os pontos para Fidelidade ao tema")]
        [Display(Name = "Fidelidade ao tema")]
        [Range(0.01, 999.99, ErrorMessage ="Os pontos devem estar entre 0,01 e 999,99")]
        [RegularExpression(@"^[0-9]{1,3}(\,[0-9]{1,2})?$|^(\d{3})[\,]$", ErrorMessage = "Por favor informe um número até 999,99")]
        public decimal PontuacaoGradeFidelidadeAoTema { get; set; }

        [Required(ErrorMessage = "Por favor informe os pontos para Organização de ideias")]
        [Display(Name = "Organização de ideias")]
        [Range(0.01, 999.99, ErrorMessage = "Os pontos devem estar entre 0,01 e 999,99")]
        [RegularExpression(@"^[0-9]{1,3}(\,[0-9]{1,2})?$|^(\d{3})[\,]$", ErrorMessage = "Por favor informe um número até 999,99")]
        public decimal PontuacaoGradeOrganizacaoIdeias { get; set; }

        [Required(ErrorMessage = "Por favor informe os pontos para Nível de Linguagem")]
        [Display(Name = "Nível de Linguagem")]
        [Range(0.01, 999.99, ErrorMessage = "Os pontos devem estar entre 0,01 e 999,99")]
        [RegularExpression(@"^[0-9]{1,3}(\,[0-9]{1,2})?$|^(\d{3})[\,]$", ErrorMessage = "Por favor informe um número até 999,99")]
        public decimal PontuacaoGradeNivelDeLinguagem { get; set; }

        [Required(ErrorMessage = "Por favor informe os pontos para Domínio das Regras")]
        [Display(Name = "Domínio das Regras")]
        [Range(0.01, 999.99, ErrorMessage = "Os pontos devem estar entre 0,01 e 999,99")]
        [RegularExpression(@"^[0-9]{1,3}(\,[0-9]{1,2})?$|^(\d{3})[\,]$", ErrorMessage = "Por favor informe um número até 999,99")]
        public decimal PontuacaoGradeDominioDasRegras { get; set; }

        public virtual Questao Questao { get; set; }
        public virtual Usuario Usuario { get; set; }

        /// <summary>
        /// Lista de respostas a serem pontuadas pelo professor
        /// </summary>
        public IPagedList<Pontuacao> Lista { get; set; }
    }
}