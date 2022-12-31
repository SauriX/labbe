using Service.MedicalRecord.Dtos.InvoiceCompany;
using System.Collections.Generic;
using Service.MedicalRecord.Domain.Request;
using System.Linq;
using System;

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
                    Monto = x.TotalEstudios,
                    Nombre = x.Expediente.NombreCompleto,
                    Compania = x.Compañia?.Nombre,
                    CompaniaId = x.Compañia?.Id,
                    ClavePatologica = x.ClavePatologica,
                    Saldo = x.Saldo,
                    Facturas = x.Pagos.Select(y => new InvoiceCompanyFacturaDto
                    {
                        FacturaId = y.FacturaId,
                        EstatusId = y.EstatusId,
                        FechaCreo = y.FechaCreo.ToString("R"),
                        Estatus = new InvoiceCompanyStatusInvoice
                        {
                            Nombre = y.Estatus?.Nombre,
                            Clave = y.Estatus?.Clave,

                        }
                    }).ToList(),
                    Estudios = x.Estudios.Select(y => new InvoiceCompanyStudiesInfoDto
                    {
                        SolicitudId = x.Id,
                        ClaveSolicitud = x.Clave,
                        SolicitudEstudioId = x.Id,
                        Estudio = y.Nombre,
                        Clave = y.Clave,
                        Area = y.AreaId,
                        Precio = y.Precio,
                        PrecioFinal = y.PrecioFinal,
                        Descuento = y.Descuento,
                        DescuentoPorcentaje = y.DescuentoPorcentaje,


                    }).ToList(),
                }).ToList(),
            };
        }
        public static int ToConsecutiveSerie(this RequestPayment model)
        {
            return Int32.Parse(model.Serie) + 1;
        }
    }
}
