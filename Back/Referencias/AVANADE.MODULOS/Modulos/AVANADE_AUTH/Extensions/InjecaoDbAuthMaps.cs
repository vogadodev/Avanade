using AVANADE.MODULOS.Modulos.AVANADE_AUTH.Maps;
using Microsoft.EntityFrameworkCore;

namespace AVANADE.MODULOS.Modulos.AVANADE_AUTH.Extensions
{
    public static class InjecaoDbAuthMaps
    {
        public static void AddEntitiesMapsAuth(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RefreshTokenMap());
        }
        public static void AddDTOsMapsAuth(ModelBuilder modelBuilder)
        {

        }
    }
}
