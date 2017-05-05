using System.Data.Entity;
using System.Threading.Tasks;
using Cesgranrio.CorretorDeProvas.DAL.Model;
using System.Data.Entity.Infrastructure;
using System;
using System.Threading;

namespace Cesgranrio.CorretorDeProvas.DAL
{
    public interface ICorretorDeProvasDbContext : IDisposable
    {
        DbSet<Grupo> Grupo { get; set; }
        DbSet<Resposta> Resposta { get; set; }
        DbSet<Questao> Questao { get; set; }
        DbSet<Usuario> Usuario { get; set; }
        DbSet<Candidato> Candidato{ get; set; }
        void Refresh();
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();

        DbEntityEntry Entry(object o);
        
        System.Data.Entity.Database Database { get; }
    }
}