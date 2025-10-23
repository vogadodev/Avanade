using AVANADE.MODULOS.Modulos.AVANADE_USUARIO.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVANADE.MODULOS.Modulos.AVANADE_USUARIO.Maps
{
    internal class UsuarioMaps : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("AVA_USUARIO_USU");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("USU_ID").IsRequired();
            builder.Property(c => c.NomeCompleto).HasColumnName("USU_NOME").IsRequired();
            builder.Property(c => c.Email).HasColumnName("CLI_EMAIL").IsRequired();            
            builder.Property(c => c.Tipo).HasColumnName("CLI_TIPO").IsRequired();
            builder.Property(c => c.DataCadastro).HasColumnName("CLI_DATACADASTRO").IsRequired();
            builder.Property(c => c.DataAtualizacao).HasColumnName("CLI_DATAATUALIZACAO");

            builder.HasIndex(u => u.Email).IsUnique();

            builder.HasOne(c => c.UsuarioPassword)
              .WithOne(ps => ps.Usuario)
              .HasForeignKey<UsuarioPassword>(ps => ps.IdUsuario)
              .IsRequired()
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
