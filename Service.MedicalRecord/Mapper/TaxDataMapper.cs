using Service.MedicalRecord.Domain.TaxData;
using Service.MedicalRecord.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace Service.MedicalRecord.Mapper
{
    public static class TaxDataMapper
    {
        public static List<TaxData> ToModel(this List<TaxDataDto> model)
        {
            if (model == null) return null;

            return model.Select(x => new TaxData
            {
                Id  = x.Id,
                RFC  = x.Rfc,
                RazonSocial  = x.RazonSocial,
                CodigoPostal  = x.Cp,
                Estado  = x.Estado,
                Ciudad  = x.Municipio,
                Calle  = x.Calle,
                ColoniaId  = x.colonia,
                Correo  = x.Correo,
                Activo  = true,
                UsuarioCreoId  = System.Guid.Empty,
                FechaCreo  = System.DateTime.Now,
                UsuarioModId  = System.Guid.Empty,
                FechaMod  = System.DateTime.Now,
             }).ToList();
        }
    }
}
