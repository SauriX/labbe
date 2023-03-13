using Service.MedicalRecord.Dtos.InvoiceCompany;
using System.Collections.Generic;
using Service.MedicalRecord.Domain.Request;
using System.Linq;
using System;
using Service.MedicalRecord.Domain.Invoice;
using Service.MedicalRecord.Dtos.Invoice;
using Service.MedicalRecord.Dtos.Request;

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
                    ExpedienteId = x.ExpedienteId,
                    NombreSucursal = x.Sucursal.Nombre,
                    FormasPagos = x.Pagos.Select(y => y.FormaPago).ToList(),
                    NumerosCuentas = x.Pagos.Select(y => y.NumeroCuenta).ToList(),
                    Facturas = x.FacturasCompañia.Select(y => new InvoiceCompanyFacturaDto
                    {
                        FacturaId = y.FacturaId,
                        FechaCreo = y.FechaCreo.ToString("HH:mm"),
                        Tipo = y.TipoFactura?.ToString(),
                        FacturapiId = y.FacturapiId,
                        Serie = y.Serie,
                        Consecutivo = y.Consecutivo.ToString(),
                        FormaPago = y.FormaPago,
                        Nombre = y.Nombre,
                        CantidadTotal = y.CantidadTotal,
                        SolicitudesId = model
                        .Where(z => z.FacturasCompañia.Select(p => p.FacturapiId).Contains(y.FacturapiId))
                        .Select(z => z.Id)
                        .ToList(),
                        Estatus = new InvoiceCompanyStatusInvoice
                        {
                            Nombre = y.Estatus,
                            Clave = y.Estatus[0].ToString(),

                        }
                    }).ToList(),
                    Estudios = x.Estudios.Select(y => new InvoiceCompanyStudiesInfoDto
                    {
                        SolicitudId = x.Id,
                        ClaveSolicitud = x.Clave,
                        SolicitudEstudioId = y.Id,
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
        public static List<InvoiceFreeDataDto> ToInvoicesFreeDataDto(this List<InvoiceCompany> model)
        {
            return model.Select(x => new InvoiceFreeDataDto
            {
               FechaCreacion = x.FechaCreo.ToString("dd/MM/yyyy"),
               Documento = $"{x.Serie}-{x.Consecutivo}",
               Monto = x.CantidadTotal,
               Cliente = ""
            }).ToList();
        }

        public static InvoiceCompany ToInvoiceCompany(this InvoiceCompanyDto model, InvoiceDto invoiceResponse, InvoiceCompanyDto invoice)
        {
            return new InvoiceCompany
            {
                Id = Guid.NewGuid(),
                Estatus = "Facturado",
                TipoFactura = invoice.TipoFactura,
                OrigenFactura = invoice.OrigenFactura,
                FacturaId = invoiceResponse.Id,
                FacturapiId = invoiceResponse.FacturapiId,
                TaxDataId = model.TaxDataId,
                CompañiaId = model.CompanyId,
                ExpedienteId = model.ExpedienteId,
                //FormaPagoId = model.FormaPagoId,
                FormaPago = model.FormaPago,
                NumeroCuenta = model.NumeroCuenta,
                Serie = model.Serie,
                UsoCFDI = model.UsoCFDI,
                TipoDesgloce = model.TipoDesgloce,
                CantidadTotal = model.CantidadTotal,
                Subtotal = model.Subtotal,
                IVA = model.IVA,
                FechaCreo = DateTime.Now,
                Nombre = model.Nombre,
                Consecutivo = model.Consecutivo,
                BancoId = model.BancoId,
                ClaveExterna = model.ClaveExterna,
                DiasCredito = model.DiasCredito,
                FormaPagoId = model.FormaPagoId,
                TipoPago = model.TipoPago,
                RFC = model.Cliente?.RFC,
                DireccionFiscal = model.Cliente?.DireccionFiscal,
                RazonSocial = model.Cliente?.RazonSocial,
                RegimenFiscal = model.Cliente?.RegimenFiscal,
                DetalleFactura = model.Detalles.Select(x => new InvoiceCompanyDetail
                {
                    Id = Guid.NewGuid(),
                    SolicitudClave = x.SolicitudClave,
                    EstudioClave = x.EstudioClave,
                    Concepto = x.Concepto,
                    Cantidad = x.Cantidad,
                    Importe = x.Importe,
                    ClaveProdServ = x.ClaveProdServ,
                }).ToList(),
            };
        }

        public static InvoiceCompanyDto ToInvoiceDto(this InvoiceCompany model)
        {
            return new InvoiceCompanyDto
            {
                Id = model.Id,
                Estatus = model.Estatus,
                FacturapiId = model.FacturapiId,
                CompañiaId = model.CompañiaId,
                TipoFactura = model.TipoFactura,
                TaxDataId = model.TaxDataId,
                ExpedienteId = model.ExpedienteId,
                FormaPago = model.FormaPago,
                NumeroCuenta = model.NumeroCuenta,
                Serie = model.Serie,
                UsoCFDI = model.UsoCFDI,
                TipoDesgloce = model.TipoDesgloce,
                CantidadTotal = model.CantidadTotal,
                Subtotal = model.Subtotal,
                IVA = model.IVA,
                Consecutivo = model.Consecutivo,
                Nombre = model.Nombre,
                FacturaId = model.FacturaId,
                BancoId = model.BancoId,
                ClaveExterna = model.ClaveExterna,
                DiasCredito = model.DiasCredito,
                FormaPagoId = model.FormaPagoId,
                TipoPago = model.TipoPago,
                OrigenFactura = model.OrigenFactura,
                RFC = model.RFC,
                DireccionFiscal = model.DireccionFiscal,
                RazonSocial = model.RazonSocial,
                RegimenFiscal = model.RegimenFiscal,
                Detalles = model.DetalleFactura.Select(x => new InvoiceDetail
                {
                    ClaveProdServ = x.ClaveProdServ,
                    SolicitudClave = x.SolicitudClave,
                    EstudioClave = x.EstudioClave,
                    Concepto = x.Concepto,
                    Cantidad = x.Cantidad,
                    Importe = x.Importe,
                    Descuento = x.Descuento
                }).ToList(),

                


            };
        }


        public static int ToConsecutiveSerie(this RequestPayment model)
        {
            return Int32.Parse(model.Serie) + 1;
        }
        public static RequestTicketDto ToRequestTicketDto(this ReceiptCompanyDto model, List<Domain.Request.RequestStudy> requestStudies, List<Domain.Request.Request> requests)
        {

            var totalEstudios = requestStudies.Sum(x => x.Precio);
            var subtotal = totalEstudios - (totalEstudios / 100) * 16;
            var iva = (totalEstudios / 100) * 16;
            var saldo = requests.Sum(x => x.Saldo);
            var anticipo = requests.Sum(x => x.Copago); //TODO: como calcular el anticipo? 
            var descuento = requests.Sum(x => x.Descuento);  

            return new RequestTicketDto
            {
                DireccionSucursal = "Laboratorio Alfonso Ramos, S.A. de C.V. Avenida Humberto Lobo #555 A, Col. del Valle C.P. 66220 San Pedro Garza García, Nuevo León.",
                Contacto = "Tel/WhatsApp: 81 4170 0769 RFC: LAR900731TL0",
                Sucursal = $"SUCURSAL {model.Sucursal}", // "SUCURSAL MONTERREY"
                Folio = model.Folio,
                Fecha = DateTime.Now.ToString("dd/MM/yyyy"),
                Atiende = model.Atiende.ToUpper(),
                Subtotal = subtotal.ToString("C"),
                Descuento = descuento.ToString("C"),
                IVA = iva.ToString("C"),
                Total = totalEstudios.ToString("C"),
                Anticipo = anticipo.ToString("C"),
                Saldo = saldo.ToString("C"),
                Usuario = "",
                Contraseña = "",
                ContactoTelefono = ""
                
            };
        }
    }
}
