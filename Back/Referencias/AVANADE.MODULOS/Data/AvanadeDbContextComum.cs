using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AVANADE.MODULOS.Data
{
    public class AvanadeDbContextComum<T>:DbContext  where T : DbContext
    {
        public AvanadeDbContextComum(DbContextOptions<T> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            InjetarMapsDbContextComum.AddEntitiesMapsDbContextCommon(modelBuilder);
            InjetarMapsDbContextComum.AddDTOsMapsDbContextCommon(modelBuilder);           
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

    }
}
