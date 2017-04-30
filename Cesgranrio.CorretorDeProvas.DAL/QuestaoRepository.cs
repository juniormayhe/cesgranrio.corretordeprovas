using Cesgranrio.CorretorDeProvas.DAL.Model;
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
    /// Repositório de questões
    /// </summary>
    public class QuestaoRepository : IRepository<Questao>
    {
        private ICorretorDeProvasDbContext _context;
        
        public QuestaoRepository(ICorretorDeProvasDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Listar questoes
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Questao>> Listar()
        {
            return await _context.Questao.ToListAsync();
        }

        /// <summary>
        /// Adicionar questão
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task Adicionar(Questao item)
        {
            
            _context.Questao.Add(item);
            await _context.Salvar();
            
        }

        /// <summary>
        /// Procurar por QuestaoID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Questao> Procurar(int id)
        {
            return _context.Questao.FirstOrDefaultAsync(t => t.QuestaoID == id);
        }

        /// <summary>
        /// Verifica se o QuestaoID já existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> Existe(int id)
        {
            return _context.Questao.AnyAsync(t => t.QuestaoID == id);
        }

        /// <summary>
        /// Verifica se número de questão já existe
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public Task<bool> ExisteNumero(int numero)
        {
            return _context.Questao.AnyAsync(t => t.QuestaoNumero == numero);
        }

        /// <summary>
        /// Retorna o maior valor de QuestaoID no banco de dados
        /// </summary>
        /// <returns></returns>
        public Task<int> MaximoID()
        {
            return _context.Questao.MaxAsync(t => t.QuestaoID);
        }
        
        /// <summary>
        /// Procurar pelo número da questão
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public Task<Questao> ProcurarPorNumero(int numero)
        {
            return _context.Questao.FirstOrDefaultAsync(t => t.QuestaoNumero == numero);
        }

        /// <summary>
        /// Remover questão
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Remover(int id)
        {
            var entity = _context.Questao.First(t => t.QuestaoID == id);
            _context.Questao.Remove(entity);
            await _context.Salvar();
        }

        /// <summary>
        /// Alterar questão
        /// </summary>
        /// <param name="item"></param>
        /// <param name="original"></param>
        /// <returns></returns>
        public async Task Alterar(Questao item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.Salvar();
        }
 
    }
}
