using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cesgranrio.CorretorDeProvas.DAL.Model;

namespace Cesgranrio.CorretorDeProvas.DAL
{
    public interface ILoginUsuarioRepository<T> : IRepository<T>
    {
        
        Task<Usuario> Autenticar(string v1, string v2);
        
    }
}
