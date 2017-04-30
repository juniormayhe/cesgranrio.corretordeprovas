namespace Cesgranrio.CorretorDeProvas.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pontuacao")]
    public partial class Pontuacao
    {
        public int PontuacaoID { get; set; }

        public int UsuarioID { get; set; }

        [StringLength(500)]
        public string PontuacaoRespostaCandidato { get; set; }

        [Required]
        [StringLength(11)]
        public string PontuacaoCPFCandidato { get; set; }

        public int QuestaoID { get; set; }

        public decimal PontuacaoFidelidadeAoTema { get; set; }

        public decimal PontuacaoOrganizacaoDeIdeias { get; set; }

        public decimal PontuacaoNivelDeLinguagem { get; set; }

        public decimal PontuacaoDominioDasRegras { get; set; }

        public virtual Questao Questao { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
