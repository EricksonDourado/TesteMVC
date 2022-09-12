using ProvaCandidato.Data.Entidade;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaCandidato.Data
{
  public class ContextoPrincipal : DbContext
  {
    //const string CONNECTION_STRING = @"Server=localhost\SQLEXPRESS;Database=provacandidato;Trusted_Connection=True;";
    const string CONNECTION_STRING = @"Server=localhost\SQLEXPRESS; Database=provacandidato; User Id=sa; Password =sa";
        public ContextoPrincipal() : base(CONNECTION_STRING) { }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Cidade> Cidades { get; set; }
  }
}
