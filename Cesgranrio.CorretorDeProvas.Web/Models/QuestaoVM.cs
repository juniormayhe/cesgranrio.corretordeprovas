using Cesgranrio.CorretorDeProvas.DAL.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using X.PagedList;
using X.PagedList.Mvc;

namespace Cesgranrio.CorretorDeProvas.Web.Models
{
    
    /// <summary>
    /// Transporta dados das questoes
    /// </summary>
    public class QuestaoVM
    {
        
        public static QuestaoVM CriarQuestaoVM(Questao questao) {
            return new QuestaoVM
            {
                QuestaoID = questao.QuestaoID,
                QuestaoNumero = questao.QuestaoNumero,
                QuestaoEnunciado = questao.QuestaoEnunciado,
                QuestaoGradeDominioDasRegras = questao.QuestaoGradeDominioDasRegras,
                QuestaoGradeFidelidadeAoTema = questao.QuestaoGradeFidelidadeAoTema,
                QuestaoGradeNivelDeLinguagem = questao.QuestaoGradeNivelDeLinguagem,
                QuestaoGradeOrganizacaoIdeias = questao.QuestaoGradeOrganizacaoIdeias,
                QuestaoControleVersao = questao.QuestaoControleVersao,
                Resposta = questao.Resposta
            };
        }
        

        [Key]
        [Display(Name = "ID")]
        public int QuestaoID { get; set; }

        [Required(ErrorMessage = "Por favor informe o número da questão")]
        [Display(Name = "Questão Nº")]
        [Range(1,999, ErrorMessage ="O número da questão deve estar entre 1 e 999")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Por favor informe um número")]
        public int QuestaoNumero { get; set; }

        [Required(ErrorMessage = "Por favor informe o enunciado da questão")]
        [Display(Name = "Enunciado")]
        [MinLength(5, ErrorMessage = "O enunciado deve conter ao menos 5 caracteres"), MaxLength(500, ErrorMessage = "O enunciado deve conter no máximo 500 caracteres")]
        public string QuestaoEnunciado { get; set; }

        //[Required(ErrorMessage = "Por favor informe os pontos para Fidelidade ao tema")]
        [Display(Name = "Fidelidade ao tema")]
        [Range(0.00, 10.00, ErrorMessage ="Os pontos devem estar entre 0,00 a 10,00")]
        [RegularExpression(@"^[0-9]{1,3}(\,[0-9]{1,2})?$|^(\d{3})[\,]$", ErrorMessage = "Por favor informe um número até 999,99")]
        public decimal QuestaoGradeFidelidadeAoTema { get; set; }

        //[Required(ErrorMessage = "Por favor informe os pontos para Organização de ideias")]
        [Display(Name = "Organização de ideias")]
        [Range(0.00, 10.00, ErrorMessage = "Os pontos devem estar entre 0,00 a 10,00")]
        [RegularExpression(@"^[0-9]{1,3}(\,[0-9]{1,2})?$|^(\d{3})[\,]$", ErrorMessage = "Por favor informe um número até 999,99")]
        public decimal QuestaoGradeOrganizacaoIdeias { get; set; }

        //[Required(ErrorMessage = "Por favor informe os pontos para Nível de Linguagem")]
        [Display(Name = "Nível de Linguagem")]
        [Range(0.00, 10.00, ErrorMessage = "Os pontos devem estar entre 0,00 a 10,00")]
        [RegularExpression(@"^[0-9]{1,3}(\,[0-9]{1,2})?$|^(\d{3})[\,]$", ErrorMessage = "Por favor informe um número até 999,99")]
        public decimal QuestaoGradeNivelDeLinguagem { get; set; }

        //[Required(ErrorMessage = "Por favor informe os pontos para Domínio das Regras")]
        [Display(Name = "Domínio das Regras")]
        [Range(0.00, 10.00, ErrorMessage = "Os pontos devem estar entre 0,00 a 10,00")]
        [RegularExpression(@"^[0-9]{1,3}(\,[0-9]{1,2})?$|^(\d{3})[\,]$", ErrorMessage = "Por favor informe um número até 999,99")]
        public decimal QuestaoGradeDominioDasRegras { get; set; }

        public virtual ICollection<Resposta> Resposta { get; set; }

        [Timestamp]
        public byte[] QuestaoControleVersao { get; set; }

        /// <summary>
        /// Lista de questões cadastradas pelo elaborador
        /// </summary>
        public IPagedList<Questao> Lista { get; set; }
        
    }
}