using Cesgranrio.CorretorDeProvas.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cesgranrio.CorretorDeProvas.Web
{
    public static class UIHelper
    {

        /// <summary>
        /// Retorna classe active para o menu acionado pelo usuário
        /// </summary>
        /// <param name="opcao"></param>
        /// <returns></returns>
        public static string ObterEstiloMenuAtivo(this ViewContext v, string opcao)
        {
            var controller = v.RouteData.Values["controller"].ToString().ToLowerInvariant();
            var acao = v.RouteData.Values["action"].ToString().ToLowerInvariant();
            opcao = opcao.ToLowerInvariant();
            string classe = string.Empty;
            //@ViewContext.ObterEstiloMenuAtivo("Questao")
            if ((controller == opcao && opcao=="questao")
                || (controller == opcao && opcao == "usuario")
                || (controller == "resposta" && opcao == "simularrespostas" && acao ==opcao)
                || (controller == "resposta" && opcao == "corrigirrespostas" && acao == opcao))
                classe ="active";
            return classe;


        }
    }
}