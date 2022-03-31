using Identidad.Api.Model.Medicos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Service.Catalog.Context.EntityConfiguration.Medics
{
    public class MedicClinicConfiguration : IEntityTypeConfiguration<MedicClinic>
    {
        public void Configure(EntityTypeBuilder<MedicClinic> builder)
        {
            builder.ToTable("CAT_Medico_Clinica");

            builder.HasKey(x => x.IdMedico_Clinica);

            builder
              .Property(x => x.MedicoId)
              .IsRequired(true);

            builder
              .Property(x => x.ClinicaId)
              .IsRequired(true);

            builder
              .Property(x => x.Activo)
              .IsRequired(true);

            builder
              .Property(x => x.UsuarioCreoId)
              .IsRequired(true);

            builder
              .Property(x => x.FechaCreo)
              .IsRequired(true);

            builder
              .Property(x => x.UsuarioModId)
              .IsRequired(false);

            builder
              .Property(x => x.FechaMod)
              .IsRequired(true);
        }
            
    }
}
