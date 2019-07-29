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
    public class CidadeRepository
    {
        public int Inserir(Cidade cidade)
        {
            SqlCommand command = Conexao.Conectar();
            command.CommandText = @"INSERT INTO cidades (id_estado, nome, numero_habitantes)
OUTPUT INSERTED.ID VALUES 
(@ID_ESTADO,@NOME, @NUMERO_HABITANTES)";
            command.Parameters.AddWithValue("@ID_ESTADO", cidade.IdEstado);
            command.Parameters.AddWithValue("@NOME", cidade.Nome);
            command.Parameters.AddWithValue("@NUMERO_HABITANTES", cidade.NumeroHabitantes);
            int id = Convert.ToInt32(command.ExecuteScalar());
            command.Connection.Close();
            return id;
        }

        public List<Cidade> ObterTodos()
        {
            SqlCommand command = Conexao.Conectar();
            command.CommandText = "SELECT * FROM cidades";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            List<Cidade> cidades = new List<Cidade>();

            foreach (DataRow row in table.Rows)
            {
                Cidade cidade = new Cidade();
                cidade.Id = Convert.ToInt32(row["id"]);
                cidade.IdEstado = Convert.ToInt32(row["id_estado"]);
                cidade.Nome = row["nome"].ToString();
                cidade.NumeroHabitantes = Convert.ToInt32(row["numero_habitantes"]);
                cidades.Add(cidade);
            }

            command.Connection.Close();
            return cidades;
        }

        public bool Apagar(int id)
        {
            SqlCommand command = Conexao.Conectar();
            command.CommandText = "DELETE FROM cidades WHERE @ID = id";
            command.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = command.ExecuteNonQuery();

            command.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public bool Alterar(Cidade cidade)
        {
            SqlCommand command = Conexao.Conectar();
            command.CommandText = @"UPDATE cidades SET
nome = @NOME,
numero_habitantes = @NUMERO_HABITANTES
WHERE @ID = id";
            command.Parameters.AddWithValue("@ID", cidade.Id);
            command.Parameters.AddWithValue("@NOME", cidade.Nome);
            command.Parameters.AddWithValue("@NUMERO_HABITANTES", cidade.NumeroHabitantes);
            int quantidadeAfetada = command.ExecuteNonQuery();

            command.Connection.Close();
            return quantidadeAfetada == 1;
        }
    }
}
