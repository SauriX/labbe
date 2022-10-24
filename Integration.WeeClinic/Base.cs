using ClosedXML.Excel;
using Integration.WeeClinic.Models;
using Integration.WeeClinic.Responses;
using Shared.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Integration.WeeClinic
{
    public class Base
    {
        protected const string baseUrl = "http://weeqp.azurewebsites.net/QP/WeeClinic";
        protected const string certKey = "693CADADEA9E51F02CCE915C6541FF99CDBE4BE494E7C5E86F3FC257CC76D851";
        protected const string token = "R3XaFcSbKFyBVPmX7Ctj7o17Np6rotqti1PcEjAb9JAtRMiiTnr1PSCY51flr6Xm7i29xg33jh0evqA2H9heNhcraL02OApzhtkmglEsBo1kAuq4Ue9gpTdGbNkfO6EO-SR92corHQcC40D7qZy7P-wGaVA-mSjZpWJVsb9Bkt6vkep0ROHZ9uvOm28FQSeyyVrWOuDzcg6rvAWawonhgcZSxAZKotBTBe1SubaUurIkASFYtWGQ5UP9ok77HibjXpbvE5b5yTfCfe35BzvZcEc4NhMw8yzBYwpZp72cLnG6iyXkntG8bHtG1tlMQt2GJP7PNB-zu7tFa7wgpW48jRM475mtbJoA86IZGgiW2FZqvz6mMtFyv4XliXBwl2Me_Q_Rbm1aSyy3jE-0OP3zW9zipRx6nA3EHGInlZaPMZ3IhHk9F-1FImCL_U6GOAK9";

        protected const string folio1 = "AD00704422003227421X";
        protected const string folio2 = "AD0070442200322751R4";
        protected const string folio3 = "AD007044220032276WRT";

        public static async Task<LoginResponse> Login()
        {
            try
            {
                var url = $"{baseUrl}/API/api/Sesion/Login";

                var loginData = new Dictionary<string, string>()
                {
                    ["username"] = "mfarias@axsistec.com",
                    ["password"] = "lab.ramo$",
                    ["grant_type"] = "password",
                };

                using HttpClient client = new();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("TokenClienteCert", certKey);

                var response = await client.PostAsync(url, new FormUrlEncodedContent(loginData));

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<LoginResponse>();
                    return data;
                }

                if ((int)response.StatusCode != 500)
                {
                    var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                    throw new CustomException(System.Net.HttpStatusCode.Unauthorized, string.Concat(error.Error, Environment.NewLine, "Descripción: ", error.ErrorDescription));
                }

                throw new Exception("Ha ocurrido un error al iniciar sesión con WeeClinic");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Dictionary<string, List<Dictionary<string, object>>>> PostService<T>(string serviceUrl, Dictionary<string, string> data)
        {
            try
            {
                var url = $"{baseUrl}/API/{serviceUrl}";

                using HttpClient client = new();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                client.DefaultRequestHeaders.Add("TokenClienteCert", certKey);

                var response = await client.PostAsync(url, new FormUrlEncodedContent(data));

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadFromJsonAsync<WeeClinicBase>();

                    if (!responseData.IsOk && (responseData.DsRespuesta == null || responseData.DsRespuesta.ContainsKey("Validacion")))
                    {
                        throw new Exception(string.IsNullOrEmpty(responseData.Mensaje) ? "Ha ocurrido un error con WeeClinic" : responseData.Mensaje);
                    }

                    if (responseData.DsRespuesta.ContainsKey("Validacion"))
                    {
                        var validations = responseData.DsRespuesta["Validacion"];

                        var message = string.Join(" | ", validations.SelectMany(x => x.Values));

                        throw new Exception(message);
                    }

                    return responseData.DsRespuesta;
                }

                if ((int)response.StatusCode == 400)
                {
                    var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                    throw new Exception(string.Concat(error.Error, Environment.NewLine, "Descripción: ", error.ErrorDescription));
                }

                if ((int)response.StatusCode == 401 || (int)response.StatusCode == 403)
                {
                    throw new Exception("Error de autenticación, token invalido o sin permisos con WeeClinic");
                }

                if ((int)response.StatusCode == 404)
                {
                    throw new Exception("No se encontró el servicio de WeeClinic");
                }

                throw new Exception("Ha ocurrido un error al buscar folio con WeeClinic");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<string> PostService<T>(string serviceUrl, MultipartFormDataContent multipartFormContent)
        {
            try
            {
                var url = $"{baseUrl}/API/{serviceUrl}";

                using HttpClient client = new();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                client.DefaultRequestHeaders.Add("TokenClienteCert", certKey);

                var response = await client.PostAsync(url, multipartFormContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    return responseData;
                }

                if ((int)response.StatusCode == 400)
                {
                    var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                    throw new Exception(string.Concat(error.Error, Environment.NewLine, "Descripción: ", error.ErrorDescription));
                }

                if ((int)response.StatusCode == 401 || (int)response.StatusCode == 403)
                {
                    throw new Exception("Error de autenticación, token invalido o sin permisos con WeeClinic");
                }

                throw new Exception("Ha ocurrido un error al buscar folio con WeeClinic");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

//{
//  "access_token": "QET6hcQtPUsrCUgN1CRjk7vr8Wco2TAIDpsHPdbdz-sywrc9DR0OKaN9uVA0Ht_jES7w-agFtcAEEWmnb-us0-nyOPFZ7iCDLM2fMSCgIPyv806sj9iIdIip09iNrOaPKmD6_gPWSM7rE2wkkNVA7ZMq9rUAoaz84BLQytR9qQzleZ8lcGiugPKBDuimaiic_84M-YCj1lAsebtj_Vlk3JB4PEIIQUKk7_G_IrShrBqfRjDgXC2USHPVsdECVsgz3NVcKjpk6Spx-ioOp-8VbXIWDArxXm1O-svpl63sIen6cgJ21l9LIMRkcfZo32nGjG0Zz5WXHkj4GbTyxVZiLa3h1CNIyzfSESq_EArhW7R2t4ZF-6juZkPpniTw8MjYhL3Q8t-TWwlwPc_jc29ppltLwPBIXzuif5gVOurpefR8YpmfnZzfXy2kO7XQwwc0",
//	"token_type": "bearer",
//	"expires_in": 2591999,
//	"userName": "mfarias@axsistec.com",
//	".issued": "Thu, 25 Aug 2022 14:37:48 GMT",
//	".expires": "Sat, 24 Sep 2022 14:37:48 GMT"
//}

//{
//  "error": "invalid_grant",
//	"error_description": "The user name or password is incorrect."
//}

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
