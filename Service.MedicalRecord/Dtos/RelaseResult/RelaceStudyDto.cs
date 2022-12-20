using System;

namespace Service.MedicalRecord.Dtos.RelaseResult
{
    public class RelaceStudyDto
    {
        public int Id { get; set; }
        public string Study { get; set; }
        public string Area { get; set; }
        public string Status { get; set; }
        public string Registro { get; set; }
        public string Entrega { get; set; }
        public int Estatus { get; set; }
        public Guid SolicitudId { get; set; }
        public bool Tipo { get; set; }
        public string Clave { get; set; }
        public string NombreEstatus { get; set; }

        public string Nombre { get; set; }




    }
}
