using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.MassSearch
{
    public class RequestsInfoDto
    {
        public Guid SolicitudId { get; set; }
        public Guid ExpedienteId { get; set; }
        public string ClavePatologica { get; set; }
        public string Solicitud { get; set; }
        public string Nombre { get; set; }
        public string Registro { get; set; }
        public string Sucursal { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public string Compania { get; set; }
        public string Parcialidad { get; set; }
        public decimal Saldo { get; set; }
        public bool SaldoPendiente { get; set; }
        public string EnvioCorreo { get; set; }
        public string EnvioWhatsapp { get; set; }
        public List<RequestsStudiesInfoDto> Estudios { get; set; }

    }

    public class RequestsStudiesInfoDto
    {
        public int EstudioId { get; set; }
        public string Estudio { get; set; }
        public string MedioSolicitado { get; set; }
        public string FechaEntrega { get; set; }
        public string Estatus { get; set; }
        public string Registro { get; set; }
        public bool isPathological{ get; set; }
        public bool IsActiveCheckbox { get; set; }

    }
}
