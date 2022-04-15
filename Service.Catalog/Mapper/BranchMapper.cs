using Service.Catalog.Domain;
using Service.Catalog.Domain.Branch;
using Service.Catalog.Dtos.Branch;
using Service.Catalog.Dtos.Study;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Mapper
{
    public static class BranchMapper
    {
        public static IEnumerable<BranchInfo> ToBranchListDto(this List<Branch> model)
        {
            if (model == null) return null;
            return model.Select(x => new BranchInfo
            {
                idSucursal= x.Id.ToString(),
                clave = x.Clave,
                nombre = x.Nombre,
                correo = x.Correo ,
                telefono = (long)x.Telefono,
                ubicacion = $"{x.Calle} {x.NumeroExterior} {x.Ciudad}",
                clinico = "test",
                activo = x.Activo,
                codigoPostal = x.Codigopostal
             });
        }
        public static Branch ToModel(this BranchForm dto)
        {
            if (dto == null) return null;

            return new Branch
            {
                Clave = dto.clave.Trim(),
                Nombre = dto.nombre.Trim(),
                Activo = dto.activo,
                Calle = dto.calle.Trim(),
                ClinicosId = Guid.NewGuid(),
                ColoniaId = dto.coloniaId,
                Correo = dto.correo,
                FechaCreo= DateTime.Now,
                FacturaciónId = Guid.NewGuid(),
                FechaModifico= DateTime.Now,
                Id=Guid.NewGuid(),
                NumeroInterior= dto.numeroInt.ToString(),
                NumeroExterior=dto.numeroExt.ToString(),
                PresupuestosId= Guid.NewGuid(),
                Telefono=dto.telefono,
                UsuarioCreoId= Guid.NewGuid(),
                UsuarioModificoId= Guid.NewGuid(),
                Ciudad = dto.ciudad,               
                Estado = dto.estado,
                Codigopostal = dto.codigoPostal
        /*Estudios = dto.Estudios.Select(x => new IndicationStudy
        {
            EstudioId = x.Id,
            FechaCreo = DateTime.Now,
            UsuarioCreoId = dto.UsuarioId,
            FechaMod = DateTime.Now,
        }).ToList(),*/
    };
        }

        public static BranchForm ToBranchFormDto(this Branch model, IEnumerable<StudyListDto> study)
        {
            if (model == null) return null;

            return new BranchForm
            {
                idSucursal = model.Id.ToString(),
                activo = model.Activo,
                calle = model.Calle,
                clave = model.Clave,
                ciudad=model.Ciudad,
                clinicosId= "test",
                codigoPostal=model.Codigopostal,
                coloniaId=model.ColoniaId,
                correo= model.Correo,
                estado = model.Estado,
                estudios=study,
                facturaciónId="test",
                nombre = model.Nombre,
                numeroExt= int.Parse(model.NumeroExterior),
                numeroInt= int.Parse(model.NumeroInterior),
                presupuestosId="test",
                telefono= (long)model.Telefono

            };
        }

        public static Branch ToModel(this BranchForm dto,Branch model)
        {
            if (dto == null) return null;

            return new Branch
            {
                Clave = model.Clave,
                Nombre = dto.nombre.Trim(),
                Activo = dto.activo,
                Calle = dto.calle.Trim(),
                ClinicosId = model.ClinicosId,
                ColoniaId = dto.coloniaId,
                Correo = dto.correo,
                FechaCreo = model.FechaCreo,
                FacturaciónId = model.FacturaciónId,
                FechaModifico = model.FechaModifico,
                Id = Guid.Parse(dto.idSucursal),
                NumeroInterior = dto.numeroInt.ToString(),
                NumeroExterior = dto.numeroExt.ToString(),
                PresupuestosId = model.PresupuestosId,
                Telefono = dto.telefono,
                UsuarioCreoId = model.UsuarioCreoId,
                UsuarioModificoId = model.UsuarioModificoId,
                Ciudad = dto.ciudad,
                Estado = dto.estado,
                Codigopostal = dto.codigoPostal
                /*Estudios = dto.Estudios.Select(x => new IndicationStudy
                {
                    EstudioId = x.Id,
                    FechaCreo = DateTime.Now,
                    UsuarioCreoId = dto.UsuarioId,
                    FechaMod = DateTime.Now,
                }).ToList(),*/
            };
        }
    }
}
