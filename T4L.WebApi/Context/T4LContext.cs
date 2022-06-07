using Microsoft.EntityFrameworkCore;
using T4L.Domain.Entities;
using T4L.Domain.Entities.Vw;

namespace T4L.WebApi.Context
{
    public class T4LContext : DbContext
    {
        public T4LContext(DbContextOptions<T4LContext> options) : base(options)
        {                
        }        

        public DbSet<Produto> Produto{ get; set; }
        public DbSet<ProdutoGrupo> ProdutoGrupo { get; set; }
        public DbSet<Venda> Venda { get; set; }
        public DbSet<VendaProduto> VendaProduto { get; set; }
        public DbSet<VwConsulta> VwConsulta { get; set; }
    }
}
