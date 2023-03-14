using Service.Catalog.Domain.Price;
using Service.MedicalRecord.Domain;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Domain.Promotion
{
    public class Promotion : BaseModel
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string TipoDeDescuento { get; set; }
        public decimal Cantidad { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Activo { get; set; }
        public Guid ListaPrecioId { get; set; }
        public virtual PriceList ListaPrecio { get; set; }
        public bool AplicaMedicos { get; set; }
        public bool Lunes { get; set; }
        public bool Martes { get; set; }
        public bool Miercoles { get; set; }
        public bool Jueves { get; set; }
        public bool Viernes { get; set; }
        public bool Sabado { get; set; }
        public bool Domingo { get; set; }
        public ICollection<PromotionBranch> Sucursales { get; set; } = new List<PromotionBranch>();
        public ICollection<PromotionMedic> Medicos { get; set; } = new List<PromotionMedic>();
        public ICollection<PromotionStudy> Estudios { get; set; } = new List<PromotionStudy>();
        public ICollection<PromotionPack> Paquetes { get; set; } = new List<PromotionPack>();
    }
}
