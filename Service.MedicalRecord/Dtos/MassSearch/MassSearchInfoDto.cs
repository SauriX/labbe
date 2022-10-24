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
        public string Nombre { get; set; }
        public string unidades { get; set; }
        public string Etiqueta { get; set; }
        public string Valor { get; set; }

    }
    public class MassSearchResult
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string paciente { get; set; }
        public int edad { get; set; }
        public string Genero { get; set; }
        public Guid ExpedienteId { get; set; }
        public string NombreEstudio { get; set; }
        public List<MassSearchParameter> Parameters { get; set; }

    }

}
