using Cesgranrio.CorretorDeProvas.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cesgranrio.CorretorDeProvas.DAL
{
    /// <summary>
    /// Repositório de candidato
    /// </summary>
    public class CandidatoRepository : IRepository<Candidato>
    {
        private ICorretorDeProvasDbContext _context;

        public CandidatoRepository(ICorretorDeProvasDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Listar candidatos
        /// </summary>
        
        /// <returns></returns>
        public async Task<IEnumerable<Candidato>> ListarAsync()
        {
            _context.Refresh();
            return await _context.Candidato.ToListAsync();
        }

        /// <summary>
        /// Listar usuarios
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Candidato> Listar()
        {
            return _context.Candidato.ToList();
        }

        /// <summary>
        /// Adicionar usuário
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> AdicionarAsync(Candidato item)
        {
            _context.Candidato.Add(item);
            int r = await _context.SaveChangesAsync();
            _context.Entry(item).Reload();
            return r;
        }

        /// <summary>
        /// Adicionar usuário
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Adicionar(Candidato item)
        {
            _context.Candidato.Add(item);
            return _context.SaveChanges();
        }

        /// <summary>
        /// Procurar por UsuarioID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Candidato> ProcurarAsync(int id)
        {
            return _context.Candidato.FirstOrDefaultAsync(t => t.CandidatoID == id);
        }

        /// <summary>
        /// Procurar por UsuarioID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Candidato Procurar(int id)
        {
            return _context.Candidato.FirstOrDefault(t => t.CandidatoID == id);
        }

        /// <summary>
        /// Procurar pelo CPF
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public Task<Candidato> ProcurarPorCPF(string cpf)
        {
            return _context.Candidato.FirstOrDefaultAsync(t => t.CandidatoCPF == cpf);
        }

        /// <summary>
        /// Remover usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoverAsync(Candidato item)
        {
            
            _context.Candidato.Remove(item);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Remover usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Remover(int id)
        {
            var entity = _context.Candidato.First(t => t.CandidatoID == id);
            _context.Candidato.Remove(entity);
            return _context.SaveChanges();
        }

        /// <summary>
        /// Alterar candidato com verificacao de versao
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task AlterarAsync(Candidato item, byte[] respostaControleVersao)
        {
            _context.Entry(item).OriginalValues["CandidatoControleVersao"] = respostaControleVersao;
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _context.Entry(item).Reload();
        }

        /// <summary>
        /// Alterar candidato
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task AlterarAsync(Candidato item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _context.Entry(item).Reload();

        }

        /// <summary>
        /// Alterar usuario
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Alterar(Candidato item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return _context.SaveChanges();
        }

        /// <summary>
        /// Verifica se o usuário já existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> ExisteAsync(int id)
        {
            return _context.Candidato.AnyAsync(t => t.CandidatoID == id);
        }

        /// <summary>
        /// Verifica se usuário já existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Existe(int id)
        {
            return _context.Candidato.Any(t => t.CandidatoID == id);
        }

        /// <summary>
        /// Retorna o maior valor de UsuarioID no banco de dados
        /// </summary>
        /// <returns></returns>
        public Task<int> MaximoIDAsync()
        {
            return _context.Candidato.MaxAsync(t => t.CandidatoID);
        }

        /// <summary>
        /// Retorna o maior valor de UsuarioID no banco de dados
        /// </summary>
        /// <returns></returns>
        public int MaximoID()
        {
            return _context.Candidato.Max(t => t.CandidatoID);
        }

        public Task<Resposta> GetRandom()
        {
            throw new NotImplementedException();
        }
    }
}
