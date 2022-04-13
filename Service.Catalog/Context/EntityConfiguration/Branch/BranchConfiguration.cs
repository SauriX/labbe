using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Context.EntityConfiguration.Branch
{
    public class BranchConfiguration : IEntityTypeConfiguration<Domain.Branch.Branch>
    {
        public void Configure(EntityTypeBuilder<Domain.Branch.Branch> builder)
        {
            builder.ToTable("CAT_Sucursal");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.FechaCreo)
                .HasColumnType("smalldatetime");

            builder
                .Property(x => x.FechaModifico)
                .HasColumnType("smalldatetime");
        }
    }
}
