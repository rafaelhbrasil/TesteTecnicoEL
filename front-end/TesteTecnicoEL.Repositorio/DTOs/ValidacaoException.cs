using System;

namespace TesteTecnicoEL.AcessoDados.DTOs
{
    public class ValidacaoException : ApplicationException
    {
        public ValidacaoException(string[] mensagens)
        {
            Mensagens = mensagens;
        }

        public string[] Mensagens { get; }
    }
}
