using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;
using TesteTecnicoEL.Dominio;

namespace TesteTecncicoEL.Api.FiltrosDeRequisicao
{
    public class FiltroResposta : IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null)
                return;
            if (context.Exception is ValidacaoException ex)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var result = new ContentResult();
                result.Content = JsonConvert.SerializeObject(ex.Mensagens);
                context.Result = result;
                context.Exception = null;
            }
        }
    }
}
