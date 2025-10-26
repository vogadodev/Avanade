using AVANADE.MODULOS.Data;
using Microsoft.EntityFrameworkCore;

namespace AVANADE.GATEWAY.API.Data
{
    public class GatewayDbContext : AvanadeDbContextComum<GatewayDbContext>
    {
        public GatewayDbContext(DbContextOptions<GatewayDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
