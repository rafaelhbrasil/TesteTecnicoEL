using System;
using System.Collections.Generic;
using System.Text;

namespace TesteTecnicoEL.Dominio.Veiculos
{
    public class Marca: Entidade
    {
        public Marca(string nome)
        {
            Nome = nome?.Trim();

            if (string.IsNullOrWhiteSpace(Nome))
                AdicionarMensagemErro($"{nameof(Nome)} é de preenchimento obrigatório");
        }
        public string Nome { get; private set; }
    }
}
