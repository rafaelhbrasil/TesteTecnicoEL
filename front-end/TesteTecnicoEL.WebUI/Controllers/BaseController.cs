using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TesteTecnicoEL.Dominio.Usuarios;

namespace TesteTecnicoEL.WebUI.Controllers
{
    public abstract class BaseController : Controller
    {
        private Cliente _cliente;

        public Cliente ClienteAutenticado
        {
            get
            {
                if (_cliente == null && this.User != null && this.User.Claims.Any() && !string.IsNullOrEmpty(this.User.Claims.First().Value))
                    _cliente = JsonConvert.DeserializeObject<Cliente>(this.User.Claims.First().Value);
                return _cliente;
            }
        }


        protected async Task SalvarCookieDeAutenticacao(Cliente cliente)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.UserData, Newtonsoft.Json.JsonConvert.SerializeObject(cliente))
                };
            var identity = new ClaimsIdentity(claims, "login");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true
                }
            );
        }
    }
}