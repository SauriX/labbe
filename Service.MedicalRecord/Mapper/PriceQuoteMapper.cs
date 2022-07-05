using Service.MedicalRecord.Domain.PriceQuote;
using Service.MedicalRecord.Dtos.PriceQuote;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.MedicalRecord.Mapper
{
    public static class PriceQuoteMapper
    {
        public static PriceQuoteListDto ToPriceQuoteListDto(this PriceQuote x)
        {
            if (x == null) return null;

            return new PriceQuoteListDto
            {
                Id = x.Id.ToString(),
                 //Presupuesto = x. 
                 NomprePaciente = x.NombrePaciente,
                 //Estudios 
                 Email = x.Correo, 
                 Whatsapp = x.Whatsapp,
                 Fecha  = x.FechaCreo,
                 Expediente = x.Expediente.Expediente,
                 Activo = x.Activo
            };
        }
        public static List<PriceQuoteListDto> ToPriceQuoteListDto(this List<PriceQuote> model)
        {
            if (model == null) return null;

            return model.Select(x => new PriceQuoteListDto
            {
                Id = x.Id.ToString(),
                //Presupuesto = x. 
                NomprePaciente = x.NombrePaciente,
                //Estudios 
                Email = x.Correo,
                Whatsapp = x.Whatsapp,
                Fecha = x.FechaCreo,
                Expediente = x.Expediente.Expediente,
                Activo = x.Activo
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
                Genero = "",    
                //edad = model.ed,
                fechaNacimiento = model.FechaNac,
                generales = new PriceQuoteGeneralDto {
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
            };
        }

        public static PriceQuote ToModel(this PriceQuoteFormDto priceQuoteForm) {

            var priceQuote = new PriceQuote {
                Procedencia = priceQuoteForm.generales.Procedencia,
                NombrePaciente = priceQuoteForm.nomprePaciente,
                FechaNac = priceQuoteForm.fechaNacimiento,
                CompaniaId = Guid.Parse(priceQuoteForm.generales.Compañia),
                MedicoId = Guid.Parse(priceQuoteForm.generales.Medico),
                Afiliacion = "",
                Correo = priceQuoteForm.generales.Email,
                Whatsapp = priceQuoteForm.generales.Whatssap,
                Observaciones = priceQuoteForm.generales.Observaciones,
                FechaPropuesta = DateTime.Now,
                Activo = priceQuoteForm.generales.Activo,
                Estatus = "",
                ExpedienteId = Guid.Parse(priceQuoteForm.expedienteid),
                FechaCreo = DateTime.Now,

                    

            };

            return priceQuote;
        }
        public static PriceQuote ToModel(this PriceQuoteFormDto priceQuoteForm, PriceQuote model)
        {

            var priceQuote = new PriceQuote
            {
                Id= model.Id,
                Procedencia = priceQuoteForm.generales.Procedencia,
                NombrePaciente = priceQuoteForm.nomprePaciente,
                FechaNac = priceQuoteForm.fechaNacimiento,
                CompaniaId = Guid.Parse(priceQuoteForm.generales.Compañia),
                MedicoId = Guid.Parse(priceQuoteForm.generales.Medico),
                Afiliacion = "",
                Correo = priceQuoteForm.generales.Email,
                Whatsapp = priceQuoteForm.generales.Whatssap,
                Observaciones = priceQuoteForm.generales.Observaciones,
                FechaPropuesta = DateTime.Now,
                Activo = priceQuoteForm.generales.Activo,
                Estatus = "",
                ExpedienteId = Guid.Parse(priceQuoteForm.expedienteid)
            };

            return priceQuote;
        }
    }
}
