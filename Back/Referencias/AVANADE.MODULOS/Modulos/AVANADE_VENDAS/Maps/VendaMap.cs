using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Maps
{
    public class VendaMap : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.ToTable("AVA_VENDA_VEN");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("VEN_ID").IsRequired();
            builder.Property(x => x.ClienteId).HasColumnName("VEN_IDCLIENTE");
            builder.Property(x => x.DataVenda).HasColumnName("VEN_DATAVENDA").IsRequired();
            builder.Property(x => x.ValorTotal).HasColumnName("VEN_VALORTOTAL").HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.StatusPagamento).HasColumnName("VEN_STATUSPAGAMENTO").HasConversion<int>().IsRequired();
            builder.Property(x => x.EstaAtivo).HasColumnName("VEN_ESTAATIVO").IsRequired();

            #region Relacionamentos
            builder.HasMany(x => x.ItensVenda)
                .WithOne(i => i.Venda)
                .HasForeignKey(i => i.VendaId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
