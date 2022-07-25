namespace Service.Identity.Dictionary
{
    public class PermissionProps
    {
        public const string Access = "Acceder";
        public const string Create = "Crear";
        public const string Update = "Modificar";
        public const string Print = "Imprimir";
        public const string Download = "Descargar";
        public const string Mail = "Enviar correo";
        public const string Wapp = "Enviar wapp";

        public const byte AccessType = 1;
        public const byte CreateType = 2;
        public const byte UpdateType = 3;
        public const byte PrintType = 4;
        public const byte DownloadType = 5;
        public const byte MailType = 6;
        public const byte WappType = 7;
    }
}
