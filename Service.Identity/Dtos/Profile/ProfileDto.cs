﻿using System;
using System.Collections.Generic;

namespace Service.Identity.Dtos.Profile
{
    public class ProfileDto
    {
        public string Nombre { get; set; }
        public string Token { get; set; }
        public bool RequiereCambio { get; set; }
        public string Sucursal { get; set; }
        public string Rol { get; set; }
        public bool Admin { get; set; }
        public List<Guid> Sucursales { get; set; }
    }
}
