using System;
using TesteTecnicoEL.Dominio.Veiculos;

namespace TesteTecnicoEL.Dominio.Locacao
{
    public class Simulacao : Entidade
    {
        public Simulacao(DateTime dataInicio, DateTime dataDevolucaoPrevista, long idVeiculo)
        {
            DataInicio = dataInicio;
            DataDevolucaoPrevista = dataDevolucaoPrevista;
            IdVeiculo = idVeiculo;

            if (DataInicio == default)
                AdicionarMensagemErro($"{nameof(DataInicio)} é de preenchimento obrigatório");
            else if (DataInicio < DateTime.Now)
                AdicionarMensagemErro($"{nameof(DataInicio)} deve ser posterior à data atual");
            if (DataDevolucaoPrevista == default)
                AdicionarMensagemErro($"{nameof(DataDevolucaoPrevista)} é de preenchimento obrigatório");
            else if (DataInicio >= DataDevolucaoPrevista)
                AdicionarMensagemErro($"{nameof(DataInicio)} deve ser anterior à {nameof(DataDevolucaoPrevista)}");

            if (IdVeiculo <= 0)
                AdicionarMensagemErro($"{nameof(IdVeiculo)} é de preenchimento obrigatório");
        }

        public DateTime DataInicio { get; private set; }
        public DateTime DataDevolucaoPrevista { get; private set; }
        public long IdVeiculo { get; private set; }
        public Veiculo Veiculo { get; private set; }
        public float ValorAluguel { get; private set; }

        public void SetVeiculo(Veiculo veiculo)
        {
            IdVeiculo = veiculo.Id;
            Veiculo = veiculo;
        }

        public void CalcularValorPrevistoAluguel()
        {
            var horas = (int)Math.Ceiling((DataDevolucaoPrevista - DataInicio).TotalHours);
            ValorAluguel = horas * Veiculo.ValorHora;
        }

    }
}
