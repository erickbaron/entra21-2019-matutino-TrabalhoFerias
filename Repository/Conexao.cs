using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Conexao
    {
        public static SqlCommand Conectar()
        {
            
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Alunos\Downloads\Trabalho\Trabalho das Férias\App_Data\BDTrabalho.mdf;Integrated Security=True;Connect Timeout=30";
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            return command;

        }
    }
}
