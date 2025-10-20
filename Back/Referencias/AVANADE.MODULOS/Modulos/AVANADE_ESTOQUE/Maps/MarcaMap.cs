﻿using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Maps
{
    public class MarcaMap : IEntityTypeConfiguration<Marca>
    {
        public void Configure(EntityTypeBuilder<Marca> builder)
        {
            builder.ToTable("AVA_MARCA_MAR");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("MAR_ID").IsRequired();
            builder.Property(x => x.Nome).HasColumnName("MAR_NOME").HasMaxLength(100).IsRequired();
        }
    }
}
