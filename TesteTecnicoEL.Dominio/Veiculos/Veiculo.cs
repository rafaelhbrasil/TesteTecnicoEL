using System;
using System.Collections.Generic;
using System.Text;

namespace TesteTecnicoEL.Dominio.Veiculos
{
    public class Veiculo : Entidade
    {
        public Veiculo(string placa,
            long idModelo,
            int anoFabricacao,
            double valorHora,
            long idCategoria,
            int capacidadePortaMalaLitros)
        {
            Placa = placa;
            IdModelo = idModelo;
            AnoFabricacao = anoFabricacao;
            ValorHora = (float)valorHora;
            IdCategoria = idCategoria;
            CapacidadePortaMalaLitros = capacidadePortaMalaLitros;

            if (string.IsNullOrWhiteSpace(Placa))
                AdicionarMensagemErro($"{nameof(Placa)} é de preenchimento obrigatório");
            if (IdModelo <= 0)
                AdicionarMensagemErro($"{nameof(IdModelo)} é de preenchimento obrigatório");
            if (AnoFabricacao <= 0 || AnoFabricacao > DateTime.Today.Year + 1)
                AdicionarMensagemErro($"{nameof(AnoFabricacao)} é inválido");
            if (ValorHora <= 0)
                AdicionarMensagemErro($"{nameof(ValorHora)} é inválido");
            if (IdCategoria <= 0)
                AdicionarMensagemErro($"{nameof(IdCategoria)} é de preenchimento obrigatório");
            if (CapacidadePortaMalaLitros <= 0)
                AdicionarMensagemErro($"{nameof(CapacidadePortaMalaLitros)} é inválido");
        }

        public string Placa { get; private set; }
        public long IdModelo { get; set; }
        public Modelo Modelo { get; private set; }
        public int AnoFabricacao { get; private set; }
        public int CapacidadePortaMalaLitros { get; private set; }
        public float ValorHora { get; private set; }
        public long IdCategoria { get; private set; }
        public Categoria Categoria { get; private set; }

        public void SetCategoria(Categoria categoria)
        {
            Categoria = categoria;
            IdCategoria = categoria.Id;
        }

        public void SetModelo(Modelo modelo)
        {
            Modelo = modelo;
            IdModelo = modelo.Id;
        }
    }


}
