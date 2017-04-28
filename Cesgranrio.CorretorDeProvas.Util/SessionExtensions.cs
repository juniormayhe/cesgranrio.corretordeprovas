using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cesgranrio.CorretorDeProvas.Util
{
    /// <summary>
    /// Helper para lidar com sessão de usuário 
    /// </summary>
    public static class SessionExtensions
    {
        /// <summary>
        /// Grava uma variavel na sessao
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessao"></param>
        /// <param name="variavel"></param>
        /// <param name="valor"></param>
        public static void Gravar<T>(this HttpSessionStateBase sessao, string variavel, object valor)
        {
            sessao[variavel] = valor;
        }
        /// <summary>
        /// Lê variável desejada da sessão
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessao"></param>
        /// <param name="variavel"></param>
        /// <returns></returns>
        public static T Ler<T>(this HttpSessionStateBase sessao, string variavel)
        {
            return (T)sessao[variavel];
        }
    }
}
