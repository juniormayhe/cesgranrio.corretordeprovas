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

        public static RespostaVM CriarRespostaVM(Resposta resposta) {

            return new RespostaVM
            {
                RespostaID = resposta.RespostaID,
                QuestaoID = resposta.QuestaoID,
                UsuarioID = resposta.UsuarioID,
                CandidatoID = resposta.CandidatoID,
                RespostaGradeEscolhida = resposta.RespostaGradeEscolhida,
                RespostaNota = resposta.RespostaNota,
                RespostaControleVersao = resposta.RespostaControleVersao,
                RespostaImagem = resposta.RespostaImagem,
                Questao = resposta.Questao,
                Usuario = resposta.Usuario,
                Candidato = resposta.Candidato,
                RespostaNotaConcluida = resposta.RespostaNotaConcluida
            };
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

        [Required]
        public int GradeEscolhida { get; set; }

        //[Required(ErrorMessage = "Por favor informe os pontos para Fidelidade ao tema")]
        [Display(Name = "Nota da prova")]
        //[Range(0.00, 10.00, ErrorMessage ="Os pontos devem estar entre 0,00 a 10,00 conforme grade escolhida")]
        [RegularExpression(@"^[0-9]{1,3}(\,[0-9]{1,2})?$|^(\d{3})[\,]$", ErrorMessage = "Por favor informe um número válido")]
        public decimal RespostaNota { get; set; }

        public int RespostaGradeEscolhida { get; set; }


        [Timestamp]
        public byte[] RespostaControleVersao { get; set; }

        public virtual Questao Questao { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Candidato Candidato { get; set; }

        /// <summary>
        /// Lista de respostas a serem pontuadas pelo professor
        /// </summary>
        public IPagedList<Resposta> Lista { get; set; }
        public bool? RespostaNotaConcluida { get; private set; }
    }
}