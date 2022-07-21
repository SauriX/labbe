using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Catalog
{
    public class EmailConfigurationContract
    {
        public EmailConfigurationContract()
        {

        }

        public EmailConfigurationContract(string correo, string remitente, string smtp, bool requiereContraseña, string contraseña)
        {
            Correo = correo;
            Remitente = remitente;
            Smtp = smtp;
            RequiereContraseña = requiereContraseña;
            Contraseña = contraseña;
        }

        public string Correo { get; set; }
        public string Remitente { get; set; }
        public string Smtp { get; set; }
        public bool RequiereContraseña { get; set; }
        public string Contraseña { get; set; }
    }
}
