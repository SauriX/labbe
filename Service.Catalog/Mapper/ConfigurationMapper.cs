using Service.Catalog.Domain.Configuration;
using Service.Catalog.Dtos.Configuration;
using Service.Catalog.Mapper;
using Service.Catalog.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Catalog.Mapper
{
    public static class ConfigurationMapper
    {
        public const int EmailCorreo = 1;
        public const int EmailRemitente = 2;
        public const int EmailSmtp = 3;
        public const int EmailRequiereContraseña = 4;
        public const int EmailContraseña = 5;

        public const int GeneralNombre = 6;
        public const int GeneralLogo = 7;

        public const int FiscalRFC = 8;
        public const int FiscalRazonSocial = 9;
        public const int FiscalCP = 10;
        public const int FiscalEstado = 11;
        public const int FiscalColonia = 12;
        public const int FiscalCalle = 13;
        public const int FiscalNumero = 14;
        public const int FiscalTelefono = 15;
        public const int FiscalCiudad = 16;

        public static ConfigurationEmailDto ToConfigurationEmailDto(this List<Configuration> model, bool pass = false)
        {
            return new ConfigurationEmailDto
            {
                Correo = model.FirstOrDefault(x => x.Id == EmailCorreo).Valor,
                Remitente = model.FirstOrDefault(x => x.Id == EmailRemitente).Valor,
                Smtp = model.FirstOrDefault(x => x.Id == EmailSmtp).Valor,
                RequiereContraseña = model.FirstOrDefault(x => x.Id == EmailRequiereContraseña).Valor == "1",
                Contraseña = pass ? model.FirstOrDefault(x => x.Id == EmailContraseña).Valor : null,
            };
        }

        public static ConfigurationGeneralDto ToConfigurationGeneralDto(this List<Configuration> model)
        {
            return new ConfigurationGeneralDto
            {
                NombreSistema = model.FirstOrDefault(x => x.Id == GeneralNombre).Valor,
                LogoRuta = "/wwwroot/images/" + model.FirstOrDefault(x => x.Id == GeneralLogo).Valor,
            };
        }

        public static ConfigurationFiscalDto ToConfigurationFiscalDto(this List<Configuration> model)
        {
            return new ConfigurationFiscalDto
            {
                Rfc = model.FirstOrDefault(x => x.Id == FiscalRFC).Valor,
                RazonSocial = model.FirstOrDefault(x => x.Id == FiscalRazonSocial).Valor,
                CodigoPostal = model.FirstOrDefault(x => x.Id == FiscalCP).Valor,
                Estado = model.FirstOrDefault(x => x.Id == FiscalEstado).Valor,
                Ciudad = model.FirstOrDefault(x => x.Id == FiscalCiudad).Valor,
                Colonia = model.FirstOrDefault(x => x.Id == FiscalColonia).Valor,
                Calle = model.FirstOrDefault(x => x.Id == FiscalCalle).Valor,
                Numero = model.FirstOrDefault(x => x.Id == FiscalNumero).Valor,
                Telefono = model.FirstOrDefault(x => x.Id == FiscalTelefono).Valor,
            };
        }

        public static List<Configuration> ToModel(this ConfigurationEmailDto dto, IEnumerable<Configuration> model, string key)
        {
            var configuration = new List<Configuration>
            {
                new Configuration
                {
                    Id = EmailCorreo,
                    Descripcion = model.FirstOrDefault(x => x.Id == EmailCorreo).Descripcion,
                    Valor = dto.Correo.Trim()
                },
                new Configuration
                {
                    Id = EmailRemitente,
                    Descripcion = model.FirstOrDefault(x => x.Id == EmailRemitente).Descripcion,
                    Valor = dto.Remitente.Trim()
                },
                new Configuration
                {
                    Id = EmailSmtp,
                    Descripcion = model.FirstOrDefault(x => x.Id == EmailSmtp).Descripcion,
                    Valor = dto.Smtp.Trim()
                },
                new Configuration
                {
                    Id = EmailRequiereContraseña,
                    Descripcion = model.FirstOrDefault(x => x.Id == EmailRequiereContraseña).Descripcion,
                    Valor = dto.RequiereContraseña ? "1" : "0"
                },
                new Configuration
                {
                    Id = EmailContraseña,
                    Descripcion = model.FirstOrDefault(x => x.Id == EmailContraseña).Descripcion,
                    Valor = !string.IsNullOrWhiteSpace(dto.Contraseña) ?
                    Crypto.EncryptString(dto.Contraseña, key) : model.FirstOrDefault(x => x.Id == EmailContraseña).Valor,
                },
            };

            return configuration;
        }

        public static List<Configuration> ToModel(this ConfigurationGeneralDto dto, IEnumerable<Configuration> model)
        {
            var configuration = new List<Configuration>
            {
                new Configuration
                {
                    Id = GeneralNombre,
                    Descripcion = model.FirstOrDefault(x => x.Id == GeneralNombre).Descripcion,
                    Valor = dto.NombreSistema.Trim()
                },
            };

            return configuration;
        }

        public static List<Configuration> ToModel(this ConfigurationFiscalDto dto, IEnumerable<Configuration> model)
        {
            var configuration = new List<Configuration>
            {
                new Configuration
                {
                    Id = FiscalRFC,
                    Descripcion = model.FirstOrDefault(x => x.Id == FiscalRFC).Descripcion,
                    Valor = dto.Rfc.Trim()
                },
                new Configuration
                {
                    Id = FiscalRazonSocial,
                    Descripcion = model.FirstOrDefault(x => x.Id == FiscalRazonSocial).Descripcion,
                    Valor = dto.RazonSocial.Trim()
                },
                new Configuration
                {
                    Id = FiscalCP,
                    Descripcion = model.FirstOrDefault(x => x.Id == FiscalCP).Descripcion,
                    Valor = dto.CodigoPostal.Trim()
                },
                new Configuration
                {
                    Id = FiscalEstado,
                    Descripcion = model.FirstOrDefault(x => x.Id == FiscalEstado).Descripcion,
                    Valor = dto.Estado.Trim()
                },
                new Configuration
                {
                    Id = FiscalCiudad,
                    Descripcion = model.FirstOrDefault(x => x.Id == FiscalCiudad).Descripcion,
                    Valor = dto.Ciudad.Trim()
                },
                new Configuration
                {
                    Id = FiscalColonia,
                    Descripcion = model.FirstOrDefault(x => x.Id == FiscalColonia).Descripcion,
                    Valor = dto.Colonia.Trim()
                },
                new Configuration
                {
                    Id = FiscalCalle,
                    Descripcion = model.FirstOrDefault(x => x.Id == FiscalCalle).Descripcion,
                    Valor = dto.Calle.Trim()
                },
                new Configuration
                {
                    Id = FiscalNumero,
                    Descripcion = model.FirstOrDefault(x => x.Id == FiscalNumero).Descripcion,
                    Valor = dto.Numero.Trim()
                },
                new Configuration
                {
                    Id = FiscalTelefono,
                    Descripcion = model.FirstOrDefault(x => x.Id == FiscalTelefono).Descripcion,
                    Valor = dto.Telefono?.Trim()
                },
            };

            return configuration;
        }
    }
}
