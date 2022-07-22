using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Identity.Domain.User;

namespace Service.Identity.Context.EntityConfiguration.User
{
    public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.HasKey(x => new { x.UsuarioId, x.MenuId });
        }
    }
}
