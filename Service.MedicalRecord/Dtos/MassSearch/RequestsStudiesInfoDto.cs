using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.MassSearch
{
    public class RequestsInfoDto
    {
        public Guid SolicitudId { get; set; }
        public string Solicitud { get; set; }
        public string Nombre { get; set; }
        public string Registro { get; set; }
        public string Sucursal { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public string Compania { get; set; }
        public List<RequestsStudiesInfoDto> Estudios { get; set; }

    }

    public class RequestsStudiesInfoDto
    {
        public string Estudio { get; set; }
        public string MedioSolicitado { get; set; }
        public string FechaEntrega { get; set; }
        public string Estatus { get; set; }
        public string Registro { get; set; }
    }
}
