using Cesgranrio.CorretorDeProvas.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cesgranrio.CorretorDeProvas.Simulador
{
    /// <summary>
    /// somente uma extensão
    /// </summary>
    internal static class RespostaExtensions
    {
        /// <summary>
        /// representação texto de uma resposta
        /// </summary>
        /// <returns></returns>
        public static string ParaTexto(this Resposta p)
        {
            return $"Enunciado: {p.Questao.QuestaoEnunciado} -> CPF: {p.Candidato.CandidatoCPF}, Nome: {p.Candidato.CandidatoNome}, Organizacao: {p.RespostaGradeOrganizacaoIdeias}, Linguagem: {p.RespostaGradeNivelDeLinguagem}, Fidelidade: {p.RespostaGradeFidelidadeAoTema}, Regras: {p.RespostaGradeDominioDasRegras}";
        }
    }
}
