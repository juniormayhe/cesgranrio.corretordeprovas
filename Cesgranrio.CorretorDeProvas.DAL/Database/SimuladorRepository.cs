using Cesgranrio.CorretorDeProvas.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cesgranrio.CorretorDeProvas.DAL.Database
{
    public sealed class SimuladorRepository
    {
        private readonly IDatabaseFactory _factory;

        public SimuladorRepository(IDatabaseFactory db)
        {
            _factory = db;
        }

        /// <summary>
        /// Limpa respostas e candidatos
        /// </summary>
        public void LimparRespostasECandidatos() {
            using (SqlConnection db = (SqlConnection)_factory.CriarConexao())
            {
                if (db.State == ConnectionState.Closed) db.Open();
                var command = new SqlCommand("LimparRespostas", db);
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();
            }
            
        }
    
        /// <summary>
        /// Cria uma resposta simulada
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int CriarResposta(Resposta r)
        {
            int i = 0;
            using (SqlConnection db = (SqlConnection)_factory.CriarConexao())
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();    
                var command = new SqlCommand("CriarResposta", db);


                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UsuarioID", r.UsuarioID);
                command.Parameters.AddWithValue("@CandidatoID", r.CandidatoID);
                command.Parameters.AddWithValue("@QuestaoID", r.QuestaoID);
                command.Parameters.AddWithValue("@RespostaImagem", r.RespostaImagem);
                command.Parameters.AddWithValue("@RespostaGradeEscolhida", r.RespostaGradeEscolhida);
                command.Parameters.AddWithValue("@RespostaNota", r.RespostaNota);
                command.Parameters.AddWithValue("@RespostaNotaConcluida", r.RespostaNotaConcluida);
                command.Parameters["@RespostaImagem"].DbType = DbType.Binary;

                //command.Parameters["@ID"].Value = customerID;

                i = command.ExecuteNonQuery();

            }
            return i;
        }
    }
}
