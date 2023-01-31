using Service.Catalog.Domain.Configuration;
using Service.Catalog.Dtos.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Mapper
{
    public static class TaxConfigurationMapper
    {
        public static TaxConfiguration ToModelCreate(this ConfigurationFiscalDto dto)
        {
            if (dto == null) return null;

            return new TaxConfiguration
            {
                Id = Guid.NewGuid(),
                RFC = dto.RFC,
                RazonSocial = dto.RazonSocial,
                Telefono = dto.Telefono,
                CodigoPostal = dto.CodigoPostal,
                Pais = "México",
                Calle = dto.Calle,
                Colonia = dto.Colonia,
                Municipio = dto.Ciudad,
                NoInterior = dto.Numero,
                Estado = dto.Estado,
                UsuarioId = dto.UsuarioId
            };
        }

        public static TaxConfiguration ToModelUpdate(this ConfigurationFiscalDto dto, TaxConfiguration model)
        {
            if (dto == null) return null;

            return new TaxConfiguration
            {
                Id = model.Id,
                RFC = dto.RFC,
                RazonSocial = dto.RazonSocial,
                Telefono = dto.Telefono,
                CodigoPostal = dto.CodigoPostal,
                Calle = dto.Calle,
                Colonia = dto.Colonia,
                Municipio = dto.Ciudad,
                NoInterior = dto.Numero,
                Estado = dto.Estado,
                UsuarioId = model.UsuarioId,
                SucursalId = model.SucursalId
            };
        }
    }
}
