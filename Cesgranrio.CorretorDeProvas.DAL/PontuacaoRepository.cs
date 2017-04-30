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
    public class PontuacaoRepository : IRepository<Pontuacao>
    {
        private ICorretorDeProvasDbContext _context;
        
        public PontuacaoRepository(ICorretorDeProvasDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Listar questoes
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Pontuacao>> Listar()
        {
            return await _context.Pontuacao.ToListAsync();
        }

        /// <summary>
        /// Adicionar questão
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task Adicionar(Pontuacao item)
        {
            if (null == this.ProcurarPorCandidatoCPF(item.PontuacaoCPFCandidato))
            {
                _context.Pontuacao.Add(item);
                await _context.Salvar();
            }
            else {
                throw new ApplicationException($"Já existem respostas cadastradas para o CPF do candidato {item.PontuacaoCPFCandidato}");
            }
            
        }

        /// <summary>
        /// Procurar por PontuacaoID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Pontuacao> Procurar(int id)
        {
            return _context.Pontuacao.FirstOrDefaultAsync(t => t.PontuacaoID == id);
        }

        /// <summary>
        /// Procurar pelo cpf do candidato
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public Task<Pontuacao> ProcurarPorCandidatoCPF(string cpf)
        {
            return _context.Pontuacao.FirstOrDefaultAsync(t => t.PontuacaoCPFCandidato == cpf);
        }

        /// <summary>
        /// Remover questão
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Remover(int id)
        {
            var entity = _context.Pontuacao.First(t => t.QuestaoID == id);
            _context.Pontuacao.Remove(entity);
            await _context.Salvar();
        }

        /// <summary>
        /// Alterar questão
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task Alterar(Pontuacao item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.Salvar();
        }

        /// <summary>
        /// Verifica se a pontuação já existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> Existe(int id)
        {
            return _context.Pontuacao.AnyAsync(t => t.PontuacaoID == id);
        }

        /// <summary>
        /// Retorna o maior valor de PontuacaoID no banco de dados
        /// </summary>
        /// <returns></returns>
        public Task<int> MaximoID()
        {
            return _context.Questao.MaxAsync(t => t.QuestaoID);
        }
    }
}
