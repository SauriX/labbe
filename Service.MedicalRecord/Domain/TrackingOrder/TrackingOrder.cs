using Service.MedicalRecord.Domain.Catalogs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.MedicalRecord.Domain.TrackingOrder
{
    public class TrackingOrder
    {
        public Guid Id { get; set; }
        public string SucursalDestinoId { get; set; }

        public string SucursalOrigenId { get; set; }
      
        public string Clave { get; set; }
        public int MaquiladorId { get; set; }
        public string RutaId { get; set; }
        public DateTime DiaRecoleccion { get; set; }
        public string MuestraId { get; set; }
        public bool EscaneoCodigoBarras { get; set; }
        public double Temperatura { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
        public virtual ICollection<TrackingOrderDetail> Estudios { get; set; }
        
    }
}
