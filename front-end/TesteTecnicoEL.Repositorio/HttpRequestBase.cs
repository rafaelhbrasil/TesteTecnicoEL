using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TesteTecnicoEL.AcessoDados.DTOs;
using TesteTecnicoEL.Dominio;
using TesteTecnicoEL.Dominio.Usuarios;

namespace TesteTecnicoEL.AcessoDados
{
    public class HttpRequestBase : IHttpRequest
    {
        private readonly HttpClient _httpClient;
        private readonly Cliente _cliente;

        public HttpRequestBase(AppSettings settings, Cliente cliente)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(settings.CaminhoBaseApi);
            _httpClient.Timeout = TimeSpan.FromSeconds(20);

            _cliente = cliente;
            if (_cliente != null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _cliente.ChaveAutenticacao);
        }

        public async Task<TOut> PostAsync<TOut>(string relativePath, object objToPost)
        {
            var content = SerializeContent(objToPost);
            var response = await _httpClient.PostAsync(relativePath, content);
            string responseContent = await ReadResponseIfSucess(response);
            return JsonConvert.DeserializeObject<TOut>(responseContent);
        }

        public async Task PostAsync(string relativePath, object objToPost)
        {
            await PostAsync<object>(relativePath, objToPost);
        }

        public async Task<T> PutAsync<T>(string relativePath, object objToPost = null)
        {
            var content = SerializeContent(objToPost);
            var response = await _httpClient.PutAsync(relativePath, content);
            var responseContent = await ReadResponseIfSucess(response);
            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        public async Task PutAsync(string relativePath, object objToPost)
        {
            await PutAsync<object>(relativePath, objToPost);
        }

        public async Task DeleteAsync(string relativePath)
        {
            var response = await _httpClient.DeleteAsync(relativePath);
            await ReadResponseIfSucess(response);
        }

        public async Task<T> GetAsync<T>(string relativePath)
        {
            var responseContent = await GetAsync(relativePath);
            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        public async Task<string> GetAsync(string relativePath)
        {
            var response = await _httpClient.GetAsync(relativePath);
            return await ReadResponseIfSucess(response);
        }

        StringContent SerializeContent(object obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);
            return new StringContent(serialized, Encoding.UTF8, "application/json");
        }

        string SerializeToString(object obj)
        {
            if (obj == null) return string.Empty;
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        async Task<string> ReadResponseIfSucess(HttpResponseMessage response)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response), "Response can't be null in ReadResponseIfSucess");
            var responseContent = await response.Content?.ReadAsStringAsync() ?? string.Empty;
            if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                try
                {
                    var mensagens = JsonConvert.DeserializeObject<string[]>(responseContent);
                    throw new ValidacaoException(mensagens);
                }
                catch (ValidacaoException)
                {
                    throw;
                }
                catch
                {
                    // se houver erro, não é uma exceção da app e sim uma interna do servidor web. nem bateu na app.
                    // nesse caso, não propagar o erro de conversão e continuar lançando erro no EnsureSuccessStatusCode abaixo.
                }
            }
            response.EnsureSuccessStatusCode();
            return responseContent;
        }
    }
}
