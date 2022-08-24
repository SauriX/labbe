using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.EquipmentMantain;

namespace Service.Catalog.Context.EntityConfiguration.EqupmentMantain
{
    public class MantainImageConfiguration : IEntityTypeConfiguration<MantainImages>
    {
        public void Configure(EntityTypeBuilder<MantainImages> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
