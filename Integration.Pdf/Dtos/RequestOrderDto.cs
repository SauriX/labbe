using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integration.Pdf.Dtos
{
    public class RequestOrderDto
    {
        public string Sucursal { get; set; }
        public string Solicitud { get; set; }
        public string Fecha { get; set; }
        public string FechaSolicitud { get; set; }
        public string FechaNacimiento { get; set; }
        public string Edad { get; set; }
        public string Paciente { get; set; }
        public string Sexo { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public string Medico { get; set; }
        public string Compañia { get; set; }
        public string Observaciones { get; set; }
        public string Descuento { get; set; }
        public string Cargo { get; set; }
        public string Copago { get; set; }
        public string PuntosAplicados { get; set; }
        public string Total { get; set; }
        public string Atiende { get; set; }

        public List<RequestOrderStudyDto> Estudios { get; set; } = new List<RequestOrderStudyDto>();
    }

    public class RequestOrderStudyDto
    {
        public string Clave { get; set; }
        public string Estudio { get; set; }
        public string Precio { get; set; }
    }
}