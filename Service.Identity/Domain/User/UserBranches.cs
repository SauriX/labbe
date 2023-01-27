using System;

namespace Service.Identity.Domain.User
{
    public class UserBranches
    {
        public Guid UsuarioId { get; set; }
        public virtual User Usuario { get; set; }
        public Guid BranchId { get; set; }
        public Guid? UsuarioCreoId { get; set; }
        public DateTime? FechaCreo { get; set; }
        public Guid? UsuarioModificoId { get; set; }
        public DateTime? FechaModifico { get; set; }
    }
}
