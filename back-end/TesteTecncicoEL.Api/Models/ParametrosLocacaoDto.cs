using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteTecncicoEL.Api.Models
{
    public class ParametrosLocacaoDto
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public long IdVeiculo { get; set; }
        public long IdUsuario { get; set; }
    }
}
