using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            builder
                .HasMany(x => x.Departamentos)
                .WithOne(x => x.Sucursal)
                //.HasForeignKey(x => x.ClinicaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(x => x.Series)
                .WithOne(x => x.Sucursal)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
