using System;

namespace Service.Catalog.Domain.Branch
{
    public class BranchStudy
    {
        public Guid Id { get; set; }
        public Guid BranchId { get; set; }
        public int EstudioId { get; set; }
        public bool Activo { get; set; }
        public Guid? UsuarioCreoId { get; set; }
        public DateTime? FechaCreo { get; set; }
        public Guid? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}
