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
    public class CategoriaRepository
    {
        public List<Categoria> ObterTodos()
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT * FROM categorias";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            List<Categoria> categorias = new List<Categoria>();

            foreach (DataRow linha in tabela.Rows)
            {
                Categoria categoria = new Categoria();
                categoria.Id = Convert.ToInt32(linha["id"]);
                categoria.Nome = linha["nome"].ToString();
                categorias.Add(categoria);
            }
            comando.Connection.Close();
            return categorias;
        }

        public int Inserir(Categoria categoria)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"INSERT INTO categorias (nome)
OUTPUT INSERTED.ID
VALUES (@NOME)";
            comando.Parameters.AddWithValue("@NOME", categoria.Nome);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public Categoria ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "SELECT * FROM categorias WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader()); ;
            comando.Connection.Close();
            if (tabela.Rows.Count == 0)
            {
                return null;
            }

            DataRow linha = tabela.Rows[0];
            Categoria categoria = new Categoria();
            categoria.Id = Convert.ToInt32(linha["id"]);
            categoria.Nome = linha["nome"].ToString();
            return categoria;
        }

        public bool Alterar(Categoria categoria)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "UPDATE categorias SET nome = @NOME WHERE @ID = id";
            comando.Parameters.AddWithValue("@NOME",categoria.Nome);
            comando.Parameters.AddWithValue("@ID",categoria.Id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "DELETE FROM categorias WHERE @ID = id";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }
    }
}
