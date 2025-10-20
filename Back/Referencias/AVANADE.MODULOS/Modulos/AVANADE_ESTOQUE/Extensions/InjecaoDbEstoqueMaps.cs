using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Maps;
using Microsoft.EntityFrameworkCore;

namespace AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Extensions
{
    public static class InjecaoDbEstoqueMaps
    {
        public static void AddEntidadesMapsEstoque(ModelBuilder modelBuilder )
        {
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new ArmazemMap());
            modelBuilder.ApplyConfiguration(new AvaliacaoMap());
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new EstoqueArmazemMap());
            modelBuilder.ApplyConfiguration(new FornecedorMap());
            modelBuilder.ApplyConfiguration(new MarcaMap());
            modelBuilder.ApplyConfiguration(new ProdutoEspecificacaoMap());
            modelBuilder.ApplyConfiguration(new ProdutoImagemMap());
        }
        public static void AddDTOsMapsEstoque(ModelBuilder modelBuilder)
        {

        }
    }
}
