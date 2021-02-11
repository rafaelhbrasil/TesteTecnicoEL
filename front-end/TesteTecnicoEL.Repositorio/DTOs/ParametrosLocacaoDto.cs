using System;

namespace TesteTecnicoEL.Api.Models
{
    public class ParametrosLocacaoDto
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public long IdVeiculo { get; set; }
        public long IdUsuario { get; set; }
    }
}
