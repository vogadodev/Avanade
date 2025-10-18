using AVANADE.MODULOS.Modulos.AVANADE_AUTH.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AVANADE.MODULOS.Data
{
    public static class InjetarMapsDbContextComum
    {
        public static void AddEntitiesMapsDbContextCommon(ModelBuilder builder)
        {           
            InjecaoDbAuthMaps.AddEntitiesMapsAuth(builder);
        }
        public static void AddDTOsMapsDbContextCommon(ModelBuilder builder)
        {           
            InjecaoDbAuthMaps.AddDTOsMapsAuth(builder);
        }
    }
}
