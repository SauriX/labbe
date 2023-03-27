using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Service.Sender.Context.EntityConfiguration.NotificationHistory
{
    public class NotificationHistoryConfiguration : IEntityTypeConfiguration<Domain.NotificationHistory.NotificationHistory>
    {
        public void Configure(EntityTypeBuilder<Domain.NotificationHistory.NotificationHistory> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Para)
                .IsRequired(true)
                .HasMaxLength(4000);

            builder
                .Property(x => x.Mensaje)
                .IsRequired(true)
                .HasMaxLength(4000);


        }
    }
}
