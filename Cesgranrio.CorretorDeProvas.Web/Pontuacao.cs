//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cesgranrio.CorretorDeProvas.Web
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pontuacao
    {
        public int PontuacaoID { get; set; }
        public int UsuarioID { get; set; }
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