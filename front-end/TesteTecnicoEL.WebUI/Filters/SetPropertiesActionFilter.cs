using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
