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
    /// Repositório de usuários
    /// </summary>
    public class UsuarioRepository : IRepository<Usuario>, ILoginUsuarioRepository<Usuario>
    {
        private ICorretorDeProvasDbContext _context;

        public UsuarioRepository(ICorretorDeProvasDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Listar usuários
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Usuario>> Listar()
        {
            return await _context.Usuario.ToListAsync();
        }

        /// <summary>
        /// Adicionar usuário
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task Adicionar(Usuario item)
        {
            if (null == this.ProcurarPorCPF(item.UsuarioCPF))
            {
                _context.Usuario.Add(item);
                await _context.Salvar();
            }
            else
            {
                throw new ApplicationException($"Já um usuário com CPF {item.UsuarioCPF}.");
            }
        }

        /// <summary>
        /// Procurar por UsuarioID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Usuario> Procurar(int id)
        {
            return _context.Usuario.FirstOrDefaultAsync(t => t.UsuarioID == id);
        }

        /// <summary>
        /// Procurar pelo CPF
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public Task<Usuario> ProcurarPorCPF(string cpf)
        {
            return _context.Usuario.FirstOrDefaultAsync(t => t.UsuarioCPF == cpf);
        }

        /// <summary>
        /// Remover usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Remover(int id)
        {
            var entity = _context.Usuario.First(t => t.UsuarioID == id);
            _context.Usuario.Remove(entity);
            await _context.Salvar();
        }

        /// <summary>
        /// Alterar usuário
        /// </summary>
        /// <param name="item"></param>
        /// <param name="original"></param>
        /// <returns></returns>
        public async Task Alterar(Usuario item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.Salvar();
        }

        /// <summary>
        /// Verifica se o usuário já existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> Existe(int id)
        {
            return _context.Usuario.AnyAsync(t => t.UsuarioID == id);
        }

        /// <summary>
        /// Retorna o maior valor de UsuarioID no banco de dados
        /// </summary>
        /// <returns></returns>
        public Task<int> MaximoID()
        {
            return _context.Usuario.MaxAsync(t => t.UsuarioID);
        }

        /// <summary>
        /// Autenticar usuário via procedure
        /// </summary>
        /// <param name="usuarioCPF"></param>
        /// <param name="usuarioSenha"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> Autenticar(string usuarioCPF, string usuarioSenha)
        {
            ObjectContext o = ((IObjectContextAdapter)_context).ObjectContext;
            
            var lista = await _context.Database.SqlQuery<string>("EXEC Autenticar {0}, {1}", usuarioCPF, usuarioSenha).ToListAsync();
            return lista.AsEnumerable();
            //return await Task.Run(() => o.ExecuteFunction<string>("Autenticar", new ObjectParameter("UsuarioCPF", usuarioCPF), new ObjectParameter("UsuarioSenha", usuarioSenha)));
        }

        
    }
}
