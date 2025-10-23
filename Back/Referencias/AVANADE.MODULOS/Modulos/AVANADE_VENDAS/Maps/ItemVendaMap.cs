using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Maps
{
    public class ItemVendaMap : IEntityTypeConfiguration<ItemVenda>
    {
        public void Configure(EntityTypeBuilder<ItemVenda> builder)
        {
            builder.ToTable("AVA_ITEMVENDA_ITV");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ITV_ID").IsRequired();
            builder.Property(x => x.VendaId).HasColumnName("ITV_IDVENDA").IsRequired();
            builder.Property(x => x.ProdutoId).HasColumnName("ITV_IDPRODUTO").IsRequired();
            builder.Property(x => x.Quantidade).HasColumnName("ITV_QUANTIDADE").IsRequired();
            builder.Property(x => x.PrecoUnitario).HasColumnName("ITV_PRECOUNITARIO").HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.EstaAtivo).HasColumnName("ITV_ESTAATIVO").IsRequired();
        }
    }
}
