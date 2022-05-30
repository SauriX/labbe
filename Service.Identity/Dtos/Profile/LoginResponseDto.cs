using System;

namespace Service.Identity.Dtos.Profile
{
    public class LoginResponseDto
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public bool CambiaContraseña { get; set; }
    }
}
