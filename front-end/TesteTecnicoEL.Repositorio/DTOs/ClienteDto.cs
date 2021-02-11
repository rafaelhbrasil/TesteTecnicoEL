using System;
using System.ComponentModel.DataAnnotations;

namespace TesteTecnicoEL.Api.Models
{
    public class ClienteDto
    {
        public long Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string CPF { get; set; }
        public DateTime Nascimento { get; set; }
        [Required]
        public EnderecoDto Endereco { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}
