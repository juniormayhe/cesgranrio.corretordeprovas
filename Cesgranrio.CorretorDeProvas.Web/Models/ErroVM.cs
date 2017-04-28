namespace Cesgranrio.CorretorDeProvas.Web.Models
{
    
    /// <summary>
    /// Transporta dados da exceção de aplicação do Global asax para view do ErroController
    /// </summary>
    public class ErroVM
    {
        public string Erro { get; set; }
        public string Descrição { get; set; }
    }
}