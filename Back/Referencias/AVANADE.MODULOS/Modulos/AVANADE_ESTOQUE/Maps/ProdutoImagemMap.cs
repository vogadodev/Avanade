using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Maps
{
    public class ProdutoImagemMap : IEntityTypeConfiguration<ProdutoImagem>
    {
        public void Configure(EntityTypeBuilder<ProdutoImagem> builder)
        {
            builder.ToTable("AVA_PRODUTOIMAGEM_PIM");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("PIM_ID").IsRequired();
            builder.Property(x => x.ProdutoId).HasColumnName("PIM_IDPRODUTO").IsRequired();
            builder.Property(x => x.UrlImagem).HasColumnName("PIM_URLIMAGEM").HasMaxLength(1024).IsRequired();
            builder.Property(x => x.TextoAlternativo).HasColumnName("PIM_TEXTOALTERNATIVO").HasMaxLength(200);
            builder.Property(x => x.Ordem).HasColumnName("PIM_ORDEM").IsRequired();
        }
    }
}
