using AVANADE.MODULOS.Modulos.AVANADE_AUTH.Extensions;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Extensions;
using AVANADE.MODULOS.Modulos.AVANADE_USUARIO.Extensions;
using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AVANADE.MODULOS.Data
{
    public static class InjetarMapsDbContextComum
    {
        public static void AddEntitiesMapsDbContextCommon(ModelBuilder builder)
        {           
            InjecaoDbAuthMaps.AddEntitiesMapsAuth(builder);
            InjecaoDbEstoqueMaps.AddEntidadesMapsEstoque(builder);           
            InjecaoDbUsuarioMaps.AddEntidadesMapsUsuario(builder);
            InjecaoDbVendasMaps.AddEntidadesMapsVendas(builder);
        }
        public static void AddDTOsMapsDbContextCommon(ModelBuilder builder)
        {           
            InjecaoDbAuthMaps.AddDTOsMapsAuth(builder);
            InjecaoDbEstoqueMaps.AddDTOsMapsEstoque(builder);
            InjecaoDbUsuarioMaps.AddDTOsMapsUsuario(builder);
            InjecaoDbVendasMaps.AddDTOsMapsVendas(builder);

        }
    }
}
