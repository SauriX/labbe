using Service.MedicalRecord.Dtos.InvoiceCompany;
using System.Collections.Generic;
using Service.MedicalRecord.Domain.Request;
using System.Linq;

namespace Service.MedicalRecord.Mapper
{
    public static class InvoiceCompanyMapper
    {
        public static InvoiceCompanyInfoDto ToInvoiceCompanyDto(this List<Request> model)
        {
            if (model == null) return null;

            return new InvoiceCompanyInfoDto
            {
                TotalEstudios = model.SelectMany(x => x.Estudios).Count(),
                TotalSolicitudes = model.Count(),
                Total = model.Sum(x => x.TotalEstudios),
                TotalC = model.Sum(x => x.Cargo),
                TotalD = model.Sum(x => x.Descuento),
                TotalPrecio = model.Sum(x => x.Total),
                Solicitudes = model.Select(x => new InvoiceCompanyRequestsInfoDto
                {
                    SolicitudId = x.Id,
                    Cargo = x.Cargo,
                    Clave = x.Clave,
                    Descuento = x.Descuento,
                    Monto = x.Total,
                    Nombre = x.Expediente.NombreCompleto,
                    Compania = x.Compañia?.Nombre,
                    //RFC = x.Compañia?.RF
                    ClavePatologica = x.ClavePatologica,
                    Facturas = x.Pagos.Select(y => new InvoiceCompanyFacturaDto
                    {
                        FacturaId = y.FacturaId,
                        Estatus = y.EstatusId,
                    }).ToList(),
                    Estudios = x.Estudios.Select(y => new InvoiceCompanyStudiesInfoDto
                    {
                        SolicitudEstudioId = x.Id,
                        Estudio = y.Nombre,
                        Clave = y.Clave,
                        Precio = y.Precio,
                        Area = y.AreaId
                    }).ToList(),
                }).ToList(),
            };
        }
    }
}
