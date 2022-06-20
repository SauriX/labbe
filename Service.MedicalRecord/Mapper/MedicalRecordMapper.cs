using Service.MedicalRecord.Dtos.MedicalRecords;
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

            return  new MedicalRecordsListDto
            {
                Id = x.Id,
                Expediente = x.Expediente,
                NomprePaciente = $"{x.NombrePaciente} {x.PrimerApellido}",
                Genero = x.Genero,
                Edad = x.Edad,
                FechaNacimiento = x.FechaDeNacimiento,
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
                FechaNacimiento = x.FechaDeNacimiento,
                MonederoElectronico = x.Monedero,
                Telefono = x.Telefono,
            }).ToList();
        }
        public static MedicalRecordsFormDto ToMedicalRecordsFormDto(this MedicalRecord.Domain.MedicalRecord.MedicalRecord model)
        {
            if (model == null) return null;

            return new MedicalRecordsFormDto
            {
                Id = model.Id.ToString(),
                Nombre = model.NombrePaciente,
                Apellido = model.PrimerApellido,
                Expediente = model.Expediente,
                Sexo = model.Genero,
                FechaNacimiento = model.FechaDeNacimiento,
                Edad = model.Edad,
                Telefono = model.Telefono,
                Correo = model.Correo,
                //Cp = model.CodigoPostal.ToString(),
                //Estado = model.EstadoId.ToString(),
                //Municipio = model.CiudadId.ToString(),
                Celular = model.Celular.ToString(),
                Calle = model.Calle,
                //Colonia = model.ColoniaId
            };
        }
        public static MedicalRecord.Domain.MedicalRecord.MedicalRecord ToModel(this MedicalRecordsFormDto model)
        {
            if (model == null) return null;

            return new MedicalRecord.Domain.MedicalRecord.MedicalRecord
            {
                Expediente = model.Expediente,
                NombrePaciente = model.Nombre,
                PrimerApellido = model.Apellido,
                SegundoApellido ="" ,
                Genero = model.Sexo,
                FechaDeNacimiento = model.FechaNacimiento,
                Edad = model.Edad,
                Telefono = model.Telefono,
                Correo = model.Correo,
             /*   CodigoPostal = int.Parse(model.Cp),
                EstadoId = int.Parse(model.Estado),
                CiudadId = int.Parse(model.Municipio),
                Celular = int.Parse(model.Celular),*/
                Calle = model.Calle,
               // ColoniaId = int.Parse(model.Colonia),
                UsuarioCreoId = model.UserId,
                FechaCreo = DateTime.Now,
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
               // CodigoPostal = int.Parse(dto.Cp),
               // EstadoId = int.Parse(dto.Estado),
               // CiudadId = int.Parse(dto.Municipio),
                Celular = int.Parse(dto.Celular),
                Calle = model.Calle,
               // ColoniaId = dto.Colonia,
                UsuarioModId = dto.UserId,
                FechaMod = DateTime.Now
            };
        }
    }
}
