namespace Shared.Dictionary
{
    public class Responses
    {
        public const string NotFound = "No se encontró el registro";
        public const string Forbidden = "No tiene permisos para realizar esta acción";
        public const string NotPossible = "No es posible realizar esta acción";
        public const string Unregistered = "El usuario no se encuentra registrado";
        public const string Blocked = "El usuario se encuentra bloqueado, contacte al administrador";
        public const string Disabled = "El usuario no se encuentra activo, contacte al administrador";
        public const string WrongPassword = "Contraseña incorrecta";
        public const string AttemptsFailed = "Se excedió el número de intentos la cuenta ha sido bloqueada, contacte al Administrador";
        public const string EmailConfigurationIncomplete = "La configuración para el envio de correos esta incompleta, favor de ingresarla";
        public const string EmailFordwardError = "El correo al que se quiere enviar la información no es válido";
        public const string EmailFailed = "Hubo un error al enviar el correo. Revise la configuración";
        public const string InvalidUser = "El usuario no es válido";
        public const string InvalidLink = "El enlace no es válido";
        public const string ConfigurePassword = "Configure su contraseña";
        public const string PasswordExpired = "La contraseña ha expirado, configurela de nuevo";
        public const string ConfigureEmail = "Configure su correo";
        public const string InvalidGuid = "El Id no es válido";
        public const string InvalidImage = "La imagén no es válida";
        public static string Duplicated(string name) => $"{name} ya se encuentra asignado(a) a un registro, favor de ingresar otro valor";
        public static string TipoDescuento(string name) => $"{name} No puede ser mayor a 100 si el tipo de descuento es por porcentaje, favor de ingresar otro valor";
        public static string DuplicatedDestiny(string name) => $"{name} no puede ser el mismo que la sucursal de origen, favor de ingresar otro valor";
        public static string EmptyDestiny(string name) => $"{name} no puede estar vacio, favor de ingresar un valor";
        public static string DuplicatedDate(string name) => $"{name} ya se encuentra asignado(a) a otra lealtad, favor de ingresar otro valor";
        public static string RabbitMQError(string url, int retry, string contract, string messageId, string message, string excepciones)
        {
            return $"" +
                $"Ha ocurrido un error en: {url}\n" +
                $"Retry: {retry}\n" +
                $"Contract: {contract}\n" +
                $"MessageId: {messageId}\n" +
                $"Message: {message}\n" +
                $"{excepciones}";
        }
    }
}
