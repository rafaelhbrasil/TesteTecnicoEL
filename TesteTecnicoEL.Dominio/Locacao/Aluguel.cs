using System;
using System.Collections.Generic;
using System.Text;
using TesteTecnicoEL.Dominio.Locacao.ObjetosValor;
using TesteTecnicoEL.Dominio.Veiculos;

namespace TesteTecnicoEL.Dominio.Locacao
{
    public class Aluguel : Entidade
    {
        public Aluguel(DateTime dataInicio, DateTime dataDevolucaoPrevista, long idVeiculo, long idUsuario)
        {
            DataInicio = dataInicio;
            DataDevolucaoPrevista = dataDevolucaoPrevista;
            IdVeiculo = idVeiculo;
            IdUsuario = idUsuario;

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
            if (IdUsuario <= 0)
                AdicionarMensagemErro($"{nameof(IdUsuario)} é de preenchimento obrigatório");
        }

        public DateTime DataInicio { get; private set; }
        public DateTime DataDevolucaoPrevista { get; private set; }
        public DateTime? DataDevolucaoReal { get; private set; }
        public long IdVeiculo { get; private set; }
        public Veiculo Veiculo { get; private set; }
        public long IdUsuario { get; private set; }
        public float ValorAluguel { get; private set; }
        public float ValorCobradoDevolucao { get; private set; }

        public ChecklistDevolucao ChecklistDevolucao { get; private set; }

        public void RealizarDevolucao(ChecklistDevolucao checklist)
        {
            ChecklistDevolucao = checklist;
            var quantidadeProblemas = 0;
            if (!checklist.CarroLimpo)
                quantidadeProblemas++;
            if (!checklist.TanqueCheio)
                quantidadeProblemas++;
            if (!checklist.SemAmassados)
                quantidadeProblemas++;
            if (!checklist.SemArranhoes)
                quantidadeProblemas++;
            ValorCobradoDevolucao = ValorAluguel + (ValorAluguel * quantidadeProblemas * 0.3f);
            DataDevolucaoReal = checklist.DataRealizacaoChecklist;
        }

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
