using System;
using System.ComponentModel.DataAnnotations;

namespace TesteTecncicoEL.Api.Models
{
    public class ClienteDto
    {
        public long Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string CPF { get; set; }
        public DateTime Nascimento { get; set; }
        public EnderecoDto Endereco { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}
