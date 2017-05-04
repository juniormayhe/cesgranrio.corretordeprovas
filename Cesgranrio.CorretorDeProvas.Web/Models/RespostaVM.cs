using System;
using Cesgranrio.CorretorDeProvas.DAL.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using X.PagedList;
using X.PagedList.Mvc;

namespace Cesgranrio.CorretorDeProvas.Web.Models
{
    
    /// <summary>
    /// Transporta dados das respostas
    /// </summary>
    public class RespostaVM
    {
        
        public RespostaVM()
        {
            
        }

        public RespostaVM(Resposta resposta)
        {
            this.RespostaID = resposta.RespostaID;
            this.QuestaoID = resposta.QuestaoID;
            this.UsuarioID= resposta.UsuarioID;
            this.CandidatoID = resposta.CandidatoID;
            this.RespostaGradeDominioDasRegras = resposta.RespostaGradeDominioDasRegras;
            this.RespostaGradeFidelidadeAoTema = resposta.RespostaGradeFidelidadeAoTema;
            this.RespostaGradeNivelDeLinguagem = resposta.RespostaGradeNivelDeLinguagem;
            this.RespostaGradeOrganizacaoIdeias = resposta.RespostaGradeOrganizacaoIdeias;
            //this.RespostaControleVersao = Convert.ToBase64String(resposta.RespostaControleVersao);
            this.RespostaControleVersao = resposta.RespostaControleVersao;
            this.RespostaImagem = resposta.RespostaImagem;
            this.Questao = resposta.Questao;
            this.Usuario = resposta.Usuario;
            this.Candidato = resposta.Candidato;
        }

        [Key]
        [Display(Name = "ID")]
        public int RespostaID { get; set; }

        [Display(Name = "Questão")]
        public int QuestaoID { get; set; }

        [Display(Name = "Professor")]
        public int UsuarioID { get; set; }

        [Display(Name = "Candidato")]
        public int CandidatoID { get; set; }

        [Display(Name = "Imagem da prova")]
        public byte[] RespostaImagem { get; set; }

        [Required(ErrorMessage = "Por favor informe os pontos para Fidelidade ao tema")]
        [Display(Name = "Fidelidade ao tema")]
        [Range(0.00, 999.99, ErrorMessage ="Os pontos devem estar entre 0,01 e 999,99")]
        [RegularExpression(@"^[0-9]{1,3}(\,[0-9]{1,2})?$|^(\d{3})[\,]$", ErrorMessage = "Por favor informe um número até 999,99")]
        public decimal RespostaGradeFidelidadeAoTema { get; set; }

        [Required(ErrorMessage = "Por favor informe os pontos para Organização de ideias")]
        [Display(Name = "Organização de ideias")]
        [Range(0.00, 999.99, ErrorMessage = "Os pontos devem estar entre 0,01 e 999,99")]
        [RegularExpression(@"^[0-9]{1,3}(\,[0-9]{1,2})?$|^(\d{3})[\,]$", ErrorMessage = "Por favor informe um número até 999,99")]
        public decimal RespostaGradeOrganizacaoIdeias { get; set; }

        [Required(ErrorMessage = "Por favor informe os pontos para Nível de Linguagem")]
        [Display(Name = "Nível de Linguagem")]
        [Range(0.00, 999.99, ErrorMessage = "Os pontos devem estar entre 0,01 e 999,99")]
        [RegularExpression(@"^[0-9]{1,3}(\,[0-9]{1,2})?$|^(\d{3})[\,]$", ErrorMessage = "Por favor informe um número até 999,99")]
        public decimal RespostaGradeNivelDeLinguagem { get; set; }

        [Required(ErrorMessage = "Por favor informe os pontos para Domínio das Regras")]
        [Display(Name = "Domínio das Regras")]
        [Range(0.00, 999.99, ErrorMessage = "Os pontos devem estar entre 0,01 e 999,99")]
        [RegularExpression(@"^[0-9]{1,3}(\,[0-9]{1,2})?$|^(\d{3})[\,]$", ErrorMessage = "Por favor informe um número até 999,99")]
        public decimal RespostaGradeDominioDasRegras { get; set; }
        
        [Timestamp]
        public byte[] RespostaControleVersao { get; set; }

        public virtual Questao Questao { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Candidato Candidato { get; set; }

        /// <summary>
        /// Lista de respostas a serem pontuadas pelo professor
        /// </summary>
        public IPagedList<Resposta> Lista { get; set; }
    }
}