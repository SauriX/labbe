using Service.MedicalRecord.Domain.PriceQuote;
using Service.MedicalRecord.Dtos.PriceQuote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.MedicalRecord.Mapper
{
    public static class PriceQuoteMapper
    {
        public static PriceQuoteListDto ToPriceQuoteListDto(this PriceQuote x)
        {
            if (x == null) return null;
            StringBuilder sb = new StringBuilder();
            foreach (var estudio in x.Estudios)
            {
                sb.Append(estudio.Clave);
                sb.Append(", ");
            }
            return new PriceQuoteListDto
            {
                Id = x.Id.ToString(),
                Presupuesto = x.Afiliacion,
                NomprePaciente = x.NombrePaciente,
                Estudios = sb.ToString(),
                Email = x.Correo,
                Whatsapp = x.Whatsapp,
                Fecha = x.FechaCreo,
                Expediente = x.Expediente.Expediente,
                Activo = x.Activo
            };
        }
        public static List<PriceQuoteListDto> ToPriceQuoteListDto(this List<PriceQuote> model)
        {
            if (model == null) return null;

            return model.Select(x =>
            {
                StringBuilder sb = new StringBuilder();

                if (x.Estudios != null)
                {
                    if (x.Estudios.Count() > 0)
                    {
                        foreach (var estudio in x.Estudios)
                        {
                            sb.Append(estudio.Clave);
                            sb.Append(", ");
                        }
                    }
                    else
                    {
                        sb.Append("");
                    }
                }
                else
                {
                    sb.Append("");
                }



                return new PriceQuoteListDto
                {
                    Id = x.Id.ToString(),
                    Presupuesto = x.Afiliacion,
                    NomprePaciente = x.NombrePaciente,
                    Estudios = sb.ToString(),
                    Email = x.Correo,
                    Whatsapp = x.Whatsapp,
                    Fecha = x.FechaCreo,
                    Expediente = x.Expediente.Expediente,
                    Activo = x.Activo
                };
            }).ToList();
        }

        public static PriceQuoteFormDto ToPriceQuoteFormDto(this PriceQuote model)
        {
            if (model == null) return null;

            return new PriceQuoteFormDto
            {
                Id = model.Id.ToString(),
                expediente = model.Expediente.Expediente,
                nomprePaciente = model.NombrePaciente,
                Genero = model.Genero,
                edad = model.Edad,
                fechaNacimiento = model.FechaNac,
                expedienteid = model.Expediente.Id.ToString(),
                typo = model.Tipo,
                cargo = model.Cargo,
                generales = new PriceQuoteGeneralDto
                {
                    Procedencia = model.Procedencia,
                    Compañia = model.CompaniaId.ToString(),
                    Medico = model.MedicoId.ToString(),
                    NomprePaciente = model.NombrePaciente,

                    Observaciones = model.Observaciones,
                    TipoEnvio = "",
                    Email = model.Correo,
                    Whatssap = model.Whatsapp,
                    Activo = model.Activo,
                },
                estudy = model.Estudios.Select(x => new QuotetPrice
                {
                    PrecioListaId = x.CotizacionId,
                    ListaPrecioId = x.ListaPrecioId,
                    PromocionId = x.PromocionId,
                    EstudioId = x.EstudioId,
                    PaqueteId = x.PaqueteId,
                    EstatusId = x.EstatusId,
                    AplicaDescuento = x.Descuento,
                    AplicaCargo = x.Cargo,
                    AplicaCopago = x.Copago,
                    Precio = x.Precio,
                    PrecioFinal = x.PrecioFinal,
                    Nombre = x.Clave,

                }).ToList()
            };
        }

        public static PriceQuote ToModel(this PriceQuoteFormDto priceQuoteForm)
        {

            var priceQuote = new PriceQuote
            {
                Procedencia = priceQuoteForm.generales.Procedencia,
                NombrePaciente = priceQuoteForm.nomprePaciente,
                FechaNac = priceQuoteForm.fechaNacimiento,
                CompaniaId = Guid.Parse(priceQuoteForm.generales.Compañia),
                MedicoId = Guid.Parse(priceQuoteForm.generales.Medico),
                Afiliacion = "",
                Tipo = priceQuoteForm.typo,
                Cargo = priceQuoteForm.cargo,
                Correo = priceQuoteForm.generales.Email,
                Whatsapp = priceQuoteForm.generales.Whatssap,
                Observaciones = priceQuoteForm.generales.Observaciones,
                FechaPropuesta = DateTime.Now,
                Activo = priceQuoteForm.generales.Activo,
                Estatus = "",
                ExpedienteId = Guid.Parse(priceQuoteForm.expedienteid),
                FechaCreo = DateTime.Now,
                Genero = priceQuoteForm.Genero,
                Edad = priceQuoteForm.edad,
                Estudios = priceQuoteForm.estudy.Select(x => new CotizacionStudy
                {
                    CotizacionId = x.PrecioListaId,
                    ListaPrecioId = x.ListaPrecioId,
                    PromocionId = x.PromocionId,
                    EstudioId = x.EstudioId,
                    PaqueteId = x.PaqueteId,
                    EstatusId = x.EstatusId,
                    Descuento = x.AplicaDescuento,
                    Cargo = x.AplicaCargo,
                    Copago = x.AplicaCopago,
                    Precio = x.Precio,
                    PrecioFinal = x.PrecioFinal,
                    Clave = x.Nombre
                })

            };

            return priceQuote;
        }
        public static PriceQuote ToModel(this PriceQuoteFormDto priceQuoteForm, PriceQuote model)
        {

            var priceQuote = new PriceQuote
            {
                Id = model.Id,
                Procedencia = priceQuoteForm.generales.Procedencia,
                NombrePaciente = priceQuoteForm.nomprePaciente,
                FechaNac = priceQuoteForm.fechaNacimiento,
                CompaniaId = Guid.Parse(priceQuoteForm.generales.Compañia),
                MedicoId = Guid.Parse(priceQuoteForm.generales.Medico),
                Afiliacion = "",
                Tipo = priceQuoteForm.typo,
                Cargo = priceQuoteForm.cargo,
                Correo = priceQuoteForm.generales.Email,
                Whatsapp = priceQuoteForm.generales.Whatssap,
                Observaciones = priceQuoteForm.generales.Observaciones,
                FechaPropuesta = DateTime.Now,
                Activo = priceQuoteForm.generales.Activo,
                Estatus = "",
                ExpedienteId = Guid.Parse(priceQuoteForm.expedienteid),
                Genero = priceQuoteForm.Genero,
                Edad = priceQuoteForm.edad,
                Estudios = priceQuoteForm.estudy.Select(x => new CotizacionStudy
                {
                    CotizacionId = x.PrecioListaId,
                    ListaPrecioId = x.ListaPrecioId,
                    PromocionId = x.PromocionId,
                    EstudioId = x.EstudioId,
                    PaqueteId = x.PaqueteId,
                    EstatusId = x.EstatusId,
                    Descuento = x.AplicaDescuento,
                    Cargo = x.AplicaCargo,
                    Copago = x.AplicaCopago,
                    Precio = x.Precio,
                    PrecioFinal = x.PrecioFinal,
                    Clave = x.Nombre,
                }),
                FechaCreo = model.FechaCreo,

            };

            return priceQuote;
        }
    }
}
