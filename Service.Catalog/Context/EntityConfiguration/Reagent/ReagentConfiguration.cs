using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Reagent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Context.EntityConfiguration.Reagent
{
    public class ReagentConfiguration : IEntityTypeConfiguration<Domain.Reagent.Reagent>
    {
        public void Configure(EntityTypeBuilder<Domain.Reagent.Reagent> builder)
        {
            builder.ToTable("CAT_Reactivo_Contpaq");

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
                .Property(x => x.ClaveSistema)
                .IsRequired(false)
                .HasMaxLength(15);

            builder
                .Property(x => x.NombreSistema)
                .IsRequired(false)
                .HasMaxLength(50);

            builder
                .Property(x => x.FechaCreo)
                .HasColumnType("smalldatetime");

            builder
                .Property(x => x.FechaModifico)
                .HasColumnType("smalldatetime");
        }
    }
}
