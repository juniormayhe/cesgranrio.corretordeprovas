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
        public async Task<IEnumerable<Usuario>> ListarAsync()
        {
            return await _context.Usuario.ToListAsync();
        }

        /// <summary>
        /// Listar usuarios
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Usuario> Listar()
        {
            return _context.Usuario.ToList();
        }

        /// <summary>
        /// Adicionar usuário
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task AdicionarAsync(Usuario item)
        {
            _context.Usuario.Add(item);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Adicionar usuário
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Adicionar(Usuario item)
        {
            _context.Usuario.Add(item);
            return _context.SaveChanges();
        }

        /// <summary>
        /// Procurar por UsuarioID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Usuario> ProcurarAsync(int id)
        {
            return _context.Usuario.FirstOrDefaultAsync(t => t.UsuarioID == id);
        }

        /// <summary>
        /// Procurar por UsuarioID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Usuario Procurar(int id)
        {
            return _context.Usuario.FirstOrDefault(t => t.UsuarioID == id);
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
        public async Task RemoverAsync(Usuario item)
        {
            
            _context.Usuario.Remove(item);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Remover usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Remover(int id)
        {
            var entity = _context.Usuario.First(t => t.UsuarioID == id);
            _context.Usuario.Remove(entity);
            return _context.SaveChanges();
        }

        /// <summary>
        /// Alterar usuário
        /// </summary>
        /// <param name="item"></param>
        /// <param name="original"></param>
        /// <returns></returns>
        public async Task AlterarAsync(Usuario item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Alterar usuario
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Alterar(Usuario item)
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
            return _context.Usuario.AnyAsync(t => t.UsuarioID == id);
        }

        /// <summary>
        /// Verifica se usuário já existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Existe(int id)
        {
            return _context.Usuario.Any(t => t.UsuarioID == id);
        }

        /// <summary>
        /// Retorna o maior valor de UsuarioID no banco de dados
        /// </summary>
        /// <returns></returns>
        public Task<int> MaximoIDAsync()
        {
            return _context.Usuario.MaxAsync(t => t.UsuarioID);
        }

        /// <summary>
        /// Retorna o maior valor de UsuarioID no banco de dados
        /// </summary>
        /// <returns></returns>
        public int MaximoID()
        {
            return _context.Usuario.Max(t => t.UsuarioID);
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

        int IRepository<Usuario>.Adicionar(Usuario item)
        {
            throw new NotImplementedException();
        }

        Task<int> IRepository<Usuario>.AdicionarAsync(Usuario item)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Usuario> IRepository<Usuario>.Listar()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Usuario>> IRepository<Usuario>.ListarAsync()
        {
            throw new NotImplementedException();
        }

        Usuario IRepository<Usuario>.Procurar(int id)
        {
            throw new NotImplementedException();
        }

        Task<Usuario> IRepository<Usuario>.ProcurarAsync(int id)
        {
            throw new NotImplementedException();
        }

        int IRepository<Usuario>.Remover(int id)
        {
            throw new NotImplementedException();
        }

        Task IRepository<Usuario>.RemoverAsync(Usuario item)
        {
            throw new NotImplementedException();
        }

        int IRepository<Usuario>.Alterar(Usuario item)
        {
            throw new NotImplementedException();
        }

        Task IRepository<Usuario>.AlterarAsync(Usuario item)
        {
            throw new NotImplementedException();
        }

        Task IRepository<Usuario>.AlterarAsync(Usuario item, byte[] controleVersao)
        {
            throw new NotImplementedException();
        }

        bool IRepository<Usuario>.Existe(int id)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRepository<Usuario>.ExisteAsync(int id)
        {
            throw new NotImplementedException();
        }

        int IRepository<Usuario>.MaximoID()
        {
            throw new NotImplementedException();
        }

        Task<int> IRepository<Usuario>.MaximoIDAsync()
        {
            throw new NotImplementedException();
        }
    }
}
