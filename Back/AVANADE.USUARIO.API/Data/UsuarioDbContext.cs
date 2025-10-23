using AVANADE.MODULOS.Data;
using Microsoft.EntityFrameworkCore;

namespace AVANADE.USUARIO.API.Data
{
    public class UsuarioDbContext:AvanadeDbContextComum<UsuarioDbContext>
    {
        public UsuarioDbContext(DbContextOptions<UsuarioDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
