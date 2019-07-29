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
    }
}
