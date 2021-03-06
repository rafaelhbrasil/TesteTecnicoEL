﻿using System.ComponentModel.DataAnnotations;

namespace TesteTecnicoEL.Api.Models
{
    public class EnderecoDto
    {
        [Required]
        public string Logradouro { get; set; }
        /// <summary>
        /// Nulo para S/N (sem número)
        /// </summary>
        public int? Numero { get; set; }
        public string Complemento { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        public string Estado { get; set; }
    }
}
