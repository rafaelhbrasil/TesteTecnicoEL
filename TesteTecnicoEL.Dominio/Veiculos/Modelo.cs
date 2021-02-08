using System;
using System.Collections.Generic;
using System.Text;

namespace TesteTecnicoEL.Dominio.Veiculos
{
    public class Modelo: Entidade
    {
        public Modelo(string nome, long idMarca, Combustivel combustivel)
        {
            Nome = nome?.Trim();
            IdMarca = idMarca;
            Combustivel = combustivel;

            if (string.IsNullOrWhiteSpace(Nome))
                AdicionarMensagemErro($"{nameof(Nome)} é de preenchimento obrigatório");
            if (IdMarca <= 0)
                AdicionarMensagemErro($"{nameof(IdMarca)} é de preenchimento obrigatório");
            if ((int)Combustivel <= 0)
                AdicionarMensagemErro($"{nameof(Combustivel)} é de preenchimento obrigatório");
        }

        public string Nome { get; private set; }
        public long IdMarca { get; private set; }
        public Marca Marca { get; private set; }
        public Combustivel Combustivel { get; private set; }

        public void SetMarca(Marca marca)
        {
            Marca = marca;
            IdMarca = marca.Id;
        }

    }

    [Flags]
    public enum Combustivel
    {
        Gasolina = 1,
        Alcool = 2,
        Diesel = 4,
    }
}
