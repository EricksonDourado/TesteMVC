using ProvaCandidato.Data;
using ProvaCandidato.Data.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaCandidato.Repository
{
    public class CidadesRepository
    {
        public readonly ContextoPrincipal _db;

        public CidadesRepository(ContextoPrincipal db = null)
        {
            _db = db ?? new ContextoPrincipal();
        }

        public void CreateCidade(Cidade cidade)
        {
            try
            {
                _db.Cidades.Add(cidade);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Houve erro na inclusão do cidade. Erro: { ex.Message }");
            }
        }

        public IEnumerable<Cidade> GetName(string nome)
        {
            try
            {
                var cidade = _db.Cidades.Where(c => c.Nome.Contains(nome)).ToList();
                return cidade;
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível recuperar dados do cidade. Erro: { ex.Message }");
            }
        }

        public Cidade GetById(int? codigo)
        {
            try
            {
                if (codigo == null)
                {
                    throw new Exception($"Não foi possível recuperar dados do cidade.");
                }
                var cidade = _db.Cidades.Where(c => c.Codigo == codigo).FirstOrDefault();
                return cidade;
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível recuperar dados do cidade. Erro: { ex.Message }");
            }
        }

        public void DeleteById(int id)
        {
            try
            {
                var cidadeId = GetById(id);
                if (cidadeId != null)
                {
                    _db.Entry(cidadeId).State = System.Data.Entity.EntityState.Deleted;
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha na exclusão. Erro: {ex.Message}");
            }
        }

        public void UpdateCidade(Cidade cidade)
        {
            try
            {              
                    _db.Entry(cidade).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();
               
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha na Edição. Erro: {ex.Message}");
            }
        }

    }
}
