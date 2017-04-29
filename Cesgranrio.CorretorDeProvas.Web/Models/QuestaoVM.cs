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
        

        public QuestaoVM()
        {
            this.Pontuacao = new HashSet<Pontuacao>();
        }

        public QuestaoVM(Questao questao)
        {
            this.QuestaoID = questao.QuestaoID;
            this.QuestaoNumero = questao.QuestaoNumero;
            this.QuestaoEnunciado = questao.QuestaoEnunciado;
            this.QuestaoGradeDominioDasRegras = questao.QuestaoGradeDominioDasRegras;
            this.QuestaoGradeFidelidadeAoTema = questao.QuestaoGradeFidelidadeAoTema;
            this.QuestaoGradeNivelDeLinguagem = questao.QuestaoGradeNivelDeLinguagem;
            this.QuestaoGradeOrganizacaoIdeias = questao.QuestaoGradeOrganizacaoIdeias;
            this.Pontuacao = questao.Pontuacao;
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

        [Required(ErrorMessage = "Por favor informe os pontos para Fidelidade ao tema")]
        [Display(Name = "Fidelidade ao tema")]
        [Range(0.01, 999.99, ErrorMessage ="Os pontos devem estar entre 0,01 e 999,99")]
        [RegularExpression(@"^[0-9]{1,3}(\,[0-9]{1,2})?$|^(\d{3})[\,]$", ErrorMessage = "Por favor informe um número até 999,99")]
        public decimal QuestaoGradeFidelidadeAoTema { get; set; }

        [Required(ErrorMessage = "Por favor informe os pontos para Organização de ideias")]
        [Display(Name = "Organização de ideias")]
        [Range(0.01, 999.99, ErrorMessage = "Os pontos devem estar entre 0,01 e 999,99")]
        [RegularExpression(@"^[0-9]{1,3}(\,[0-9]{1,2})?$|^(\d{3})[\,]$", ErrorMessage = "Por favor informe um número até 999,99")]
        public decimal QuestaoGradeOrganizacaoIdeias { get; set; }

        [Required(ErrorMessage = "Por favor informe os pontos para Nível de Linguagem")]
        [Display(Name = "Nível de Linguagem")]
        [Range(0.01, 999.99, ErrorMessage = "Os pontos devem estar entre 0,01 e 999,99")]
        [RegularExpression(@"^[0-9]{1,3}(\,[0-9]{1,2})?$|^(\d{3})[\,]$", ErrorMessage = "Por favor informe um número até 999,99")]
        public decimal QuestaoGradeNivelDeLinguagem { get; set; }

        [Required(ErrorMessage = "Por favor informe os pontos para Domínio das Regras")]
        [Display(Name = "Domínio das Regras")]
        [Range(0.01, 999.99, ErrorMessage = "Os pontos devem estar entre 0,01 e 999,99")]
        [RegularExpression(@"^[0-9]{1,3}(\,[0-9]{1,2})?$|^(\d{3})[\,]$", ErrorMessage = "Por favor informe um número até 999,99")]
        public decimal QuestaoGradeDominioDasRegras { get; set; }

        public virtual ICollection<Pontuacao> Pontuacao { get; set; }

        public IPagedList<Questao> Lista { get; set; }
        
    }
}