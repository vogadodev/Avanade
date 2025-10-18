using AVANADE.MODULOS.Modulos.AVANADE_AUTH.Entidades;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AVANADE.MODULOS.Modulos.AVANADE_AUTH.Maps
{
    public class RefreshTokenMap : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("AVANADE_USUARIOREFRESHTOKEN_URT");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID).HasColumnName("URT_ID").IsRequired();

            builder.Property(x => x.IDUsuario).HasColumnName("URT_IDUSUARIO").IsRequired();

            builder.Property(x => x.Token).HasColumnName("URT_TOKEN").HasMaxLength(500).IsRequired();

            builder.Property(x => x.Created).HasColumnName("URT_CREATED").IsRequired();

            builder.Property(x => x.Expires).HasColumnName("URT_EXPIRES").IsRequired();

            builder.Property(x => x.IsUsed).HasColumnName("URT_ISUSED").IsRequired();

            builder.Property(x => x.IsRevoked).HasColumnName("URT_REVOKED");

        }
    }
}
