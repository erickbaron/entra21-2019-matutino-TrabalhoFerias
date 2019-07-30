using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ProjetoController : Controller
    {
        private ProjetoRepository repository;

        public ProjetoController()
        {
            repository = new ProjetoRepository();
        }

        // GET: Projeto
        public ActionResult Index()
        {
            List<Projeto> projetos = repository.ObterTodos();
            ViewBag.Projetos = projetos;
            return View();
        }

        public ActionResult Cadastro()
        {
            ClienteRepository clienteRepository = new ClienteRepository();
            List<Cliente> clientes = clienteRepository.ObterTodos();
            ViewBag.Clientes = clientes;

            CidadeRepository cidadeRepository = new CidadeRepository();
            List<Cidade> cidades = cidadeRepository.ObterTodos();
            ViewBag.Cidades = cidades;

            EstadoRepository estadoRepository = new EstadoRepository();
            List<Estado> estados = estadoRepository.ObterTodos();
            ViewBag.Estados = estados;

            return View();
        }

        public ActionResult Store(int cliente, string nome, DateTime dataCriacao, DateTime dataFinalizacao)
        {
            Projeto projeto = new Projeto();
            projeto.Nome = nome;
            projeto.IdCliente = cliente;
            projeto.DataCriacao = dataCriacao;
            projeto.DataFinalizacao = dataFinalizacao;
            repository.Inserir(projeto);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            Projeto projeto = repository.ObterPeloId(id);
            ViewBag.Projeto = projeto;

            ClienteRepository clienteRepository = new ClienteRepository();
            List<Cliente> clientes = clienteRepository.ObterTodos();
            ViewBag.Clientes = clientes;

            CidadeRepository cidadeRepository = new CidadeRepository();
            List<Cidade> cidades = cidadeRepository.ObterTodos();
            ViewBag.Cidades = cidades;

            EstadoRepository estadoRepository = new EstadoRepository();
            List<Estado> estados = estadoRepository.ObterTodos();
            ViewBag.Estados = estados;
            return View();
        }

        public ActionResult Update(int id, int cliente, string nome, DateTime dataCriacao, DateTime dataFinalizacao)
        {
            Projeto projeto = new Projeto();
            projeto.Id = id;
            projeto.IdCliente = cliente;
            projeto.Nome = nome;
            projeto.DataCriacao = dataCriacao;
            projeto.DataFinalizacao = dataFinalizacao;

            repository.Alterar(projeto);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }
    }
}