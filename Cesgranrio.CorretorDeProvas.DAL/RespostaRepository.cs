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
    /// Repositório de respostas
    /// </summary>
    public class RespostaRepository : IRepository<Resposta>
    {
        private ICorretorDeProvasDbContext _context;
        
        public RespostaRepository(ICorretorDeProvasDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Listar respostas
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Resposta>> ListarAsync()
        {
            return await _context.Resposta.ToListAsync();
        }

        /// <summary>
        /// Listar respostas
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Resposta> Listar()
        {
            return _context.Resposta.ToList();
        }

        /// <summary>
        /// Adicionar resposta
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task AdicionarAsync(Resposta item)
        {
            _context.Resposta.Add(item);
            await _context.SaveChangesAsync();
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
        public async Task RemoverAsync(int id)
        {
            var entity = _context.Resposta.First(t => t.RespostaID == id);
            _context.Resposta.Remove(entity);
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
        /// Alterar resposta
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task AlterarAsync(Resposta item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
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
        /// Limpar respostas
        /// </summary>
        /// <returns></returns>
        public Task LimparRespostaAsync()
        {
            return _context.Database.ExecuteSqlCommandAsync("EXEC LimparRespostas");
        }

        public void LimparResposta()
        {
            //return _context.Database.ExecuteSqlCommand("EXEC LimparRespostas");
            //_context.Database.SqlQuery<string>("EXEC LimparRespostas");
            _context.Database.ExecuteSqlCommand("exec LimparRespostas");
            //_context.Database.SqlQuery<object>("LimparRespostas");
        }
    }
}
