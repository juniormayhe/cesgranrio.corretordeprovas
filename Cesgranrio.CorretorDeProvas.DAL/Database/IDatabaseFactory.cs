using System.Data;

namespace Cesgranrio.CorretorDeProvas.DAL.Database
{
    public interface IDatabaseFactory
    {
        IDbConnection CriarConexao();
        
    }
}
