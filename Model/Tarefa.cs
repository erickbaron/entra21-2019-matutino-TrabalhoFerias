using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Tarefa
    {
        public int Id;
        public string Titulo;
        public string Descricao;
        public DateTime Duracao;

        public int IdProjeto;
        public int IdCategoria;
        public int IdUsuarioResponsavel;

        public Projeto Projeto;
        public Categoria Categoria;
        public Usuario Usuario;
    }
}
