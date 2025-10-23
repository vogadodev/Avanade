using AVANADE.MODULOS.Modulos.AVANADE_USUARIO.Maps;
using Microsoft.EntityFrameworkCore;

namespace AVANADE.MODULOS.Modulos.AVANADE_USUARIO.Extensions
{
    public static class InjecaoDbUsuarioMaps
    {
        public static void AddEntidadesMapsUsuario(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMaps());
            modelBuilder.ApplyConfiguration(new UsuarioPasswordMap());
        }
        public static void AddDTOsMapsUsuario(ModelBuilder modelBuilder)
        {
        }
    }
}
