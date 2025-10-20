using AVANADE.MODULOS.Data;
using Microsoft.EntityFrameworkCore;

namespace AVANADE.ESTOQUE.API.Data
{
    public class EstoqueDbContext : AvanadeDbContextComum<EstoqueDbContext>
    {
        public EstoqueDbContext(DbContextOptions<EstoqueDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
