﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScopesController : ControllerBase
    {
        [HttpGet("reagent")]
        public ScopeDto GetReagentScopes()
        {
            return new ScopeDto
            {
                Pantalla = "Catálogo de Reactivos",
                Acceder = true,
                Crear = true,
                Editar = true,
                Descargar = true,
            };
        }

        [HttpGet("medics")]
        public ScopeDto GetMedicsScopes()
        {
            return new ScopeDto
            {
                Pantalla = "Catálogo de Medicos",
                Acceder = true,
                Crear = true,
                Editar = true,
                Descargar = true,
            };
        }
    }

}
