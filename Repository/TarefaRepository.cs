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
        public int Inserir(Tarefa tarefa)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"INSERT INTO tarefas
(id_usuario_responsavel, id_projeto, id_categoria, titulo, descricao, duracao)
OUTPUT INSERTED.ID VALUES
(@ID_USUARIO_RESPONSAVEL, @ID_PROJETO, @ID_CATEGORIA, @TITULO, @DESCRICAO, @DURACAO)";
            comando.Parameters.AddWithValue("@ID_USUARIO_RESPONSAVEL", tarefa.IdUsuarioResponsavel);
            comando.Parameters.AddWithValue("@ID_PROJETO", tarefa.Projeto);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", tarefa.Categoria);
            comando.Parameters.AddWithValue("@TITULO", tarefa.Titulo);
            comando.Parameters.AddWithValue("@DESCRICAO", tarefa.Descricao);
            comando.Parameters.AddWithValue("@DURACAO", tarefa.Duracao);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public List<Tarefa> ObterTodos()
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"
SELECT
tarefas.id_usuario_responsavel AS 'IdUsuarioResponsável',
tarefas.id_projeto AS 'IdProjeto',
tarefas.categoria AS 'IdCategoria',
tarefas.titulo AS 'Título',
tarefas.descricao AS 'Descrição',
tarefas.duracao AS 'Duração'
FROM tarefas
INNER JOIN categorias ON (tarefas.id_categoria = categorias.id)
INNER JOIN projetos ON (tarefas.id_projeto = projetos.id)
INNER JOIN usuarios ON (tarefas.id_usuario_responsavel = usuarios.id)
;";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            List<Tarefa> tarefas = new List<Tarefa>();
            foreach (DataRow linha in tabela.Rows)
            {
                Tarefa tarefa = new Tarefa();
                tarefa.Id = Convert.ToInt32(linha["TarefaId"]);
                tarefa.Titulo = linha["Titulo"].ToString();
                tarefa.Descricao = linha["Descrição"].ToString();
                tarefa.Duracao = Convert.ToDateTime(linha["Duração"]);
                tarefa.IdUsuarioResponsavel = Convert.ToInt32(linha["IdUsuarioResponsável"]);
                tarefa.IdProjeto = Convert.ToInt32(linha["IdProjeto"]);
                tarefa.IdCategoria = Convert.ToInt32(linha["IdCategoria"]);

                tarefa.Categoria = new Categoria();
                tarefa.Categoria.Nome = linha["CategoriaNome"].ToString();

                tarefa.Projeto = new Projeto();
                tarefa.Projeto.Nome = linha["ProjetoNome"].ToString();

                tarefa.Usuario = new Usuario();
                tarefa.Usuario.Nome = linha["UsuarioNome"].ToString();
                tarefas.Add(tarefa);
            }

            return tarefas;
        }

        public Tarefa ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "SELECT * FROM tarefas WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            if (tabela.Rows.Count == 0)
            {
                return null;
            }

            DataRow linha = tabela.Rows[0]; 
            Tarefa tarefa = new Tarefa();
            tarefa.Id = Convert.ToInt32(linha["id"]);
            tarefa.Titulo = linha["titulo"].ToString();
            tarefa.Descricao = linha["descrição"].ToString();
            tarefa.Duracao = Convert.ToDateTime(linha["duração"]);
            return tarefa;
        }

        public bool Alterar(Tarefa tarefa)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "UPDATE tarefas SET nome = @NOME WHERE @ID = id";
            comando.Parameters.AddWithValue("@TITULO",tarefa.Titulo);
            comando.Parameters.AddWithValue("@ID",tarefa.Id);
            comando.Parameters.AddWithValue("@DESCRICAO",tarefa.Descricao);
            comando.Parameters.AddWithValue("@DURACAO",tarefa.Duracao);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "DELETE FROM clientes WHERE @ID = id";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;

        }
    }
}
