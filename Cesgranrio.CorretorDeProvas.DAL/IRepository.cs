using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cesgranrio.CorretorDeProvas.DAL.Model;

namespace Cesgranrio.CorretorDeProvas.DAL
{
    public interface IRepository<T>
    {
        Task Adicionar(T item);
        Task<IEnumerable<T>> Listar();
        Task<T> Procurar(int id);
        Task Remover(int id);
        Task Alterar(T item);
        Task<bool> Existe(int id);
        Task<int> MaximoID();

    }
}
