using AVANADE.MODULOS.Modulos.AVANADE_USUARIO.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AVANADE.MODULOS.Modulos.AVANADE_USUARIO.Maps
{
    public class UsuarioPasswordMap : IEntityTypeConfiguration<UsuarioPassword>
    {
        public void Configure(EntityTypeBuilder<UsuarioPassword> builder)
        {
            builder.ToTable("AVA_USUARIOPASSWORD_UPW");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).HasColumnName("UPW_ID").IsRequired();
            builder.Property(u => u.IdUsuario).HasColumnName("UPW_IDUSUARIO").IsRequired();
            builder.Property(u => u.Hash).HasColumnName("UPW_HASHPASSWORD").IsRequired();
            builder.Property(u => u.Salt).HasColumnName("UPW_SALTPASSWORD").IsRequired();
        }
    }
}
