using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cesgranrio.CorretorDeProvas.DAL.Database
{
    /// <summary>
    /// Cria conexão com banco de dados
    /// </summary>
    public class DatabaseFactory : IDatabaseFactory
    {
        private readonly string _connectionString;

        public DatabaseFactory() {
            _connectionString = ConfigurationManager.ConnectionStrings["CorretorDeProvasDbContext"].ConnectionString;
        }

        public DatabaseFactory(string connectionString)
        {
            _connectionString = connectionString;        
        }

        public IDbConnection CriarConexao()
        {
            var conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn;
        }
    }
    
}
