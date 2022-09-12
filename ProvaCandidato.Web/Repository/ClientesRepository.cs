using ProvaCandidato.Data;
using ProvaCandidato.Data.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaCandidato.Repository
{
    public class ClientesRepository
    {
        public readonly ContextoPrincipal _db;

        public ClientesRepository(ContextoPrincipal db = null)
        {
            _db = db ?? new ContextoPrincipal();
        }

        public void CreateCliente(Cliente cliente)
        {
            try
            {
                _db.Clientes.Add(cliente);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Houve erro na inclusão do cliente. Erro: { ex.Message }");
            }
        }

        public IEnumerable<Cliente> GetName(string nome)
        {
            try
            {
                var cliente = _db.Clientes.Where(c => c.Nome.Contains(nome));
                return cliente.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível recuperar dados do cliente. Erro: { ex.Message }");
            }
        }

        public Cliente GetById(int? codigo)
        {
            try
            {
                if (codigo == null)
                {
                    throw new Exception($"Não foi possível recuperar dados do cliente.");
                }
                var cliente = _db.Clientes.Where(c => c.Codigo == codigo).FirstOrDefault();
                return cliente;
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível recuperar dados do cliente. Erro: { ex.Message }");
            }
        }

        public void DeleteById(int id)
        {
            try
            {
                var clienteId = GetById(id);
                if (clienteId != null)
                {
                    _db.Entry(clienteId).State = System.Data.Entity.EntityState.Deleted;
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha na exclusão. Erro: {ex.Message}");
            }
        }

        public void UpdateCliente(Cliente cliente)
        {
            try
            {              
                    _db.Entry(cliente).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();
               
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha na Edição. Erro: {ex.Message}");
            }
        }

    }
}
