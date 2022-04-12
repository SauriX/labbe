using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Service.Identity.Dtos
{
    public class RolForm
    {
        public string Id { get; set; }
        public string nombre { get; set; }
        public bool activo { get; set; }
        public List<UserPermission> permisos { get; set; }
    }
}
