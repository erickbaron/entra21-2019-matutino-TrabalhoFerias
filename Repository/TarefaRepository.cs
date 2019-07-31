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
    public class TarefaRepository
    {
        public List<Tarefa> ObterTodos()
        {
            SqlCommand command = Conexao.Conectar();
            command.CommandText = "SELECT * FROM tarefas";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            List<Tarefa> tarefas = new List<Tarefa>();

            foreach (DataRow row in table.Rows)
            {
                Tarefa tarefa = new Tarefa();
                tarefa.Id = Convert.ToInt32(row["id"]);
                tarefa.IdCategoria = Convert.ToInt32(row["id_categoria"]);
                tarefa.IdUsuarioResponsavel = Convert.ToInt32(row["id_usuario_responsavel"]);
                tarefa.IdProjeto = Convert.ToInt32(row["id_projeto"]);
                tarefa.Titulo = row["titulo"].ToString();
                tarefa.Descricao = row["descricao"].ToString();
                tarefa.Duracao = Convert.ToDateTime(row["duracao"].ToString());
                tarefas.Add(tarefa);
            }
            command.Connection.Close();
            return tarefas;
        }


        public int Inserir(Tarefa tarefa)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"INSERT INTO tarefas
(titulo, descricao, duracao, id_usuario_responsavel, id_projeto, id_categoria)
OUTPUT INSERTED.ID VALUES 
(@TITULO, @DESCRICAO, @DURACAO, @ID_USUARIO_RESPONSAVEL, @ID_PROJETO, @ID_CATEGORIA)";
            comando.Parameters.AddWithValue("@TITULO", tarefa.Titulo);
            comando.Parameters.AddWithValue("@DESCRICAO", tarefa.Descricao);
            comando.Parameters.AddWithValue("@DURACAO", tarefa.Duracao);
            comando.Parameters.AddWithValue("@ID_USUARIO_RESPONSAVEL", tarefa.IdUsuarioResponsavel);
            comando.Parameters.AddWithValue("@ID_PROJETO", tarefa.IdProjeto);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", tarefa.IdCategoria);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public Tarefa ObterPeloId(int id)
        {
            SqlCommand command = Conexao.Conectar();
            command.CommandText = "SELECT * FROM tarefas WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            command.Connection.Close();
            if (table.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = table.Rows[0];
            Tarefa tarefa = new Tarefa();
            tarefa.Id = Convert.ToInt32(row["id"]);
            tarefa.IdCategoria = Convert.ToInt32(row["id_categoria"]);
            tarefa.IdUsuarioResponsavel = Convert.ToInt32(row["id_usuario_responsavel"]);
            tarefa.IdProjeto = Convert.ToInt32(row["id_projeto"]);
            tarefa.Titulo = row["titulo"].ToString();
            tarefa.Descricao = row["descricao"].ToString();
            tarefa.Duracao = Convert.ToDateTime(row["duracao"].ToString());
            return tarefa;
        }

        public bool Alterar(Tarefa tarefa)
        {
            SqlCommand command = Conexao.Conectar();
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"UPDATE tarefas SET
titulo = @TITULO,
descricao = @DESCRICAO,
duracao = @DURACAO,
id_usuario_responsavel = @ID_USUARIO_RESPONSAVEL,
id_projeto = @ID_PROJETO,
id_categoria = @ID_CATEGORIA
WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", tarefa.Id);
            comando.Parameters.AddWithValue("@TITULO", tarefa.Titulo);
            comando.Parameters.AddWithValue("@DESCRICAO", tarefa.Descricao);
            comando.Parameters.AddWithValue("@DURACAO", tarefa.Duracao);
            comando.Parameters.AddWithValue("@ID_USUARIO_RESPONSAVEL", tarefa.IdUsuarioResponsavel);
            comando.Parameters.AddWithValue("@ID_PROJETO", tarefa.IdProjeto);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", tarefa.IdCategoria);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public bool Apagar(int id)
        {
            SqlCommand command = Conexao.Conectar();
            command.CommandText = "DELETE FROM tarefas WHERE @ID = id";
            command.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = command.ExecuteNonQuery();
            command.Connection.Close();
            return quantidadeAfetada == 1;
        }
    }
}
