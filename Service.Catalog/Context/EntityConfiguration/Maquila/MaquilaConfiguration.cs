using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Reagent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Context.EntityConfiguration.Reagent
{
    public class MaquilaConfiguration : IEntityTypeConfiguration<Domain.Maquila.Maquila>
    {
        public void Configure(EntityTypeBuilder<Domain.Maquila.Maquila> builder)
        {
            builder.ToTable("CAT_Maquilador");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Clave)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(x => x.Correo)
                .IsRequired(false)
                .HasMaxLength(100);  
            
            builder
                .Property(x => x.NumeroExterior)
                .IsRequired()
                .HasMaxLength(10);         
            
            builder
                .Property(x => x.NumeroInterior)
                .IsRequired(false)
                .HasMaxLength(10);

            builder
                .Property(x => x.Calle)
                .IsRequired()
                .HasMaxLength(100);        
            
            builder
                .Property(x => x.Telefono)
                .IsRequired(false)
                .HasMaxLength(13);

            builder
                .Property(x => x.PaginaWeb)
                .IsRequired(false)
                .HasMaxLength(100);

            builder
                .Property(x => x.FechaCreo)
                .HasColumnType("smalldatetime");

            builder
                .Property(x => x.FechaModifico)
                .HasColumnType("smalldatetime");
        }
    }
}
