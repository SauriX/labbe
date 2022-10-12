using Service.MedicalRecord.Domain;
using System;

namespace Service.Catalog.Domain.EquipmentMantain
{
    public class MantainImages : BaseModel
    {
        public MantainImages()
        {
        }

        public MantainImages(int id, Guid solicitudId, string clave, string ruta, string tipo)
        {
            Id = id;
            MantainId = solicitudId;
            Clave = clave;
            Ruta = ruta;
            Tipo = tipo;
        }

        public int Id { get; set; }

        public Guid MantainId { get; set; }
        public virtual Mantain Mantain { get; set; }
        public string Clave { get; set; }
        public string Ruta { get; set; }
        public string Tipo { get; set; }
    }
}
