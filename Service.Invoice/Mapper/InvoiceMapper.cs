using Service.Billing.Domain.Invoice;
using Service.Billing.Dtos.Facturapi;
using Service.Billing.Dtos.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Billing.Mapper
{
    public static class InvoiceMapper
    {
        public static InvoiceDto ToInvoiceDto(this Invoice model)
        {
            if (model == null) return null;

            return new InvoiceDto
            {
                Id = model.Id,
                FormaPago = model.FormaPago,
                MetodoPago = model.MetodoPago,
                UsoCFDI = model.UsoCFDI,
                RegimenFiscal = model.RegimenFiscal,
                RFC = model.RFC,
                Desglozado = model.Desglozado,
                ConNombre = model.ConNombre,
                EnvioCorreo = model.EnvioCorreo,
                EnvioWhatsapp = model.EnvioWhatsapp,
                SolicitudId = model.SolicitudId,
                Solicitud = model.Solicitud,
                ExpedienteId = model.ExpedienteId,
                Expediente = model.Expediente,
                Paciente = model.Paciente,
                CreationDate = model.FechaCreo,
                FacturapiId = model.FacturapiId,
                Serie = model.Serie,
                SerieNumero= model.SerieNumero,
            };
        }
        public static List<InvoiceDto> ToInvoiceDto(this InvoiceCompany model)
        {
            if (model == null) return null;

            return model.Solicitudes.Select(x=>new InvoiceDto
            {
                Id = model.Id,
                FormaPago = model.FormaPago,
                MetodoPago = model.MetodoPago,
                UsoCFDI = model.UsoCFDI,
                RegimenFiscal = model.RegimenFiscal,
                RFC = model.RFC,
                Desglozado = model.Desglozado,
                ConNombre = model.ConNombre,
                EnvioCorreo = model.EnvioCorreo,
                EnvioWhatsapp = model.EnvioWhatsapp,
                CreationDate = model.FechaCreo,
                FacturapiId = model.FacturapiId,
               SolicitudId= x.SolicitudId ,
       

    }).ToList();
        }
        public static List<InvoiceDto> ToInvoiceDto(this List<InvoiceCompany> model)
        {
            List<InvoiceDto> list = new List<InvoiceDto>();
            if (model == null) return null;
            foreach (InvoiceCompany company in model) {
                var invoice = company.ToInvoiceDto();
                list.AddRange(invoice);
            }
            return list;
        }
        public static List<InvoiceDto> ToInvoiceDto(this List<Invoice> model)
        {
            if (model == null) return null;

            return model.Select(x => x.ToInvoiceDto()).ToList();
        }

        public static FacturapiDto ToFacturapiDto(this InvoiceDto dto)
        {
            if (dto == null) return null;

            return new FacturapiDto
            {
                ClaveExterna = dto.Id.ToString(),
                FormaPago = dto.FormaPago.Split(" ")[0],
                UsoCDFI = dto.UsoCFDI.Split(" ")[0],
                Cliente = new FacturapiClientDto
                {
                    RazonSocial = dto.Cliente.RazonSocial,
                    RFC = dto.Cliente.RFC,
                    RegimenFiscal = dto.Cliente.RegimenFiscal.Split(" ")[0],
                    Correo = dto.Cliente.Correo,
                    Telefono = dto.Cliente.Telefono,
                    Domicilio = new FacturapiAddressDto
                    {
                        CodigoPostal = dto.Cliente.CodigoPostal,
                        Calle = dto.Cliente.Calle,
                        //NumeroExterior = dto.Cliente?.NumeroInterior,
                        //NumeroInterior = dto.Cliente?.NumeroExterior,
                        Colonia = dto.Cliente?.Colonia == null ? "": dto.Cliente?.Colonia,
                        Ciudad = dto.Cliente.Ciudad,
                        Municipio = dto.Cliente.Ciudad,
                        Estado = dto.Cliente.Estado,
                        Pais = dto.Cliente.Pais,
                    }
                },
                Productos = dto.Productos.Select(x => new FacturapiProductDto
                {
                    ClaveProductoSAT = x.ClaveProdServ,
                    Clave = x.Clave,
                    Descripcion = x.Descripcion,
                    Precio = x.Precio,
                    Descuento = x.Descuento,
                    Cantidad = 1
                }).ToList(),
            };
        }

        public static Invoice ToModel(this InvoiceDto dto)
        {
            if (dto == null) return null;

            return new Invoice
            {
                Id = Guid.NewGuid(),
                Serie = dto.Serie,
                FormaPago = dto.FormaPago,
                MetodoPago = dto.MetodoPago,
                UsoCFDI = dto.UsoCFDI,
                RegimenFiscal = dto.Cliente.RegimenFiscal,
                RFC = dto.Cliente.RFC,
                Desglozado = dto.Desglozado,
                ConNombre = dto.ConNombre,
                EnvioCorreo = dto.EnvioCorreo,
                EnvioWhatsapp = dto.EnvioWhatsapp,
                SolicitudId = dto.SolicitudId,
                Solicitud = dto.Solicitud,
                ExpedienteId = dto.ExpedienteId,
                Expediente = dto.Expediente,
                Paciente = dto.Paciente,
                Estatus = "Facturado"
            };
        }
        
        public static Invoice ToModelCompany(this InvoiceDto dto)
        {
            if (dto == null) return null;

            return new Invoice
            {
                Id = Guid.NewGuid(),
                Serie = dto.Serie,
                FormaPago = dto.FormaPago,
                MetodoPago = dto.MetodoPago,
                UsoCFDI = dto.UsoCFDI,
                RegimenFiscal = dto.Cliente.RegimenFiscal,
                RFC = dto.Cliente.RFC,
                Desglozado = dto.Desglozado,
                ConNombre = dto.ConNombre,
                EnvioCorreo = dto.EnvioCorreo,
                EnvioWhatsapp = dto.EnvioWhatsapp,
                Estatus = "Facturado",
                Solicitudes = dto.SolicitudesId.Select(x => new InvoiceCompanyRequests
                {
                    Id = Guid.NewGuid(),
                    SolicitudId = x
                }).ToList(),
            };
        }
    }
}
