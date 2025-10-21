using AVANADE.MODULOS.Data;
using Microsoft.EntityFrameworkCore;

namespace AVANADE.Menssageria.API.Data
{
    public class MenssageriaDbContext : AvanadeDbContextComum<MenssageriaDbContext>
    {
        public MenssageriaDbContext(DbContextOptions<MenssageriaDbContext> options) : base(options)
        {
        }

        // Ex: public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Ex: modelBuilder.ApplyConfigurationsFromAssembly(typeof(MenssageriaDbContext).Assembly);
        }
    }
}