using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json.Serialization;

namespace Service.Catalog.Dtos.Equipmentmantain
{
    public class MantainImageDto
    {
        public int Id { get; set; }
        public Guid SolicitudId { get; set; }
        public Guid ExpedienteId { get; set; }
        public string Clave { get; set; }
        public IFormFile Imagen { get; set; }
        public string ImagenUrl { get; set; }
        public string Tipo { get; set; }
        [JsonIgnore]
        public Guid UsuarioId { get; set; }

    }
}
