using Service.MedicalRecord.Dtos.Invoice;
using Service.MedicalRecord.Dtos.InvoiceCatalog;
using System.Collections.Generic;
using System.Linq;

namespace Service.MedicalRecord.Mapper
{
    public static  class InvoiceCatalogMapper
    {
        public static List<InvoiceCatalogList> ToInvoiceList(this List<InvoiceDto> invoice)
        {
            if (invoice == null) return null;

            return invoice.Select(x=> new InvoiceCatalogList {
                Id = x.FacturapiId.ToString(),
                Clave = x.SerieNumero,
                Serie = x.Serie,
                Descripcion = x.UsoCFDI,
                FechaCreacion = x.CreationDate.ToShortDateString(),
                Solicitud = x.Solicitud,
                Compañia = "",
                Tipo="FAC",
                SolicitudId=x.SolicitudId.ToString(),
                ExpedienteId =x.ExpedienteId.ToString(),
            }).ToList();
        }
        public static List<InvoiceCatalogList> ToInvoiceList(this List<Domain.Request.Request> invoice)
        {
            if (invoice == null) return null;

            return invoice.Select(x => new InvoiceCatalogList
            {
                Id = x.Id.ToString(),
                Clave = x.SerieNumero!=null?x.SerieNumero:"",
                Serie = x.Serie != null ? x.Serie:"",
                Descripcion = "",
                FechaCreacion = x.FechaCreo.ToShortDateString(),
                Solicitud = x.Clave,
                Compañia = x.Compañia?.Nombre,
                Tipo = "REC",
                SolicitudId = x.Id.ToString(),
                ExpedienteId = x.ExpedienteId.ToString(),

            }).ToList();
        }

    }
}
