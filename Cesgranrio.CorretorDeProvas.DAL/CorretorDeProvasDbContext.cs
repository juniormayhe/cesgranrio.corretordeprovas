namespace Cesgranrio.CorretorDeProvas.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Cesgranrio.CorretorDeProvas.DAL.Model;
    using System.Threading.Tasks;

    public partial class CorretorDeProvasDbContext : DbContext, ICorretorDeProvasDbContext
    {
        public CorretorDeProvasDbContext()
            : base("name=CorretorDeProvasDbContext")
        {
        }

        public virtual DbSet<Grupo> Grupo { get; set; }
        public virtual DbSet<Pontuacao> Pontuacao { get; set; }
        public virtual DbSet<Questao> Questao { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grupo>()
                .Property(e => e.GrupoNome)
                .IsUnicode(false);

            modelBuilder.Entity<Grupo>()
                .HasMany(e => e.Usuario)
                .WithRequired(e => e.Grupo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pontuacao>()
                .Property(e => e.PontuacaoRespostaCandidato)
                .IsUnicode(false);

            modelBuilder.Entity<Pontuacao>()
                .Property(e => e.PontuacaoCPFCandidato)
                .IsUnicode(false);

            modelBuilder.Entity<Pontuacao>()
                .Property(e => e.PontuacaoFidelidadeAoTema)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Pontuacao>()
                .Property(e => e.PontuacaoOrganizacaoDeIdeias)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Pontuacao>()
                .Property(e => e.PontuacaoNivelDeLinguagem)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Pontuacao>()
                .Property(e => e.PontuacaoDominioDasRegras)
                .HasPrecision(5, 2);

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
                .HasMany(e => e.Pontuacao)
                .WithRequired(e => e.Questao)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.UsuarioCPF)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.UsuarioSenha)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Pontuacao)
                .WithRequired(e => e.Usuario)
                .WillCascadeOnDelete(false);
        }

        public async Task Salvar()
        {
            await base.SaveChangesAsync();
        }

        
        //public Database Database { get => base.Database; }
    }
}
