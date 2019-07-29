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
    public class UsuarioRepository
    {
        public int Inserir(Usuario usuario)
        {
            SqlCommand command = Conexao.Conectar();
            command.CommandText = @"INSERT INTO usuarios (nome, login, senha)
OUTPUT INSERTED.ID VALUES 
(@NOME, @LOGIN, @SENHA)";
            command.Parameters.AddWithValue("@NOME", usuario.Nome);
            command.Parameters.AddWithValue("@LOGIN", usuario.Login);
            command.Parameters.AddWithValue("@SENHA", usuario.Senha);
            int id = Convert.ToInt32(command.ExecuteScalar());
            command.Connection.Close();
            return id;
        }

        public List<Usuario> ObterTodos()
        {
            SqlCommand command = Conexao.Conectar();
            command.CommandText = "SELECT * FROM usuarios";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            List<Usuario> usuarios = new List<Usuario>();

            foreach (DataRow row in table.Rows)
            {
                Usuario usuario = new Usuario();
                usuario.Id = Convert.ToInt32(row["id"]);
                usuario.Nome = row["nome"].ToString();
                usuario.Login = row["login"].ToString();
                usuario.Senha = row["senha"].ToString();
                usuarios.Add(usuario);
            }
            command.Connection.Close();
            return usuarios;
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "DELETE FROM usuarios WHERE @ID = id";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public Usuario ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "SELECT * FROM usuarios WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            comando.Connection.Close();
            if (tabela.Rows.Count == 0)
            {
                return null;
            }
            DataRow linha = tabela.Rows[0];
            Usuario usuario = new Usuario();
            usuario.Id = Convert.ToInt32(linha["id"]);
            usuario.Nome = linha["nome"].ToString();
            usuario.Login = linha["login"].ToString();
            usuario.Senha = linha["senha"].ToString();
            return usuario;
        }

        public bool Alterar(Usuario usuario)
        {
            SqlCommand command = Conexao.Conectar();
            command.CommandText = @"UPDATE usuarios SET
nome = @NOME
WHERE @ID = id";
            command.Parameters.AddWithValue("@NOME", usuario.Nome);
            command.Parameters.AddWithValue("@ID", usuario.Id);
            command.Parameters.AddWithValue("@LOGIN", usuario.Login);
            command.Parameters.AddWithValue("@SENHA", usuario.Senha);
            int quantidadeAfetada = command.ExecuteNonQuery();
            command.Connection.Close();
            return quantidadeAfetada == 1;
        }
    }
}
