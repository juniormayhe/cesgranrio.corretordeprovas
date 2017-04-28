using System.Collections.Generic;
using System.Linq;
using X.PagedList;
using X.PagedList.Mvc;

namespace Cesgranrio.CorretorDeProvas.Web.Models
{
    
    /// <summary>
    /// Transporta dados das questoes
    /// </summary>
    public class QuestaoVM
    {
        public IPagedList<Questao> Lista { get; set; }
        
    }
}