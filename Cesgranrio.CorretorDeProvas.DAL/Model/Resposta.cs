namespace Cesgranrio.CorretorDeProvas.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Resposta")]
    public partial class Resposta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RespostaID { get; set; }
        public int UsuarioID { get; set; }
        
        public int CandidatoID { get; set; }
        public int QuestaoID { get; set; }
        [Column(TypeName = "image")]
        public byte[] RespostaImagem { get; set; }
        public decimal RespostaFidelidadeAoTema { get; set; }
        public decimal RespostaOrganizacaoDeIdeias { get; set; }
        public decimal RespostaNivelDeLinguagem { get; set; }
        public decimal RespostaDominioDasRegras { get; set; }

        public virtual Candidato Candidato { get; set; }
        public virtual Questao Questao { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
