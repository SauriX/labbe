using Shared.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Shared.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<TResponse> ReadContentAs<TResponse>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new CustomException(HttpStatusCode.FailedDependency, $"A ocurrido un error al llamar el API: {response.ReasonPhrase}");
            }

            if (!response.IsSuccessStatusCode && response.StatusCode.In(HttpStatusCode.Unauthorized, HttpStatusCode.Forbidden, HttpStatusCode.NotFound))
            {
                throw new CustomException(response.StatusCode, response.ReasonPhrase);
            }

            if (!response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.InternalServerError)
            {
                var errorsAsString = await response.Content.ReadAsStringAsync();
                var errors = JsonSerializer.Deserialize<Dictionary<string, object>>(errorsAsString);

                var errorMessage = errors.ContainsKey("Errors") ? errors["Errors"] : errors.ContainsKey("ExceptionMessage") ? errors["ExceptionMessage"] : response.ReasonPhrase;

                throw new CustomException(response.StatusCode, $"El API respondió con el siguiente mensaje: {errorMessage}");
            }

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return default;
            }

            var dataAsString = await response.Content.ReadAsStringAsync();

            try
            {
                return JsonSerializer.Deserialize<TResponse>(dataAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (JsonException)
            {
                return (TResponse)Convert.ChangeType(dataAsString, typeof(TResponse));
            }
        }

        public static async Task<TResponse> GetAsJson<TResponse>(this HttpClient httpClient, string url)
        {
            var response = await httpClient.GetAsync(url);

            return await ReadContentAs<TResponse>(response);
        }

        public static async Task<TResponse> PostAsJson<TResponse>(this HttpClient httpClient, string url, object data)
        {
            var dataAsString = JsonSerializer.Serialize(data);

            var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PostAsync(url, content);

            return await ReadContentAs<TResponse>(response);
        }

        public static async Task<TResponse> PutAsJson<TResponse>(this HttpClient httpClient, string url, object data)
        {
            var dataAsString = JsonSerializer.Serialize(data);

            var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PutAsync(url, content);

            return await ReadContentAs<TResponse>(response);
        }
    }
}