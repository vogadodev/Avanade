using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Maps;
using Microsoft.EntityFrameworkCore;

namespace AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Extensions
{
    public static class InjecaoDbVendasMaps
    {
        public static void AddEntidadesMapsVendas(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ItemVendaMap());
            modelBuilder.ApplyConfiguration(new VendaMap());           
        }
        public static void AddDTOsMapsVendas(ModelBuilder modelBuilder)
        {

        }
    }
}
