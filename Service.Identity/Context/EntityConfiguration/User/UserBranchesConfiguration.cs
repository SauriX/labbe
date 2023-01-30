using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Identity.Domain.User;

namespace Service.Identity.Context.EntityConfiguration.User
{
    public class UserBranchesConfiguration : IEntityTypeConfiguration<UserBranches>
    {
        public void Configure(EntityTypeBuilder<UserBranches> builder)
        {
            builder.ToTable("Relacion_User_Sucursal");

            builder.HasKey(x => new { x.UsuarioId, x.BranchId });
        }
    }
}
