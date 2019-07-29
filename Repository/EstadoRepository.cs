using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EstadoRepository
    {

        public int Inserir(Estado estado)
        {
            SqlCommand command = Conexao.Conectar();
            command.CommandText = @"INSERT INTO estados (nome, sigla)
            OUTPUT INSERTED.ID
            VALUES (@NOME, @SIGLA)";
            command.Parameters.AddWithValue("@NOME", estado.Nome);
            command.Parameters.AddWithValue("@SIGLA", estado.Sigla);
            int id = Convert.ToInt32(command.ExecuteScalar());

            command.Connection.Close();
            return id;
        }

        public List<Estado> ObterTodos()
        {
            SqlCommand command = Conexao.Conectar();
            command.CommandText = "SELECT * FROM estados";

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            List<Estado> estados = new List<Estado>();

            foreach (DataRow row in table.Rows)
            {
                Estado estado = new Estado();
                estado.Id = Convert.ToInt32(row["id"]);
                estado.Nome = row["nome"].ToString();
                estado.Sigla = row["sigla"].ToString();
                estados.Add(estado);
            }

            command.Connection.Close();
            return estados;
        }

        public bool Apagar(int id)
        {
            SqlCommand command = Conexao.Conectar();
            command.CommandText = "DELETE FROM estados WHERE @ID = id";
            command.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = command.ExecuteNonQuery();

            command.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public bool Alterar(Estado estado)
        {
            SqlCommand command = Conexao.Conectar();
            command.CommandText = @"UPDATE estados SET 
nome = @NOME, 
sigla = @SIGLA
WHERE @ID = id";
            command.Parameters.AddWithValue("@ID", estado.Id);
            command.Parameters.AddWithValue("@NOME", estado.Nome);
            command.Parameters.AddWithValue("@SIGLA", estado.Sigla);
            int quantidadeAfetada = command.ExecuteNonQuery();

            command.Connection.Close();
            return quantidadeAfetada == 1;
        }
    }
}
