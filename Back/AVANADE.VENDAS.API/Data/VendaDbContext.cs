using AVANADE.MODULOS.Data;
using Microsoft.EntityFrameworkCore;

namespace AVANADE.VENDAS.API.Data
{
    public class VendaDbContext : AvanadeDbContextComum<VendaDbContext>
    {
        public VendaDbContext(DbContextOptions<VendaDbContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
