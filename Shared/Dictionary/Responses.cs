using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dictionary
{
    public class Responses
    {
        public const string NotFound = "No se encontró el registro";
        public const string Forbidden = "No tiene permisos para realizar esta acción";
        public const string NotPossible = "No es posible realizar esta acción";
        public static string Duplicated(string name) => $"{name} ya se encuentra asignado(a) a un registro, favor de ingresar otro valor";
        public const string Unregistered = "El usuario no se encuentra registrado";
        public const string Blocked = "El usuario se encuentra bloqueado, contacte al administrador";
        public const string Disabled = "El usuario no se encuentra activo, contacte al administrador";
        public const string WrongPassword = "Contraseña incorrecta";
        public const string AttemptsFailed = "Se excedió el número de intentos la cuenta ha sido bloqueada, contacte al Administrador";
        public const string EmailFailed = "Hubo un error al enviar el correo. Revise la configuración";
        public const string InvalidUser = "El usuario no es válido";
        public const string InvalidLink = "El enlace no es válido";
        public const string ConfigurePassword = "Configure su contraseña";
        public const string PasswordExpired = "La contraseña ha expirado, configurela de nuevo";
        public const string ConfigureEmail = "Configure su correo";
        public const string InvalidGuid = "El Id no es válido";
    }
}
