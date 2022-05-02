using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Branch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Context.EntityConfiguration.Branch
{
    public class BranchDepartmentConfiguration : IEntityTypeConfiguration<BranchDepartment>
    {
        public void Configure(EntityTypeBuilder<BranchDepartment> builder)
        {
            builder.ToTable("CAT_Sucursal_Departamento");

            builder.HasKey(x => new { x.SucursalId, x.DepartamentoId });

            //builder
            //    .HasOne(x => x.Sucursal)
            //    .WithMany()
            //    .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Departamento)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
