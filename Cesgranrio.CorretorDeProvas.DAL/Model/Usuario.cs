namespace Cesgranrio.CorretorDeProvas.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Usuario")]
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            Pontuacao = new HashSet<Pontuacao>();
        }

        public int UsuarioID { get; set; }

        [Required]
        [StringLength(11)]
        public string UsuarioCPF { get; set; }

        [Required]
        [StringLength(50)]
        public string UsuarioSenha { get; set; }

        public int GrupoID { get; set; }

        public virtual Grupo Grupo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pontuacao> Pontuacao { get; set; }
    }
}
