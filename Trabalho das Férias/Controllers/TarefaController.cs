using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class TarefaController : Controller
    {
        private TarefaRepository repository;

        public TarefaController()
        {
            repository = new TarefaRepository();
        }

        public ActionResult Index()
        {
            List<Tarefa> tarefas = repository.ObterTodos();
            ViewBag.Tarefas = tarefas;
            return View();
        }

        public ActionResult Cadastro()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            List<Usuario> usuarios = usuarioRepository.ObterTodos();
            ViewBag.Usuarios = usuarios;
            ProjetoRepository projetoRepository = new ProjetoRepository();
            List<Projeto> projetos = projetoRepository.ObterTodos();
            ViewBag.Projetos = projetos;
            CategoriaRepository categoriaRepository = new CategoriaRepository();
            List<Categoria> categorias = categoriaRepository.ObterTodos();
            ViewBag.Categorias = categorias;
            return View();
        }

        public ActionResult Store(string titulo, string descricao, DateTime duracao, int usuario, int projeto, int categoria)
        {
            Tarefa tarefa = new Tarefa();
            tarefa.Titulo = titulo;
            tarefa.Descricao = descricao;
            tarefa.Duracao = duracao;
            tarefa.IdUsuarioResponsavel = usuario;
            tarefa.IdProjeto = projeto;
            tarefa.IdCategoria = categoria;
            repository.Inserir(tarefa);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            Tarefa tarefa = repository.ObterPeloId(id);
            ViewBag.Tarefas = tarefa;

            UsuarioRepository usuarioRepository = new UsuarioRepository();
            List<Usuario> usuarios = usuarioRepository.ObterTodos();
            ViewBag.Usuarios = usuarios;

            ProjetoRepository projetoRepository = new ProjetoRepository();
            List<Projeto> projetos = projetoRepository.ObterTodos();
            ViewBag.Projetos = projetos;

            CategoriaRepository categoriaRepository = new CategoriaRepository();
            List<Categoria> categorias = categoriaRepository.ObterTodos();
            ViewBag.Categorias = categorias;

            return View();
        }

        public ActionResult Update(int id, string titulo, string descricao, DateTime duracao, int usuario, int projeto, int categoria)
        {
            Tarefa tarefa = new Tarefa();
            tarefa.Id = id;
            tarefa.Titulo = titulo;
            tarefa.Descricao = descricao;
            tarefa.Duracao = duracao;
            tarefa.IdUsuarioResponsavel = usuario;
            tarefa.IdProjeto = projeto;
            tarefa.IdCategoria = categoria;

            repository.Alterar(tarefa);
            return RedirectToAction("Index");
        }
    }
}