using Service.Identity.Domain.Branch;
using Service.Identity.Dtos;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Service.Identity.Mapper
{
    public class BranchMapper
    {
        public static Branch toBranch(BranchForm branchForm,string token) {
            string jwt = token;
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(jwt);
            var claimValue = securityToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
            if (branchForm == null) return null;

            return new Branch {
                Activo = branchForm.Activo,
                Calle = branchForm.Calle,
                CiudadId = branchForm.CiudadId,
                Clave = branchForm.Clave,
                ClinicosId = new Guid(),
                CodigoPostal = branchForm.CodigoPostal,
                ColoniaId = branchForm.ColoniaId,
                Correo = branchForm.Correo,
                FechaCreo = DateTime.Now,
                FacturaciónId = new Guid(),
                EstadoId = branchForm.EstadoId,
                NumeroExterior = branchForm.NumeroExterior,
                IdSucursal = new Guid(),
                Nombre = branchForm.Nombre,
                NumeroInterior = branchForm.NumeroInterior,
                PresupuestosId = new Guid(),
                ServicioId = new Guid(),
                Telefono = branchForm.Telefono,
            };

        }

        public static BranchForm toBranchForm(Branch branchForm, string token)
        {
            string jwt = token;
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(jwt);
            var claimValue = securityToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
            if (branchForm == null) return null;

            return new BranchForm
            {
                Activo = branchForm.Activo,
                Calle = branchForm.Calle,
                CiudadId = branchForm.CiudadId,
                Clave = branchForm.Clave,
                //ClinicosId = new Guid(),
                CodigoPostal = branchForm.CodigoPostal,
                ColoniaId = branchForm.ColoniaId,
                Correo = branchForm.Correo,
                //FechaCreo = DateTime.Now,
                //FacturaciónId = new Guid(),
                EstadoId = branchForm.EstadoId,
                NumeroExterior = branchForm.NumeroExterior,
                IdSucursal = new Guid(),
                Nombre = branchForm.Nombre,
                NumeroInterior = branchForm.NumeroInterior,
                //PresupuestosId = new Guid(),
                //ServicioId = new Guid(),
                Telefono = branchForm.Telefono,
            };

        }
    }
}
