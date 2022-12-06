using Service.MedicalRecord.Domain.TaxData;
using Service.MedicalRecord.Dtos;
using Service.MedicalRecord.Dtos.MedicalRecords;
using Service.MedicalRecord.Dtos.Reports;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.MedicalRecord.Mapper
{
    public static class MedicalRecordMapper
    {
        public static MedicalRecordsListDto ToMedicalRecordsListDto(this MedicalRecord.Domain.MedicalRecord.MedicalRecord x)
        {
            if (x == null) return null;

            return new MedicalRecordsListDto
            {
                Id = x.Id,
                Expediente = x.Expediente,
                NomprePaciente = $"{x.NombrePaciente} {x.PrimerApellido}",
                Genero = x.Genero,
                Edad = x.Edad,
                FechaNacimiento = x.FechaDeNacimiento.Date.ToShortDateString().ToString(),
                MonederoElectronico = x.Monedero,
                Telefono = x.Telefono,
            };
        }

        public static List<MedicalRecordsListDto> ToMedicalRecordsListDto(this List<MedicalRecord.Domain.MedicalRecord.MedicalRecord> model)
        {
            if (model == null) return null;

            return model.Select(x => new MedicalRecordsListDto
            {
                Id = x.Id,
                Expediente = x.Expediente,
                NomprePaciente = $"{x.NombrePaciente} {x.PrimerApellido}",
                Genero = x.Genero,
                Edad = x.Edad,
                FechaNacimiento = x.FechaDeNacimiento.Date.ToString("dd/MM/yyyy"),
                MonederoElectronico = x.Monedero,
                Telefono = x.Telefono,
            }).ToList();
        }

        public static MedicalRecordsFormDto ToMedicalRecordsFormDto(this MedicalRecord.Domain.MedicalRecord.MedicalRecord model)
        {
            if (model == null) return null;
            var taxdata = model.TaxData;
            IEnumerable<TaxDataDto> data = null;
            if (taxdata.Count() > 0)
            {
                data = model.TaxData?.Select(x => x.Factura)?.OrderBy(x=> x.FechaCreo)?.ToTaxDataDto();
            }
            return new MedicalRecordsFormDto
            {
                Id = model.Id.ToString(),
                Nombre = model.NombrePaciente,
                Apellido = model.PrimerApellido,
                Expediente = model.Expediente,
                Sexo = model.Genero,
                FechaNacimiento = model.FechaDeNacimiento,
                FechaNacimientoFormat = model.FechaDeNacimiento.ToString("MM/dd/yyyy"),
                Edad = model.Edad,
                Telefono = model.Telefono,
                Correo = model.Correo,
                Cp = model.CodigoPostal,
                Estado = model.Estado,
                Municipio = model.Ciudad,
                Celular = model.Celular,
                Calle = model.Calle,
                Colonia = model.ColoniaId,
                TaxData = data == null ? Enumerable.Empty<TaxDataDto>(): data,
                sucursal = model.IdSucursal.ToString(),
                HasWallet = model.MonederoActivo,
                Wallet = model.Monedero,
                FechaActivacionMonedero = model.FechaActivacionMonedero
            };
        }

        public static List<TaxDataDto> ToTaxDataDto(this IEnumerable<TaxData> model)
        {
            if (model == null) return null;

            return model.Select(x => new TaxDataDto
            {
                Id = x.Id,
                Rfc = x.RFC,
                RazonSocial = x.RazonSocial,
                RegimenFiscal = x.RegimenFiscal,
                Cp = x.CodigoPostal,
                Estado = x.Estado,
                Municipio = x.Ciudad,
                Calle = x.Calle,
                Colonia = x.ColoniaId,
                Correo = x.Correo,
            }).ToList();
        }

        public static TaxData ToTaxData(this TaxDataDto dto)
        {
            if (dto == null) return null;

            return new TaxData
            {
                Id = Guid.NewGuid(),
                RFC = dto.Rfc,
                RazonSocial = dto.RazonSocial,
                RegimenFiscal = dto.RegimenFiscal,
                CodigoPostal = dto.Cp,
                Estado = dto.Estado,
                Ciudad = dto.Municipio,
                Calle = dto.Calle,
                ColoniaId = dto.Colonia,
                Correo = dto.Correo,
                Activo = true,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
            };
        }

        public static List<TaxData> ToTaxData(this IEnumerable<TaxDataDto> model)
        {
            if (model == null) return null;

            return model.Select(x => new TaxData
            {
                Id = Guid.NewGuid(),
                RFC = x.Rfc,
                RazonSocial = x.RazonSocial,
                RegimenFiscal = x.RegimenFiscal,
                CodigoPostal = x.Cp,
                Estado = x.Estado,
                Ciudad = x.Municipio,
                Calle = x.Calle,
                ColoniaId = x.Colonia,
                Correo = x.Correo,
                Activo = true,
                UsuarioCreoId = System.Guid.Empty,
                FechaCreo = System.DateTime.Now,
                UsuarioModId = System.Guid.Empty,
                FechaMod = System.DateTime.Now,
            }).ToList();
        }

        public static TaxData ToTaxDataUpdate(this TaxDataDto dto, TaxData model)
        {
            if (dto == null) return null;

            return new TaxData
            {
                Id = model.Id,
                RFC = dto.Rfc,
                RazonSocial = dto.RazonSocial,
                RegimenFiscal = dto.RegimenFiscal,
                CodigoPostal = dto.Cp,
                Estado = dto.Estado,
                Ciudad = dto.Municipio,
                Calle = dto.Calle,
                ColoniaId = dto.Colonia,
                Correo = dto.Correo,
                Activo = true,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModId = dto.UsuarioId,
                FechaMod = DateTime.Now
            };
        }

        public static List<TaxData> ToTaxDataUpdate(this IEnumerable<TaxDataDto> model)
        {
            if (model == null) return null;

            return model.Select(x => new TaxData
            {
                Id = (Guid)x.Id,
                RFC = x.Rfc,
                RazonSocial = x.RazonSocial,
                CodigoPostal = x.Cp,
                Estado = x.Estado,
                Ciudad = x.Municipio,
                Calle = x.Calle,
                ColoniaId = x.Colonia,
                Correo = x.Correo,
                Activo = true,
                UsuarioCreoId = System.Guid.Empty,
                FechaCreo = System.DateTime.Now,
                UsuarioModId = System.Guid.Empty,
                FechaMod = System.DateTime.Now,
            }).ToList();
        }

        public static List<Domain.MedicalRecord.MedicalRecordTaxData> ToTaxDataMedicalRecord(this IEnumerable<TaxData> model)
        {
            if (model == null) return null;

            return model.Select(x => new Domain.MedicalRecord.MedicalRecordTaxData
            {
                FacturaID = x.Id,
                Activo = true,
                UsuarioCreoId = System.Guid.Empty,
                FechaCreo = System.DateTime.Now,
                UsuarioModId = System.Guid.Empty,
                FechaMod = System.DateTime.Now,
            }).ToList();
        }

        public static MedicalRecord.Domain.MedicalRecord.MedicalRecord ToModel(this MedicalRecordsFormDto model)
        {
            if (model == null) return null;

            return new MedicalRecord.Domain.MedicalRecord.MedicalRecord
            {
                Expediente = model.Expediente,
                NombrePaciente = model.Nombre,
                PrimerApellido = model.Apellido,
                SegundoApellido = "",
                Genero = model.Sexo,
                FechaDeNacimiento = model.FechaNacimiento,
                Edad = model.Edad,
                Telefono = model.Telefono,
                Correo = model.Correo,
                CodigoPostal = model.Cp,
                Estado = model.Estado,
                Ciudad = model.Municipio,
                Celular = model.Celular,
                Calle = model.Calle,
                ColoniaId = model.Colonia,
                UsuarioCreoId = model.UsuarioId,
                FechaCreo = DateTime.Now,
                IdSucursal = Guid.Parse(model.sucursal)
            };
        }


        public static MedicalRecord.Domain.MedicalRecord.MedicalRecord ToModel(this MedicalRecordsFormDto dto, MedicalRecord.Domain.MedicalRecord.MedicalRecord model)
        {
            if (model == null || dto == null) return null;

            return new MedicalRecord.Domain.MedicalRecord.MedicalRecord
            {
                Id = model.Id,
                Expediente = model.Expediente,
                NombrePaciente = dto.Nombre,
                PrimerApellido = dto.Apellido,
                SegundoApellido = "",
                Genero = dto.Sexo,
                FechaDeNacimiento = dto.FechaNacimiento,
                Edad = dto.Edad,
                Telefono = dto.Telefono,
                Correo = dto.Correo,
                CodigoPostal = dto.Cp,
                Estado = dto.Estado,
                Ciudad = dto.Municipio,
                Celular = dto.Celular,
                Calle = dto.Calle,
                ColoniaId = dto.Colonia,
                UsuarioModId = dto.UsuarioId,
                FechaMod = DateTime.Now,
                FechaCreo = model.FechaCreo,
                IdSucursal = Guid.Parse(dto.sucursal)
            };
        }

        public static List<MedicalRecordDto> ToMedicalRecordDto(this List<Domain.MedicalRecord.MedicalRecord> model)
        {
            if (model == null) return null;

            return model.Select(x => new MedicalRecordDto
            {
                Id = x.Id,
                Expediente = x.Expediente,
                Nombre = x.NombreCompleto,
                Edad = x.Edad,
                Sexo = x.Genero,
                Celular = x.Celular,
                Correo = x.Correo,
            }).ToList();
        }
    }
}
