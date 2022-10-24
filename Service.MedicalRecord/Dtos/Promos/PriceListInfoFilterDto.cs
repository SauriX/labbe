using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.Promos
{
    public class PriceListInfoFilterDto
    {
        public PriceListInfoFilterDto()
        {
        }

        public PriceListInfoFilterDto(int estudioId, int paqueteId, Guid sucursalId, Guid medicoId, Guid compañiaId, Guid listaPrecioId)
        {
            EstudioId = estudioId;
            PaqueteId = paqueteId;
            SucursalId = sucursalId;
            MedicoId = medicoId;
            CompañiaId = compañiaId;
            ListaPrecioId = listaPrecioId;
        }

        public int? EstudioId { get; set; }
        public int? PaqueteId { get; set; }
        public Guid SucursalId { get; set; }
        public Guid MedicoId { get; set; }
        public Guid CompañiaId { get; set; }
        public Guid ListaPrecioId { get; set; }
        public List<string> Estudios { get; set; } = new List<string>();
    }
}
