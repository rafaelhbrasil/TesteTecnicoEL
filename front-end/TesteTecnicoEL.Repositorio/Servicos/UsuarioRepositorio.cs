using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using TesteTecnicoEL.Api.Models;
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

        public async Task<Cliente> CadastrarCliente(ClienteDto cliente)
        {
            await _httpRequest.PostAsync($"Clientes", cliente);
            var novoCliente = (await Autenticar(cliente.CPF, cliente.Senha));
            return novoCliente as Cliente;
        }
    }
}
