using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Identity.Context;
using Service.Identity.Domain.Users;
using Service.Identity.Dtos;
using Service.Identity.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Service.Identity.Mapper;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Security.Principal;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.Dictionary;
using ClosedXML.Report;
using Shared.Extensions;
using ClosedXML.Excel;
using Service.Identity.Domain.permissions;
using System.Linq.Dynamic.Core;

namespace Service.Identity.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly UserManager<UsersModel> _userManager;
        private readonly SignInManager<UsersModel> _signInManager;
        private readonly IndentityContext _context;
        private readonly IConfiguration _configuration;
        readonly ITokenAcquisition _tokenAcquisition;

        public List<UsersModel> ApUsers { get; private set; }
        public UsersModel ApUser { get; private set; }

        public UserRepository(
                    UserManager<UsersModel> userManager,
                    SignInManager<UsersModel> signInManager,
                    IndentityContext indentityContext,
                    IConfiguration configuration)
                    
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = indentityContext;
            _configuration = configuration;
        }

        public async Task<List<UserList>> GetAll(string search)
        {
            var users = _context.Users.AsQueryable();



            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                search = search.Trim().ToLower();
                users = users.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }
            else {
                users = users.Where(x => x.Activo);
            }
             var listuser =Mapper.UserMapper.ToUserListDto(users);
            return (List<UserList>)listuser;
        }
        public async Task<UserList> NewUser(RegisterUserDTO user,string token) {
            var Menus = await getMenus();
            token = token.Replace("Bearer ",string.Empty);
            var usermodel = Mapper.UserMapper.ToregisterUSerDto(user,token);
            usermodel.TwoFactorEnabled = false;
            IdentityResult results= await _userManager.CreateAsync(usermodel,user.Contraseña);
                if (results.Succeeded) {
                ApUsers = _userManager.Users.ToList();
                ApUser = await _userManager.FindByIdAsync(usermodel.Id.ToString());
                if (ApUser != null)
                {
                    var permisos = Mapper.UserPermissionMapper.toPermission(user, ApUser.Id, token, Menus);
                    foreach (Permission permiso in permisos)
                    {
                        await _context.CAT_Permisos.AddAsync(permiso);
                        await _context.SaveChangesAsync();
                    }
                    return Mapper.UserMapper.ToUserInfoDto(ApUser);
                }
            }
            return null;
        }
        public async Task DeleteUser(UsersModel user)
        {
            var logins = await _userManager.GetLoginsAsync(user);
            var rolesForUser = await _userManager.GetRolesAsync(user);
            using (var transaction = _context.Database.BeginTransaction())
            {
                IdentityResult result = IdentityResult.Success;
                foreach (var login in logins)
                {
                    result = await _userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
                    if (result != IdentityResult.Success)
                        break;
                }
                if (result == IdentityResult.Success)
                {
                    foreach (var item in rolesForUser)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, item);
                        if (result != IdentityResult.Success)
                            break;
                    }
                }
                if (result == IdentityResult.Success)
                {
                    result = await _userManager.DeleteAsync(user);
                    if (result == IdentityResult.Success)
                        transaction.Commit();
                }
            }
        }
        public async Task<UserList> GetById(string id) {
            var Menus = await getMenus();
            var permisoslist = await GetPermission();
            ApUsers = _userManager.Users.ToList();
            ApUser = await _userManager.FindByIdAsync(id);
            if (ApUser != null)
            {
                IdentityResult result = await _userManager.UpdateAsync(ApUser);
                if (result.Succeeded)
                {
                    var permisos = _context.CAT_Permisos.AsQueryable();
                    var permiso = permisos.Where(x => x.UsuarioId == ApUser.Id);
                    var permisoM = Mapper.UserPermissionMapper.toListPermision(permiso, Menus, permisoslist);

                    return Mapper.UserMapper.ToUserInfoDto(ApUser,permisoM);
                }
            }
            return null;
        }
        public async Task<UserList> UpdateUser(RegisterUserDTO user,string token) {
            token = token.Replace("Bearer ", string.Empty);
            var Menus = await getMenus();
            string id = user.idUsuario;
            ApUsers = _userManager.Users.ToList();
            
            ApUser = await _userManager.FindByIdAsync(id);
            
            if (ApUser != null)
            {
                
                var ApUsers = Mapper.UserMapper.ToupdateUSerDto(user,token);
                ApUser.Activo = ApUsers.Activo;
                ApUser.Clave = ApUsers.Clave;
                ApUser.FechaMod = DateTime.Now;
                ApUser.Nombre = ApUsers.Nombre;
                ApUser.PrimerApellido = ApUsers.PrimerApellido;
                ApUser.SegundoApellido = ApUsers.SegundoApellido;
                ApUser.UserName = ApUsers.UserName;
                ApUser.UsuarioModId = ApUsers.UsuarioModId;
                ApUser.IdRol = ApUsers.IdRol;
                ApUser.IdSucursal = ApUsers.IdSucursal;
                IdentityResult result = await _userManager.UpdateAsync(ApUser);
                if (result.Succeeded) {

                    var permisions = _context.CAT_Permisos.ToList();
                    var permisios = permisions.Where(x => x.UsuarioId == Guid.Parse(user.idUsuario));
                    foreach (var permiso in permisios)
                    {


                        _context.CAT_Permisos.Remove(permiso);
                        _context.SaveChanges();


                    }
                    var nuevosPermisos = Mapper.UserPermissionMapper.toPermission(user, Guid.Parse(user.idUsuario), token, Menus);
                    foreach (Permission permiso in nuevosPermisos)
                    {
                        await _context.CAT_Permisos.AddAsync(permiso);
                        await _context.SaveChangesAsync();
                    }
                }
                var AppUsers = Mapper.UserMapper.ToUserInfoDto(ApUsers);
                return AppUsers;
            }
            return null;
        }
        public async Task<string> generatePassword()     {
            return GenerarPassword(8);
        }
        public async Task<string> generateClave(clave data) {
            StringBuilder clave = new StringBuilder();
            clave.Append(data.nombre.Substring(0, 1));
            clave.Append(data.primerApllido);
            var result = await _userManager.FindByNameAsync(clave.ToString());
            if (result is null)
            {
                return clave.ToString();
            }
            else {
                clave.Clear();
                clave.Append(data.nombre.Substring(0, 1));
                clave.Append(data.primerApllido.Substring(0, 1));
                clave.Append(data.segundoApellido);
                return clave.ToString().Trim();
            }
        }

        public async Task<UsersModel> AssingRol(string rolId,string userId) {
            ApUsers = _userManager.Users.ToList();
            ApUser = await _userManager.FindByIdAsync(userId);
            if (ApUser != null)
            {
                ApUser.IdRol = Guid.Parse(rolId);
                IdentityResult result = await _userManager.UpdateAsync(ApUser);
                if (result.Succeeded)
                {
                    return ApUser;
                }
            }
            return null;
        }
        public async Task<UsersModel> ChangePassword(ChangePasswordForm form) {
            var id = form.id;
            if (form.id.IsNullOrEmpty())
            {
                string jwt = form.token;
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(jwt);
                var claimValue = securityToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
                 id = claimValue;
            }
 
            if (form.Password.Contains(form.ConfirmPassword))
            {
                if (PasswordValidator(form.Password))
                {
                    var user = await _userManager.FindByIdAsync(id);

                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var result = await _userManager.ResetPasswordAsync(user, token, form.Password);
                    ApUsers = _userManager.Users.ToList();
                    ApUser = await _userManager.FindByIdAsync(id);
                    if (ApUser != null)
                    {
                        ApUser.flagpassword = true;
                        IdentityResult identityResult = await _userManager.UpdateAsync(ApUser);
                        if (identityResult.Succeeded)
                        {
                            ApUsers = _userManager.Users.ToList();
                            ApUser = await _userManager.FindByIdAsync(id);
                            if (ApUser != null)
                            {
                                if (result.Succeeded)
                                {
                                    return ApUser;
                                }
                            }
                            
                        }
                    }
                    return null;
                }
                return null;
            }
            return null;
        }

        public async Task<byte[]> ExportList(string search = null)
        {
            var users = await GetAll(search);

            var path = Assets.UserList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Usuarios");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Usuarios", users);

            template.Generate();

            var range = template.Workbook.Worksheet("Usuarios").Range("Usuarios");
            var table = template.Workbook.Worksheet("Usuarios").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return template.ToByteArray();
        }

        public async Task<byte[]> ExportForm(string id)
        {
            var user = await GetById(id);

            var path = Assets.UserForm;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Usuarios");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Usuario", user);
            template.AddVariable("Permisos", user.permisos);
            template.Generate();

            return template.ToByteArray();
        }
        public async Task<List<Menu>> getUserMenus(string id) {
            id = id.Replace("Bearer ", string.Empty);
            string jwt = id;
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(jwt);
            var claimValue = securityToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
            List<Menu> menuList = new List<Menu>();
            var menus = await getMenus();
            var permisos =_context.CAT_Permisos.Where(x=>x.UsuarioId == Guid.Parse(claimValue));
            foreach (var permiso in permisos) { 
                foreach (var item in menus) {
                    if (item.id == permiso.SubmoduloId) {
                        menuList.Add(item);
                    }
                }
            }
            menuList = menuList.OrderBy(x => x.id).ToList();
            return menuList;
        }
        public async Task<Profile> getProfile(string token) {
            token = token.Replace("Bearer ", string.Empty);
            string jwt = token;
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(jwt);
            var claimValue = securityToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
            ApUsers = _userManager.Users.ToList();
            ApUser = await _userManager.FindByIdAsync(claimValue);
            StringBuilder nombre = new StringBuilder();
            nombre.Append(ApUser.Nombre);
            nombre.Append(" ");
            nombre.Append(ApUser.PrimerApellido);
            nombre.Append(" ");
            nombre.Append(ApUser.SegundoApellido);

            return new Profile
            {
                nombre = nombre.ToString(),
                token = token
            };
        }
        public static string GenerarPassword(int longitud)
        {
            string contraseña = string.Empty;
            string[] letras = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "ñ", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
            Random EleccionAleatoria = new Random();

            for (int i = 0; i < longitud; i++)
            {
                int LetraAleatoria = EleccionAleatoria.Next(0, 100);
                int NumeroAleatorio = EleccionAleatoria.Next(0, 9);

                if (LetraAleatoria < letras.Length)
                {
                    contraseña += letras[LetraAleatoria];
                }
                else
                {
                    contraseña += NumeroAleatorio.ToString();
                }
            }
            return contraseña;
        }
        public static bool PasswordValidator(string pass) {
            Match matchLongitud = Regex.Match(pass, @"^\w{8}\b");
           // Match matchNumeros = Regex.Match(pass, @"\d");
           // Match matchMayusculas = Regex.Match(pass, @"[A-Z]");
           // Match matchAdmin = Regex.Match(pass, @"admin");
           // Match matchContraseña = Regex.Match(pass, @"contraseña");
          // String[] palabrasProhibidas = { "123", "12345", "56789", "123456789", "321", "54321", "987654321", "56789", "qwerty", "asdf", "zxcv", "poiuy", "lkjhg", " mnbv" };
            bool errorFlag = false;
            int errorCode = 0;
            /*if (!matchNumeros.Success)
            {

                errorCode = 1;
                errorFlag = true;
            }*/
            if (errorFlag || !matchLongitud.Success)
            {

                errorCode = 2;
                errorFlag = true;
            }

            switch (errorCode)
            {
                case 1:
                    return false;
                    break;
                case 2:
                    return false;
                    break;
                default:
                    return true;
                    break;
            }
        }
        public async Task<List<Menu>> getMenus()
        {
            List<Menu> menu = new List<Menu>();
            List<Menu> subMenu = new List<Menu>();
            subMenu.Add(new Menu { id = 3, ruta = "users", icono = "user", descripcion = "Usuarios" });
            subMenu.Add(new Menu { id = 4, ruta = "roles", icono = "role", descripcion = "Roles" });
            subMenu.Add(new Menu { id = 7, ruta = "sucursales", icono = "laboratorio", descripcion = "Sucursales" });
            menu.Add(new Menu
            {
                id = 1,
                ruta = "",
                icono = "home",
                descripcion = "Inicio",
            });
            menu.Add(new Menu
            {
                id = 2,
                icono = "admin",
                descripcion = "Administración",
                subMenus = subMenu,
            });
            menu.Add(new Menu
            {
                id = 5,
                ruta = "medics",
                descripcion = "Medicos",
                icono = "medico",
            });
            menu.Add(new Menu
            {
                id = 6,
                ruta = "indication",
                descripcion = "Indicaciones",
                icono = "role",
            });
            menu.Add(new Menu
            {
                id = 8,
                ruta = "parameter",
                descripcion = "Parametros",
                icono = "role",
            });
            menu.Add(new Menu
            {
                id = 9,
                ruta = "company",
                descripcion = "Compañias",
                icono = "role",
            });//Siguiente id libre 10
            return menu;
        }
        public async Task<List<UserPermission>> GetPermission()
        {
            List<UserPermission> list = new List<UserPermission>();
            var Menus = await getMenus();
            var idP = 1;
            foreach (Menu menu in Menus)
            {
                list.Add(new UserPermission
                {
                    id = idP++,
                    permiso = "Crear",
                    asignado = false,
                    menu = menu.descripcion,
                    tipo = menu.id
                });
                list.Add(new UserPermission
                {
                    id = idP++,
                    permiso = "Modificación",
                    asignado = false,
                    menu = menu.descripcion,
                    tipo = menu.id
                });
                list.Add(new UserPermission
                {
                    id = idP++,
                    permiso = "Impresión",
                    asignado = false,
                    menu = menu.descripcion,
                    tipo = menu.id
                });
                list.Add(new UserPermission
                {
                    id = idP++,
                    permiso = "Descarga",
                    asignado = false,
                    menu = menu.descripcion,
                    tipo = menu.id
                });
                list.Add(new UserPermission
                {
                    id = idP++,
                    permiso = "EnvioCorreo",
                    asignado = false,
                    menu = menu.descripcion,
                    tipo = menu.id
                });
                list.Add(new UserPermission
                {
                    id = idP++,
                    permiso = "EnvioWapp",
                    asignado = false,
                    menu = menu.descripcion,
                    tipo = menu.id
                });
                if (menu.subMenus != null)
                {
                    foreach (Menu subMenu in menu.subMenus.ToList<Menu>())
                    {
                        list.Add(new UserPermission
                        {
                            id = idP++,
                            permiso = "Crear",
                            asignado = false,
                            menu = subMenu.descripcion,
                            tipo = subMenu.id
                        });
                        list.Add(new UserPermission
                        {
                            id = idP++,
                            permiso = "Modificación",
                            asignado = false,
                            menu = subMenu.descripcion,
                            tipo = subMenu.id
                        });
                        list.Add(new UserPermission
                        {
                            id = idP++,
                            permiso = "Impresión",
                            asignado = false,
                            menu = subMenu.descripcion,
                            tipo = subMenu.id
                        });
                        list.Add(new UserPermission
                        {
                            id = idP++,
                            permiso = "Descarga",
                            asignado = false,
                            menu = subMenu.descripcion,
                            tipo = subMenu.id
                        });
                        list.Add(new UserPermission
                        {
                            id = idP++,
                            permiso = "EnvioCorreo",
                            asignado = false,
                            menu = subMenu.descripcion,
                            tipo = subMenu.id
                        });
                        list.Add(new UserPermission
                        {
                            id = idP++,
                            permiso = "EnvioWapp",
                            asignado = false,
                            menu = subMenu.descripcion,
                            tipo = subMenu.id
                        });
                    }
                }
            }
            return list;
        }
    }
}
