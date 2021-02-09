using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteTecncicoEL.Api.Models;
using TesteTecnicoEL.AcessoDados.Extensions;
using TesteTecnicoEL.Dominio.Usuarios;

namespace TesteTecnicoEL.AcessoDados
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly IHttpRequest _httpRequest;

        public UsuarioRepositorio(IHttpRequest httpRequest)
        {
            this._httpRequest = httpRequest;
        }
        public async Task<object> Autenticar(string login, string senha)
        {
            var usuarioJson = await _httpRequest.GetAsync<JObject>($"Autenticacao/autenticar?login={login.EncodeUrl()}&senha={senha.EncodeUrl()}");
            if (usuarioJson == null || usuarioJson["id"] == null) return null;
            if (usuarioJson["cpf"] != null)
                return JsonConvert.DeserializeObject<Cliente>(usuarioJson.ToString());
            return JsonConvert.DeserializeObject<Operador>(usuarioJson.ToString());
        }

        public Task<Cliente> CadastrarCliente(ClienteDto cliente)
        {
            return _httpRequest.PostAsync<Cliente>($"Clientes", cliente);
        }
    }
}
