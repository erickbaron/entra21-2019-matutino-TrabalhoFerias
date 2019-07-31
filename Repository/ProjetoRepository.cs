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
    public class ProjetoRepository
    {
        public List<Projeto> ObterTodos()
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "SELECT * FROM projetos";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            List<Projeto> projetos = new List<Projeto>();

            foreach (DataRow linha in tabela.Rows)
            {
                Projeto projeto = new Projeto();
                projeto.Id = Convert.ToInt32(linha["id"]);
                projeto.Nome = linha["nome"].ToString();
                projeto.IdCliente = Convert.ToInt32(linha["id_cliente"]);
                projeto.DataCriacao = Convert.ToDateTime(linha["data_criacao"].ToString());
                projeto.DataFinalizacao = Convert.ToDateTime(linha["data_finalizacao"].ToString());
                projetos.Add(projeto);
            }
            comando.Connection.Close();

            return projetos;
        }

        public int Inserir(Projeto projeto)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"INSERT INTO projetos (id_cliente, nome, data_criacao, data_finalizacao)
OUTPUT INSERTED.ID VALUES
(@ID_CLIENTE, @NOME, @DATA_CRIACAO, @DATA_FINALIZACAO)";
            comando.Parameters.AddWithValue("@ID_CLIENTE", projeto.IdCliente);
            comando.Parameters.AddWithValue("@NOME", projeto.Nome);
            comando.Parameters.AddWithValue("@DATA_CRIACAO", projeto.DataCriacao);
            comando.Parameters.AddWithValue("@DATA_FINALIZACAO", projeto.DataFinalizacao);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public Projeto ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "SELECT * FROM projetos WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            if (tabela.Rows.Count == 0)
            {
                return null;
            }
            DataRow linha = tabela.Rows[0];
            Projeto projeto = new Projeto();
            projeto.IdCliente = Convert.ToInt32(linha["id_cliente"]);
            projeto.Nome = linha["nome"].ToString();
            projeto.DataCriacao = Convert.ToDateTime(linha["data_criacao"]);
            projeto.DataFinalizacao = Convert.ToDateTime(linha["data_finalizacao"]);
            return projeto;
        }

        public bool Alterar(Projeto projeto)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"UPDATE projetos SET
nome = @NOME,
id_cliente = ID_CLIENTE,
data_criacao = @DATA_CRIACAO,
data_finalizacao = @DATA_FINALIZACAO
WHERE @ID = id";
            comando.Parameters.AddWithValue("@ID", projeto.Id);
            comando.Parameters.AddWithValue("@ID_CLIENTE", projeto.IdCliente);
            comando.Parameters.AddWithValue("@NOME", projeto.Nome);
            comando.Parameters.AddWithValue("@DATA_CRIACAO", projeto.DataCriacao);
            comando.Parameters.AddWithValue("@DATA_FINALIZACAO", projeto.DataFinalizacao);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "DELETE FROM projetos WHERE @ID = id";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }
    }
}
