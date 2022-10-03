using Microsoft.EntityFrameworkCore;
using Service.Identity.Domain.User;
namespace Service.Identity.Context.EntityConfiguration.User
{
    public class UserConfiguracion : IEntityTypeConfiguration<Domain.User.User>
    {


        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.User.User> builder)
        {
            builder
            .HasMany(x => x.Imagenes)
                        .WithOne(x => x.User)
                        .HasForeignKey(x => x.UserId)
                        .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
