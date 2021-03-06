﻿using Cesgranrio.CorretorDeProvas.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cesgranrio.CorretorDeProvas.DAL
{
    /// <summary>
    /// Repositório de respostas
    /// </summary>
    public class RespostaRepository : IRespostaRepository
    {
        private ICorretorDeProvasDbContext _context;
        private static Random rand = new Random();

        public RespostaRepository(ICorretorDeProvasDbContext context)
        {
            _context = context;
            _context.Database.CommandTimeout = 120;
        }

        /// <summary>
        /// Listar respostas
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Resposta>> ListarAsync()
        {
            _context.Refresh();
            return await _context.Resposta.AsNoTracking().Where(x=>x.RespostaNotaConcluida != true) .Take(1000).ToListAsync();
        }
        
        /// <summary>
        /// Listar respostas
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Resposta> Listar()
        {
            return _context.Resposta.AsNoTracking().AsEnumerable<Resposta>();
        }

        /// <summary>
        /// Adicionar resposta
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> AdicionarAsync(Resposta item)
        {
            _context.Resposta.Add(item);
            int r = await _context.SaveChangesAsync();
            _context.Entry(item).Reload();
            return r;
        }

        /// <summary>
        /// Adicionar resposta
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Adicionar(Resposta item)
        {
            _context.Resposta.Add(item);
            return _context.SaveChanges();
        }

        /// <summary>
        /// Procurar por RespostaID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Resposta> ProcurarAsync(int id)
        {
            return _context.Resposta.FirstOrDefaultAsync(t => t.RespostaID == id);
        }

        /// <summary>
        /// Procurar por RespostaID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Resposta Procurar(int id)
        {
            return _context.Resposta.FirstOrDefault(t => t.RespostaID == id);
        }
        
        /// <summary>
        /// Remover resposta
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoverAsync(Resposta item)
        {
            
            _context.Resposta.Remove(item);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Remover resposta
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Remover(int id)
        {
            var entity = _context.Resposta.First(t => t.RespostaID == id);
            _context.Resposta.Remove(entity);
            return _context.SaveChanges();
        }


        /// <summary>
        /// Alterar resposta com verificacao de versao
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task AlterarAsync(Resposta item, byte[] controleVersao)
        {
            _context.Entry(item).OriginalValues["RespostaControleVersao"] = controleVersao;
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _context.Entry(item).Reload();
            _context.Refresh();
        }

        /// <summary>
        /// Alterar resposta
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task AlterarAsync(Resposta item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _context.Entry(item).Reload();
            
        }

        /// <summary>
        /// Alterar resposta
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Alterar(Resposta item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return _context.SaveChanges();
        }
        
        /// <summary>
        /// Verifica se a resposta já existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> ExisteAsync(int id)
        {
            return _context.Resposta.AnyAsync(t => t.RespostaID == id);
        }

        /// <summary>
        /// Verifica se resposta já existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Existe(int id)
        {
            return _context.Resposta.Any(t => t.RespostaID == id);
        }
        
        /// <summary>
        /// Retorna o maior valor de RespostaID no banco de dados
        /// </summary>
        /// <returns></returns>
        public Task<int> MaximoIDAsync()
        {
            return _context.Resposta.MaxAsync(t => t.RespostaID);
        }
        
        /// <summary>
        /// Retorna o maior valor de RespostaID no banco de dados
        /// </summary>
        /// <returns></returns>
        public int MaximoID()
        {
            return _context.Resposta.Max(t => t.RespostaID);
        }
        
        /// <summary>
        /// obtem uma resposta para dar nota de modo aleatorio
        /// </summary>
        /// <returns></returns>
        public async Task<Resposta> GetRandom()
        {
            _context.Refresh();
            var lista =  await _context.Resposta.AsNoTracking().Where(x => x.RespostaNotaConcluida == null || x.RespostaNotaConcluida == false).ToListAsync();
            return lista.ElementAtOrDefault(rand.Next(lista.Count()));
        }

        /// <summary>
        /// Recarregar item
        /// </summary>
        /// <param name="item"></param>
        public void Recarregar(Resposta item)
        {
            _context.Entry(item).Reload();
        }
    }
}
