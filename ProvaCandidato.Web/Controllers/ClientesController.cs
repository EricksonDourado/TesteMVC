using ProvaCandidato.Data;
using ProvaCandidato.Data.Entidade;
using ProvaCandidato.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProvaCandidato.Controllers
{
    public class ClientesController : GenericaController
    {
        private readonly ContextoPrincipal _db = new ContextoPrincipal();
        private readonly ClientesRepository _clienteRepository = new ClientesRepository();

        public ActionResult Index(string nome)
        {
            return View(_clienteRepository.GetName(nome));
        }

        public ActionResult Details(int id)
        {
            try
            {
                if (id == 0)
                {
                    return new HttpNotFoundResult();
                }
                var cliente = _clienteRepository.GetById(id);
                var cidades = _db.Cidades.Select(c => c).ToList();

                ViewBag.CidadeId = new SelectList(cidades, "Codigo", "Nome");

                return View(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi encontrado os dados do Cliente. Erro: {ex.Message}");
            }
        }


        public ActionResult Create()
        {
            var cidades = _db.Cidades.Select(c => c).ToList();

            ViewBag.CidadeId = new SelectList(cidades, "Codigo", "Nome");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome, DataNascimento, CidadeId, Ativo")] Cliente cliente)
        {
            try
            {

                if (cliente.DataNascimento >= DateTime.Now)
                {
                    ModelState.AddModelError("", "Data invalida, informe data menor que a data atual.");
                }
                if (ModelState.IsValid)
                {
                    _clienteRepository.CreateCliente(cliente);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha na inclusão do Cliente. Erro: {ex.Message}");
            }

            var cidades = _db.Cidades.Select(c => c).ToList();

            ViewBag.CidadeId = new SelectList(cidades, "Codigo", "Nome");
            return View(cliente);
        }

        public ActionResult Edit(int id)
        {
            try
            {
                if (id == 0)
                {
                    ModelState.AddModelError("", "Código de Cliente inválido, verifique novamente.");
                }
                var cliente = _clienteRepository.GetById(id);
                var cidades = _db.Cidades.Select(c => c).ToList();

                ViewBag.CidadeId = new SelectList(cidades, "Codigo", "Nome");

                return View(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi encontrado os dados do Cliente. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nome, DataNascimento, CidadeId, Ativo")] Cliente cliente)
        {
            if (cliente.DataNascimento >= DateTime.Now)
            {
                ModelState.AddModelError("", "Data invalida, informe data menor que a data atual.");
            }
            if (ModelState.IsValid)
            {
                _clienteRepository.UpdateCliente(cliente);
                return RedirectToAction("index");
            }

            var cidades = _db.Cidades.Select(c => c).ToList();
            ViewBag.CidadeId = new SelectList(cidades, "Codigo", "Nome");
            return View(cliente);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                ModelState.AddModelError("", "Código 0 invalido, acesse novamente registro.");
            }
            _clienteRepository.DeleteById(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    ModelState.AddModelError("", "Código 0 invalido, acesse novamente registro.");
                }

                var cliente = _clienteRepository.GetById(id);
                if (cliente == null)
                {
                    return View();
                }
                return View(cliente);

            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi encontrado os dados do Cliente. Erro: {ex.Message}");
            }
        }
    }


}