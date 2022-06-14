using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Route;

namespace Service.Catalog.Context.EntityConfiguration.Route
{
    public class Route_StudyConfiguration : IEntityTypeConfiguration<Route_Study>
    {
        public void Configure(EntityTypeBuilder<Route_Study> builder)
        {
            builder.ToTable("Relacion_Ruta_Estudio");

            builder.HasKey(x => new
            {
                x.RouteId,
                x.EstudioId,
            });
        }
    }
}
