using Service.MedicalRecord.Domain.Catalogs;
using Service.MedicalRecord.Domain.Status;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.MedicalRecord.Domain.Request
{
    public class RequestStudy : BaseModel
    {
        public int Id { get; set; }
        public Guid SolicitudId { get; set; }
        public virtual Request Solicitud { get; set; }
        public int EstudioId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int? PaqueteId { get; set; }
        public virtual RequestPack Paquete { get; set; }
        public Guid ListaPrecioId { get; set; }
        public string ListaPrecio { get; set; }
        public int? PromocionId { get; set; }
        public string Promocion { get; set; }
        public int? DepartamentoId { get; set; }
        public int? AreaId { get; set; }
        public Guid? DestinoId { get; set; }
        public byte EstatusId { get; set; }
        public virtual StatusRequestStudy Estatus { get; set; }
        public decimal Dias { get; set; }
        public int Horas { get; set; }
        public DateTime FechaEntrega { get; set; }
        public int? TaponId { get; set; }
        public virtual Cap Tapon { get; set; }
        public int? MaquilaId { get; set; }
        public virtual Maquila Maquila { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public decimal PrecioFinal { get; set; }
        public string MedioSolicitado { get; set; }
        public string Observacion { get; set; }
        public string Metodo { get; set; }
        public int OrdenEstudio { get; set; }
        public virtual RequestStudyWee EstudioWeeClinic { get; set; }
        // Fechas Actualizacion
        public DateTime? FechaPendiente { get; set; }
        public string UsuarioPendiente { get; set; }
        public DateTime? FechaTomaMuestra { get; set; }
        public string UsuarioTomaMuestra { get; set; }      
        public DateTime? FechaValidacion { get; set; }
        public string UsuarioValidacion { get; set; }     
        public DateTime? FechaSolicitado { get; set; }
        public string UsuarioSolicitado { get; set; }  
        public DateTime? FechaCaptura { get; set; }
        public string UsuarioCaptura { get; set; }     
        public DateTime? FechaLiberado { get; set; }
        public string UsuarioLiberado { get; set; }    
        public DateTime? FechaEnviado { get; set; }
        public string UsuarioEnviado { get; set; }
        // Fechas Actualizacion
        public virtual ICollection<ClinicResults> Resultados { get; set; }
        public virtual ICollection<ClinicalResultsPathological> ResultadosPatologicos { get; set; }
        public string IdArchivoWeeClinic { get; set; }
    }
}
