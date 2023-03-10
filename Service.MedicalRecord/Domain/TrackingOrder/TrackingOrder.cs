using Service.MedicalRecord.Domain.Catalogs;
using Service.MedicalRecord.Domain.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.MedicalRecord.Domain.TrackingOrder
{
    public class TrackingOrder
    {
        public Guid Id { get; set; }
        public string DestinoId { get; set; }
        public string OrigenId { get; set; }
        public string Clave { get; set; }
        public int MaquiladorId { get; set; }
        public Guid RutaId { get; set; }
        public DateTime DiaRecoleccion { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string Muestra { get; set; }
        public bool Escaneo { get; set; }
        public decimal Temperatura { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
        public virtual ICollection<TrackingOrderDetail> Estudios { get; set; }
        public virtual ICollection<RequestTag> Etiquetas { get; set; }   
    }
}
