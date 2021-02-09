using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace TesteTecnicoEL.AcessoDados.Extensions
{
    public static class StringExtensions
    {
        public static string EncodeUrl(this string texto)
        {
            if (texto == null) return null;
            return HttpUtility.UrlEncode(texto);
        }
    }
}
