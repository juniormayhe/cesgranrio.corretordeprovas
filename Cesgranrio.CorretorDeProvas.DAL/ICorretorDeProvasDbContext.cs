using System.Data.Entity;
using System.Threading.Tasks;
using Cesgranrio.CorretorDeProvas.DAL.Model;
using System.Data.Entity.Infrastructure;

namespace Cesgranrio.CorretorDeProvas.DAL
{
    public interface ICorretorDeProvasDbContext
    {
        DbSet<Grupo> Grupo { get; set; }
        DbSet<Pontuacao> Pontuacao { get; set; }
        DbSet<Questao> Questao { get; set; }
        DbSet<Usuario> Usuario { get; set; }

        Task Salvar();
        DbEntityEntry Entry(object o);
        
        Database Database { get; }
    }
}