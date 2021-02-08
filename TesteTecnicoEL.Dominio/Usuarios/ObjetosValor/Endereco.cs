using System;
using System.Collections.Generic;
using System.Text;

namespace TesteTecnicoEL.Dominio.Usuarios.ObjetosValor
{
    public class Endereco: EntidadeValidacao
    {
        public Endereco(string logradouro, int? numero, string complemento, string cidade, string estado)
        {
            Logradouro = logradouro?.Trim();
            Numero = numero;
            Complemento = complemento?.Trim();
            Cidade = cidade?.Trim();
            Estado = estado?.Trim();

            if (string.IsNullOrWhiteSpace(Logradouro))
                AdicionarMensagemErro($"{nameof(Logradouro)} é de preenchimento obrigatório");
            if (string.IsNullOrWhiteSpace(Cidade))
                AdicionarMensagemErro($"{nameof(Cidade)} é de preenchimento obrigatório");
            if (string.IsNullOrWhiteSpace(Estado))
                AdicionarMensagemErro($"{nameof(Estado)} é de preenchimento obrigatório");
        }

        public string Logradouro { get; set; }
        /// <summary>
        /// Nulo para S/N (sem número)
        /// </summary>
        public int? Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}
