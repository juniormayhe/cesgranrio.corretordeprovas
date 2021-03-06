﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cesgranrio.CorretorDeProvas.DAL.Model;

namespace Cesgranrio.CorretorDeProvas.DAL
{
    public interface IRepository<T>
    {
        int Adicionar(T item);
        Task<int> AdicionarAsync(T item);

        IEnumerable<T> Listar();
        Task<IEnumerable<T>> ListarAsync();
        T Procurar(int id);
        Task<T> ProcurarAsync(int id);
        
        int Remover(int id);
        Task RemoverAsync(T item);
        int Alterar(T item);
        Task AlterarAsync(T item);
        Task AlterarAsync(T item, byte[] controleVersao);

        bool Existe(int id);
        Task<bool> ExisteAsync(int id);
        int MaximoID();
        Task<int> MaximoIDAsync();
        
        void Recarregar(T item);
    }

    public interface IRespostaRepository : IRepository<Resposta> {
        Task<Resposta> GetRandom();
        
    }

    public interface IQuestaoRepository : IRepository<Questao>
    {
        
        Task<bool> ExisteNumeroAsync(int numero);
        Task<int> MaximoNumeroAsync();
    }
}
