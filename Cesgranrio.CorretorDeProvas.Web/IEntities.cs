using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace Cesgranrio.CorretorDeProvas.Web
{
    /// <summary>
    /// Interface para facilitar testes e redução de dependência
    /// </summary>
    public interface IEntities
    {
        
        DbSet<Grupo> Grupo { get; set; }
        DbSet<Pontuacao> Pontuacao { get; set; }
        DbSet<Questao> Questao { get; set; }
        DbSet<Usuario> Usuario { get; set; }

        ObjectResult<string> Autenticar(string usuarioCPF, string usuarioSenha);
        Task SaveChangesAsync();
        DbEntityEntry Entry(Usuario usuario);
        void Dispose();
    }

    public partial class Entities : DbContext, IEntities
    {
        //DbSet<GradeCorrecao> IEntities.GradeCorrecao { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //DbSet<Grupo> IEntities.Grupo { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //DbSet<Pontuacao> IEntities.Pontuacao { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //DbSet<Questao> IEntities.Questao { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //DbSet<Usuario> IEntities.Usuario { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //ObjectResult<string> IEntities.Autenticar(string usuarioCPF, string usuarioSenha)
        //{
        //    throw new NotImplementedException();
        //}

        DbEntityEntry IEntities.Entry(Usuario usuario)
        {
            return base.Entry(usuario);
        }

        Task IEntities.SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}