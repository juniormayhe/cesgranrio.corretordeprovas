namespace Cesgranrio.CorretorDeProvas.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Cesgranrio.CorretorDeProvas.DAL.Model;
    using System.Threading.Tasks;
    using System.Threading;

    public partial class CorretorDeProvasDbContext : DbContext, ICorretorDeProvasDbContext
    {
        //"name=CorretorDeProvasDbContext"
        public CorretorDeProvasDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public virtual DbSet<Grupo> Grupo { get; set; }
        public virtual DbSet<Resposta> Resposta { get; set; }
        public virtual DbSet<Questao> Questao { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Candidato> Candidato { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            
            
            modelBuilder.Entity<Candidato>()
                .Property(e => e.CandidatoNome)
                .IsUnicode(false);

            modelBuilder.Entity<Candidato>()
                .Property(e => e.CandidatoCPF)
                .IsUnicode(false);

            modelBuilder.Entity<Candidato>()
                .HasMany(e => e.Resposta)
                .WithRequired(e => e.Candidato)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Grupo>()
                .Property(e => e.GrupoNome)
                .IsUnicode(false);

            modelBuilder.Entity<Grupo>()
                .HasMany(e => e.Usuario)
                .WithRequired(e => e.Grupo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Questao>()
                .Property(e => e.QuestaoEnunciado)
                .IsUnicode(false);

            modelBuilder.Entity<Questao>()
                .Property(e => e.QuestaoGradeFidelidadeAoTema)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Questao>()
                .Property(e => e.QuestaoGradeOrganizacaoIdeias)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Questao>()
                .Property(e => e.QuestaoGradeNivelDeLinguagem)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Questao>()
                .Property(e => e.QuestaoGradeDominioDasRegras)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Questao>()
                .HasMany(e => e.Resposta)
                .WithRequired(e => e.Questao)
                .WillCascadeOnDelete(false);


            

            modelBuilder.Entity<Resposta>()
                .Property(e => e.RespostaFidelidadeAoTema)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Resposta>()
                .Property(e => e.RespostaOrganizacaoDeIdeias)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Resposta>()
                .Property(e => e.RespostaNivelDeLinguagem)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Resposta>()
                .Property(e => e.RespostaDominioDasRegras)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.UsuarioCPF)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.UsuarioSenha)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Resposta)
                .WithRequired(e => e.Usuario)
                .WillCascadeOnDelete(false);
            
        }

    }
}
