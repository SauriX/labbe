using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Integration.WeeClinic
{
    public class Base
    {
        public static async Task<string> Login()
        {
            try
            {
                var baseUrl = "http://weeqp.azurewebsites.net/QP/WeeClinic";

                var url = $"{baseUrl}/API/api/Sesion/Login";

                var data = new Dictionary<string, string>()
                {
                    ["username"] = "mfarias@axsistec.com",
                    ["password"] = "lab.ramo$",
                    ["grant_type"] = "password",
                };

                using HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
                client.DefaultRequestHeaders.Add("TokenClienteCert", "693CADADEA9E51F02CCE915C6541FF99CDBE4BE494E7C5E86F3FC257CC76D851");

                var response = await client.PostAsync(url, new FormUrlEncodedContent(data));

                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
            catch (Exception e)
            {
                return e.ToString();
            }

        }
    }
}

//Les compartimos las credenciales solicitadas para la realización de pruebas.

//Laboratorio Ramos QA
//Usuario	mfarias@axsistec.com

//Contraseña	lab.ramo$
//ClienteTokenCert	693CADADEA9E51F02CCE915C6541FF99CDBE4BE494E7C5E86F3FC257CC76D851

//Adicionalmente compartimos unos Folios para llevar a cabo pruebas de los WS.

//AD00704422003227421X
//AD0070442200322751R4
//AD007044220032276WRT

//Como recordatorio, nuestra documentación técnica se encuentra en esta liga: weewiki.azurewebsites.net /

//Quedamos atentos a sus comentarios. 

//¡Saludos!
