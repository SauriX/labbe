using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestOrderDto
    {
        public string FolioVenta { get; set; }
        public string FechaVenta { get; set; }

        public string Clave { get; set; }
        public string FechaSolicitud { get; set; }
        public string Paciente { get; set; }
        public string Expediente { get; set; }
        public string Codigo { get; set; }
        public string PacienteId { get; set; }
        public string FechaEntrega { get; set; }
        public string Edad { get; set; }
        public string FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string TelefonoPaciente { get; set; }
        public string Medico { get; set; }
        public string TelefonoMedico { get; set; }
        public string Sucursal { get; set; }
        public string Compañia { get; set; }
        public string Correo { get; set; }
        public string Observaciones { get; set; }
        public string EnvioPaciente { get; set; }

        public string Descuento { get; set; }
        public string Cargo { get; set; }
        public string PuntosAplicados { get; set; }
        public string Total { get; set; }

        public string Fecha { get; set; }
        public string Personal { get; set; }

        public List<RequestOrderStudyDto> Estudios { get; set; }
    }

    public class RequestOrderStudyDto
    {
        public string Clave { get; set; }
        public string Estudio { get; set; }
        public string Precio { get; set; }
    }
}
