using System;

namespace Service.Identity.Dtos
{
    public class LoginResponse
    {
        public string token { get; set; }
        public bool changePassword { get; set; }
        public Guid id { get; set; }
    }
}
