namespace Cesgranrio.CorretorDeProvas.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Questao")]
    public partial class Questao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Questao()
        {
            Resposta = new HashSet<Resposta>();
        }

        [Key]
        public int QuestaoID { get; set; }

        public int QuestaoNumero { get; set; }

        [Required]
        [StringLength(500)]
        public string QuestaoEnunciado { get; set; }

        public decimal QuestaoGradeFidelidadeAoTema { get; set; }

        public decimal QuestaoGradeOrganizacaoIdeias { get; set; }

        public decimal QuestaoGradeNivelDeLinguagem { get; set; }

        public decimal QuestaoGradeDominioDasRegras { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Resposta> Resposta { get; set; }
    }
}
