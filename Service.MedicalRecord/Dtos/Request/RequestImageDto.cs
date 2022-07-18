using Microsoft.AspNetCore.Http;
using System;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestImageDto
    {
        public Guid SolicitudId { get; set; }
        public string Clave { get; set; }
        public IFormFile Imagen { get; set; }
        public string ImagenUrl { get; set; }
        public string Tipo { get; set; }
    }
}
