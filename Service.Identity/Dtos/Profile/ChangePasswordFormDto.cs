using System;

namespace Service.Identity.Dtos.Profile
{
    public class ChangePasswordFormDto
    {
        public string Contraseña { get; set; }
        public string ConfirmaContraseña { get; set; }
        public string Token { get; set; }
        public string Id { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
