using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cesgranrio.CorretorDeProvas.DAL.Database
{
    public interface IDatabaseFactory
    {
        IDbConnection CriarConexao();
    }
}
