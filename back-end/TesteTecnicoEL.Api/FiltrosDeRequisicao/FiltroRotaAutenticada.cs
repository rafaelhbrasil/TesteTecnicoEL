using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TesteTecncicoEL.Api.Models;
using TesteTecnicoEL.Api.FiltrosDeRequisicao;
using TesteTecnicoEL.Dominio.Usuarios;
using TesteTecnicoEL.Dominio.Usuarios.Servicos;

namespace TesteTecncicoEL.Api.FiltrosDeRequisicao
{
    public class FiltroRotaAutenticada : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (RotaEhAutenticada(context))
            {
                var authHeader = context.HttpContext.Request.Headers["Authorization"];
                if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.ToString().Contains(' '))
                {
                    ThrowUnauthorized(context);
                    return;
                }

                var chaveAutenticacao = authHeader.First().Split(' ')[1];
                var servicoUsuario = context.HttpContext.RequestServices.GetService(typeof(ServicoAutenticacao)) as ServicoAutenticacao;
                var usuario = await servicoUsuario.ObterPorChave(chaveAutenticacao);
                if (usuario == null)
                {
                    ThrowUnauthorized(context);
                    return;
                }
                CopiarDadosParaUserIdentity(context, usuario);
            }
            await next.Invoke();
        }

        private static void CopiarDadosParaUserIdentity(ActionExecutingContext context, object usuario)
        {
            var userIdentity = (context.HttpContext.User.Identities.Last() as UserIdentity);
            if (userIdentity == null) return;
            if (usuario is Cliente cliente)
                userIdentity.ArmazenarUsuario(cliente);
            else
                userIdentity.ArmazenarUsuario(usuario as Operador);
        }

        private void ThrowUnauthorized(ActionExecutingContext context)
        {
            context.Result = new Microsoft.AspNetCore.Mvc.StatusCodeResult((int)HttpStatusCode.Unauthorized);
        }

        static readonly string[] _anonymousUserActions = new[] { "authenticate", "post", "recoverpassword", "confirm" };
        private bool RotaEhAutenticada(ActionExecutingContext context)
        {
            var attributes = context.ActionDescriptor.EndpointMetadata.OfType<RotaAutenticadaAttribute>();
            return attributes.Any(); ;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // do something after the action executes
        }


    }
}
