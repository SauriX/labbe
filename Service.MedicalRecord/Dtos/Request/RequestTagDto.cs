using Service.MedicalRecord.Domain;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestTagDto 
    {
        public int Id { get; set; }
        public string Identificador { get; set; }
        public int EtiquetaId { get; set; }
        public string ClaveEtiqueta { get; set; }
        public string ClaveInicial { get; set; }
        public string NombreEtiqueta { get; set; }
        public string Color { get; set; }
        public decimal Cantidad { get; set; }
        public bool Borrado { get; set; }
        public List<RequestTagStudyDto> Estudios { get; set; }
    }

    public class RequestTagStudyDto 
    {
        public int Id { get; set; }
        public int EtiquetaId { get; set; }
        public int EstudioId { get; set; }
        public int Cantidad { get; set; }
        public int Orden { get; set; }
        public bool Manual { get; set; }
        public bool Borrado { get; set; }
        public string Nombre { get; set; }
        public string Identificador { get; set; }
        public string IdentificadorEtiqueta { get; set; }
    }
}