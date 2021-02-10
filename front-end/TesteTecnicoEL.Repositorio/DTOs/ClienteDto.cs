using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
