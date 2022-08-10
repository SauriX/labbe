using Microsoft.EntityFrameworkCore;

namespace Service.Identity.Context.EntityConfiguration.Menu
{
    public class MenuConfiguration : IEntityTypeConfiguration<Domain.Menu.Menu>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Menu.Menu> builder)
        {
            builder.HasKey(x => x.Id);

            //builder
            //    .Property(x => x.Id)
            //    .ValueGeneratedNever();
        }
    }
}
