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
    public class ClienteRepository
    {
        public List<Cliente> ObterTodos()       
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "SELECT * FROM clientes";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            List<Cliente> clientes = new List<Cliente>();

            foreach (DataRow linha in tabela.Rows)
            {
                Cliente cliente = new Cliente();
                cliente.Id = Convert.ToInt32(linha["id"]);
                cliente.Nome = linha["nome"].ToString();
                cliente.Cpf = linha["cpf"].ToString();
                cliente.DataNascimento = Convert.ToDateTime(linha["data_nascimento"].ToString());
                cliente.Numero = Convert.ToInt32(linha["numero"].ToString());
                cliente.Complemento = linha["complemento"].ToString();
                cliente.Logradouro = linha["logradouro"].ToString();
                cliente.Cep = linha["cep"].ToString();
                clientes.Add(cliente);
            }
            comando.Connection.Close();
            return clientes;
        }

        public int Inserir(Cliente cliente)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"INSERT INTO clientes (id_cidade, nome, cpf, data_nascimento, numero, complemento, logradouro, cep)
OUTPUT INSERTED.ID VALUES
(@ID_CIDADE, @NOME, @CPF, @DATA_NASCIMENTO, @NUMERO, @COMPLEMENTO, @LOGRADOURO, @CEP)";
            comando.Parameters.AddWithValue("@ID_CIDADE", cliente.IdCidade);
            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@CPF", cliente.Cpf);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", cliente.DataNascimento);
            comando.Parameters.AddWithValue("@NUMERO", cliente.Numero);
            comando.Parameters.AddWithValue("@COMPLEMENTO", cliente.Complemento);
            comando.Parameters.AddWithValue("@LOGRADOURO", cliente.Logradouro);
            comando.Parameters.AddWithValue("@CEP", cliente.Cep);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public Cliente ObterPeloId(int id)
        {
            SqlCommand command = Conexao.Conectar();
            command.CommandText = "SELECT * FROM clientes WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            command.Connection.Close();
            if (table.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = table.Rows[0];
            Cliente cliente = new Cliente();
            cliente.Id = Convert.ToInt32(row["id"]);
            cliente.Nome = row["nome"].ToString();
            cliente.Cpf = row["cpf"].ToString();
            cliente.DataNascimento = Convert.ToDateTime(row["data_nascimento"].ToString());
            cliente.Numero = Convert.ToInt32(row["numero"].ToString());
            cliente.Complemento = row["complemento"].ToString();
            cliente.Logradouro = row["logradouro"].ToString();
            cliente.Cep = row["cep"].ToString();
            return cliente;
        }

        public bool Alterar(Cliente cliente)
        {
            SqlCommand command = Conexao.Conectar();
            command.CommandText = @"UPDATE clientes SET
nome = @NOME,
id_estado = @ID_ESTADO,
cpf = @CPD,
data_nascimento =@DATA_NASCIMENTO,
numero = @NUMERO,
complemento = @COMPLEMENTO,
logradouro = @LOGRADOURO,
cep = @CEP
WHERE @ID = id";
            command.Parameters.AddWithValue("@ID", cliente.Id);
            command.Parameters.AddWithValue("@ID_CIDADE", cliente.IdCidade);
            command.Parameters.AddWithValue("@NOME", cliente.Nome);
            command.Parameters.AddWithValue("@CPF", cliente.Cpf);
            command.Parameters.AddWithValue("@DATA_NASCIMENTO", cliente.DataNascimento);
            command.Parameters.AddWithValue("@NUMERO", cliente.Numero);
            command.Parameters.AddWithValue("@COMPLEMENTO", cliente.Complemento);
            command.Parameters.AddWithValue("@LOGRADOURO", cliente.Logradouro);
            command.Parameters.AddWithValue("@CEP", cliente.Cep);
            int quantidadeAfetada = command.ExecuteNonQuery();
            command.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public bool Apagar(int id)
        {
            SqlCommand command = Conexao.Conectar();
            command.CommandText = "DELETE FROM clientes WHERE @ID = id";
            command.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = command.ExecuteNonQuery();
            command.Connection.Close();
            return quantidadeAfetada == 1;
        }
    }
}
