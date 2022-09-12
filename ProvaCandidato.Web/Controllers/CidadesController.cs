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
    public class CidadesController : GenericaController
    {
        private readonly ContextoPrincipal _db = new ContextoPrincipal();
        private readonly CidadesRepository _cidadeRepository = new CidadesRepository();

        public ActionResult Index(string nome)
        {
            return View(_cidadeRepository.GetName(nome));
        }

        public ActionResult Details(int id)
        {
            try
            {
                if (id == 0)
                {
                    ModelState.AddModelError("", "Código 0 invalido, acesse novamente registro.");
                }
                var cidade = _cidadeRepository.GetById(id);
                             

                return View(cidade);
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi encontrado os dados da Cidade. Erro: {ex.Message}");
            }
        }


        public ActionResult Create()
        {
            var cidades = _db.Cidades.Select(c => c).ToList();

        
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome")] Cidade cidade)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    _cidadeRepository.CreateCidade(cidade);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha na inclusão da Cidade. Erro: {ex.Message}");
            }
            return View(cidade);
        }

        public ActionResult Edit(int id)
        {
            try
            {
                if (id == 0)
                {
                    ModelState.AddModelError("", "Código 0 invalido, acesse novamente registro.");
                }
                var cidade = _cidadeRepository.GetById(id);

                return View(cidade);
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi encontrado os dados da Cidade. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nome")] Cidade cidade)
        {
          
            if (ModelState.IsValid)
            {
                _cidadeRepository.UpdateCidade(cidade);
                return RedirectToAction("index");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                ModelState.AddModelError("", "Código 0 invalido, acesse novamente registro.");
            }
            _cidadeRepository.DeleteById(id);
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

                var cidade = _cidadeRepository.GetById(id);
                if (cidade == null)
                {
                    return View();
                }
                return View(cidade);

            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi encontrado os dados da Cidade. Erro: {ex.Message}");
            }            
        }

    }
}