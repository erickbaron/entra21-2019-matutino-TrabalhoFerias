using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Trabalho_das_Férias.Controllers
{
    public class CidadeController : Controller
    {
        private CidadeRepository repository;

        public CidadeController()
        {
            repository = new CidadeRepository();
        }

        // GET: Cidade
        public ActionResult Index()
        {
            List<Cidade> cidades = repository.ObterTodos();
            ViewBag.Cidade = cidades;
            return View();
        }

        public ActionResult Cadastro()
        {
            EstadoRepository estadoRepository = new EstadoRepository();
            List<Estado> estados = estadoRepository.ObterTodos();
            ViewBag.Estados = estados;
            return View();
        }

        public ActionResult Store(int estado, string nome, int numeroHabitantes)
        {
            Cidade cidade = new Cidade();
            cidade.IdEstado = estado;
            cidade.Nome = nome;
            cidade.NumeroHabitantes = numeroHabitantes;

            repository.Inserir(cidade);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }


        public ActionResult Editar(int id)
        {
            EstadoRepository EstadoRepository = new EstadoRepository();
            List<Estado> estados = EstadoRepository.ObterTodos();
            ViewBag.Estados = estados;

            Cidade cidade = repository.ObterPeloId(id);
            ViewBag.Cidades = cidade;

            return View();
        }

        public ActionResult Update(int id, int estado, string nome, int numeroHabitantes)
        {

            Cidade cidade = new Cidade();
            cidade.Id = id;
            cidade.IdEstado = estado;
            cidade.Nome = nome;
            cidade.NumeroHabitantes = numeroHabitantes;

            repository.Alterar(cidade);
            return RedirectToAction("Index");
        }

    }
}