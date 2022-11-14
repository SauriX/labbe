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
        public static IEnumerable<PriceQuoteInfoDto> ToPriceQuoteInfoDto(this IEnumerable<PriceQuote> model)
        {
            if (model == null) return new List<PriceQuoteInfoDto>();

            return model.Select(x => new PriceQuoteInfoDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Expediente = x.Expediente?.Expediente,
                Paciente = x.Expediente?.NombreCompleto,
                Correo = x.EnvioCorreo,
                Whatsapp = x.EnvioWhatsapp,
                Fecha = x.FechaCreo,
                Activo = x.Activo,
                Estudios = x.Estudios?.Select(s => new PriceQuoteStudyInfoDto
                {
                    Id = s.Id,
                    EstudioId = s.EstudioId,
                    Clave = s.Clave,
                    Nombre = s.Nombre
                }) ?? new List<PriceQuoteStudyInfoDto>()
            });
        }

        public static PriceQuoteDto ToPriceQuoteDto(this PriceQuote model)
        {
            if (model == null) return null;

            return new PriceQuoteDto();
        }

        public static PriceQuoteGeneralDto ToPriceQuoteGeneralDto(this PriceQuote model)
        {
            if (model == null) return null;

            return new PriceQuoteGeneralDto();
        }

        public static PriceQuote ToModel(this PriceQuoteDto model)
        {
            if (model == null) return null;

            return new PriceQuote();
        }
    }
}