using AVANADE.MODULOS.Data;
using Microsoft.EntityFrameworkCore;

namespace AVANADE.AUTH.API.Data
{
    public class AuthDbContext : AvanadeDbContextComum<AuthDbContext>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
