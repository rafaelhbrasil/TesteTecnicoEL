using Microsoft.AspNetCore.Mvc.Filters;
using TesteTecnicoEL.WebUI.Controllers;

namespace TesteTecnicoEL.WebUI.Filters
{
    public class SetPropertiesActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            (context.Controller as BaseController).ViewBag.ClienteAutenticado = (context.Controller as BaseController).ClienteAutenticado;
        }
    }
}
