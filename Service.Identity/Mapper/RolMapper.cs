using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.Domain.UsersRol;
using Service.Identity.Dtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Service.Identity.Mapper
{
    public class RolMapper 
    {
        public static UserRol ToUserRol(RolForm model, string token) {
            string jwt = token;
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(jwt);
            var claimValue = securityToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
            if (model == null) return null;
            return new UserRol {
                Id = new Guid(),
                RolUsuario = model.nombre,
                Activo = model.activo,
                UsuarioCreoId = Guid.Parse(claimValue),
                FechaCreo = DateTime.Now,
                Name = model.nombre
            };
        }
        public static RolForm ToRolForm(UserRol model,List<UserPermission>permisos)
        {
          
            if (model == null) return null;
            return new RolForm
            {
                Id = model.Id.ToString(),
                nombre = model.RolUsuario,
                activo = model.Activo,
                permisos=permisos,
            };
        }
        public static IEnumerable<RolInfo> ToRolListDto(IEnumerable<UserRol> modelList)
        {
            List<RolInfo> roles = new List<RolInfo>();
            if (modelList == null) return null;
            foreach (UserRol userRol in modelList)
            {
                roles.Add(new RolInfo {
                    id = userRol.Id,
                    nombre = userRol.RolUsuario,
                    activo = userRol.Activo,    
                }); ;
            }
            return roles;
        }

        public static RolInfo ToRolInfo(UserRol model)
        {
            if (model == null) return null;
            return new RolInfo
            {
                id = new Guid(),
                nombre = model.RolUsuario,
                activo = model.Activo,
            };
        }
    }
}
