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
    public class QuestaoRepository : IQuestaoRepository
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
        public async Task<IEnumerable<Questao>> ListarAsync()
        {
            _context.Refresh();
            return await _context.Questao.ToListAsync();
        }
        
        /// <summary>
        /// Listar questoes
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Questao> Listar()
        {
            return _context.Questao.ToList();
        }

        /// <summary>
        /// Adicionar questão
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> AdicionarAsync(Questao item)
        {
            _context.Questao.Add(item);
            int r = await _context.SaveChangesAsync();
            _context.Entry(item).Reload();
            _context.Refresh();
            return r;
        }

        /// <summary>
        /// Adicionar questao
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Adicionar(Questao item)
        {
            _context.Questao.Add(item);
            return _context.SaveChanges();
            
        }

        /// <summary>
        /// Procurar por QuestaoID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Questao> ProcurarAsync(int id)
        {
            return _context.Questao.FirstOrDefaultAsync(t => t.QuestaoID == id);
        }

        /// <summary>
        /// Procurar por QuestaoID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Questao Procurar(int id)
        {
            return _context.Questao.FirstOrDefault(t => t.QuestaoID == id);
        }

        /// <summary>
        /// Remover questão
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoverAsync(Questao item)
        {
            //var entity = _context.Questao.First(t => t.QuestaoID == id);
            //_context.Questao.Remove(entity);
            //await _context.SaveChangesAsync();
            //_context.Entry(item).Reload();
            _context.Entry(item).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            
        }

        public void Recarregar(Questao item) { 
            _context.Entry(item).Reload();
        }

        /// <summary>
        /// Remover questao
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Remover(int id)
        {
            var entity = _context.Questao.First(t => t.QuestaoID == id);
            _context.Questao.Remove(entity);
            return _context.SaveChanges();
        }

        /// <summary>
        /// Alterar questão
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task AlterarAsync(Questao item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _context.Entry(item).Reload();
            _context.Refresh();
        }

        /// <summary>
        /// Alterar questão
        /// </summary>
        /// <param name="item"></param>
        /// <param name="controleVersao"></param>
        /// <returns></returns>
        public async Task AlterarAsync(Questao item, byte[] controleVersao)
        {
            _context.Entry(item).OriginalValues["QuestaoControleVersao"] = controleVersao;
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _context.Entry(item).Reload();
            _context.Refresh();
        }

        /// <summary>
        /// Alterar questão
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Alterar(Questao item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return _context.SaveChanges();
        }

        /// <summary>
        /// Verifica se o QuestaoID já existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> ExisteAsync(int id)
        {
            return _context.Questao.AnyAsync(t => t.QuestaoID == id);
        }

        /// <summary>
        /// Verifica se questão já existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Existe(int id)
        {
            return _context.Questao.Any(t => t.QuestaoID == id);
        }

        /// <summary>
        /// Verifica se número de questão já existe
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public async Task<bool> ExisteNumeroAsync(int numero)
        {
            return await _context.Questao.AnyAsync(t => t.QuestaoNumero == numero);
        }

        /// <summary>
        /// Retorna o maior valor de QuestaoID no banco de dados
        /// </summary>
        /// <returns></returns>
        public async Task<int> MaximoIDAsync()
        {
            return await _context.Questao.MaxAsync(t => t.QuestaoID);
        }

        /// <summary>
        /// retorna o maioir numero de questao
        /// </summary>
        /// <returns></returns>
        public async Task<int> MaximoNumeroAsync()
        {
            return await _context.Questao.MaxAsync(t => t.QuestaoNumero); 
        }

        /// <summary>
        /// Retorna o maior valor de QuestaoID no banco de dados
        /// </summary>
        /// <returns></returns>
        public int MaximoID()
        {
            return _context.Questao.Max(t => t.QuestaoID);
        }

        /// <summary>
        /// Procurar pelo número da questão
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public Task<Questao> ProcurarPorNumeroAsync(int numero)
        {
            return _context.Questao.FirstOrDefaultAsync(t => t.QuestaoNumero == numero);
        }

        public Task<Resposta> GetRandom()
        {
            throw new NotImplementedException();
        }
    }
}
