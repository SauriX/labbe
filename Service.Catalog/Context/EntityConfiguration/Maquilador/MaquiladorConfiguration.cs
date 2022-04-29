using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Reagent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Context.EntityConfiguration.Reagent
{
    public class MaquiladorConfiguration : IEntityTypeConfiguration<Domain.Maquilador.Maquilador>
    {
        public void Configure(EntityTypeBuilder<Domain.Maquilador.Maquilador> builder)
        {
            builder.ToTable("CAT_Maquilador");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Clave)
                .IsRequired()
                .HasMaxLength(15);

            builder
                .Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(50);
            builder
                .Property(x => x.Correo)
                .IsRequired(false)
                .HasMaxLength(100);

            builder
                .Property(x => x.PaginaWeb)
                .IsRequired(false)
                .HasMaxLength(500);

            builder
                .Property(x => x.FechaCreo)
                .HasColumnType("smalldatetime");

            builder
                .Property(x => x.FechaMod)
                .HasColumnType("smalldatetime");
        }
    }
}
