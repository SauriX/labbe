using Service.MedicalRecord.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Domain.Request
{
    public class RequestTag : BaseModel
    {
        public int Id { get; set; }
        public Guid SolicitudId { get; set; }
        public virtual Request Solicitud { get; set; }
        public string Clave { get; set; }
        public string DestinoId { get; set; }
        public string Destino { get; set; }
        public byte DestinoTipo { get; set; }
        public int EtiquetaId { get; set; }
        public string ClaveEtiqueta { get; set; }
        public string ClaveInicial { get; set; }
        public string NombreEtiqueta { get; set; }
        public string Observaciones { get; set; }
        public string Color { get; set; }
        public decimal Cantidad { get; set; }
        public virtual ICollection<RequestTagStudy> Estudios { get; set; }
    }
}