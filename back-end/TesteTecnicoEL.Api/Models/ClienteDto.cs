using System;

namespace TesteTecncicoEL.Api.Models
{
    public class ClienteDto
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime Nascimento { get; set; }
        public EnderecoDto Endereco { get; set; }
        public string Senha { get; set; }
    }
}
