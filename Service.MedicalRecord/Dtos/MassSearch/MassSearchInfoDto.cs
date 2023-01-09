using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.MassSearch
{
    public class MassSearchInfoDto
    {
        public List<MassSearchParameter> Parameters { get; set; }
        public List<MassSearchResult> Results { get; set; }
    }
    public class MassSearchParameter 
    {
        public Guid Id { get; set; }
        public string TipoValorId { get; set; }
        public int EstudioId { get; set; }
        public int SolicitudEstudioId { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public string Unidades { get; set; }
        public string Etiqueta { get; set; }
        public string Valor { get; set; }

    }
    public class MassSearchResult
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Paciente { get; set; }
        public int Edad { get; set; }
        public string Genero { get; set; }
        public int ReuqestStudyId { get; set; }
        public Guid ExpedienteId { get; set; }
        public string NombreEstudio { get; set; }
        public string HoraSolicitud { get; set; }
        public List<MassSearchParameter> Parameters { get; set; }

    }

}
