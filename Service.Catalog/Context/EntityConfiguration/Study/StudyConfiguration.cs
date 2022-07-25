using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Service.Catalog.Context.EntityConfiguration.Study
{
    public class StudyConfiguration : IEntityTypeConfiguration<Domain.Study.Study>
    {
        public void Configure(EntityTypeBuilder<Domain.Study.Study> builder)
        {
            builder.ToTable("CAT_Estudio");
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Clave)
                .IsRequired(false)
                .HasMaxLength(100);

            builder
                .Property(x => x.Nombre)
                .IsRequired(true)
                .HasMaxLength(100);

            builder
                .Property(x => x.Orden)
                .IsRequired(true);

            builder
                .Property(x => x.Titulo)
                .IsRequired(false)
                .HasMaxLength(100);

            builder
                .Property(x => x.NombreCorto)
                .IsRequired(true)
                .HasMaxLength(50);

            builder
                .Property(x => x.Visible)
                .IsRequired(true);

            builder
                .Property(x => x.DiasResultado)
                .IsRequired(true);

            builder
                .HasOne(x => x.Area)
                .WithMany();

            builder
                .HasOne(x => x.Formato)
                .WithMany();

            builder
                .HasOne(x => x.Maquilador)
                .WithMany();

            builder
                .HasOne(x => x.Metodo)
                .WithMany();

            builder
                .HasOne(x => x.SampleType)
                .WithMany()
                .HasForeignKey(x => x.SampleTypeId);

            builder
                .Property(x => x.Activo)
                .IsRequired(true);

            builder
                .Property(x => x.Prioridad)
                .IsRequired(true);

            builder
               .Property(x => x.Urgencia)
               .IsRequired(true);

            builder
                .HasMany(x => x.WorkLists)
                .WithOne(x => x.Estudio);

            builder
               .HasMany(x => x.Reagents)
               .WithOne(x => x.Estudio);

            builder
               .HasMany(x => x.Packets)
               .WithOne(x => x.Estudio)
               .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Tapon)
                .WithMany();
        }
    }
}
