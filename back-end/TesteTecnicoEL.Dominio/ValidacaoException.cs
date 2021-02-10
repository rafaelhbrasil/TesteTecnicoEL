using System;
using System.Collections.Generic;
using System.Text;

namespace TesteTecnicoEL.Dominio
{
    public class ValidacaoException : ApplicationException
    {
        public ValidacaoException(IEnumerable<string> mensagens)
        {
            Mensagens = mensagens;
        }

        public IEnumerable<string> Mensagens { get; }
    }
}
