using Service.MedicalRecord.Domain;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestTagDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string DestinoId { get; set; }
        public string Destino { get; set; }
        public byte DestinoTipo { get; set; }
        public int EtiquetaId { get; set; }
        public string ClaveEtiqueta { get; set; }
        public string ClaveInicial { get; set; }
        public string NombreEtiqueta { get; set; }
        public string Color { get; set; }
        public decimal Cantidad { get; set; }
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
        public List<RequestTagStudyDto> Estudios { get; set; }
    }

    public class RequestTagStudyDto
    {
        public int Id { get; set; }
        public int EstudioId { get; set; }
        public decimal Cantidad { get; set; }
        public int Orden { get; set; }
        public string NombreEstudio { get; set; }
    }
}