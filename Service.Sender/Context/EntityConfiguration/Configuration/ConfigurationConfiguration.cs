using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Sender.Context.EntityConfiguration.Configuration
{
    public class ConfigurationConfiguration : IEntityTypeConfiguration<Domain.EmailConfiguration.EmailConfiguration>
    {
        public void Configure(EntityTypeBuilder<Domain.EmailConfiguration.EmailConfiguration> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Remitente)
                .IsRequired(true)
                .HasMaxLength(4000);

            builder
                .Property(x => x.Correo)
                .IsRequired(true)
                .HasMaxLength(4000);

            builder
                .Property(x => x.Smtp)
                .IsRequired(true)
                .HasMaxLength(4000);

            builder
                .Property(x => x.Contraseña)
                .IsRequired(false)
                .HasMaxLength(4000);
        }
    }
}