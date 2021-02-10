using System;
using TesteTecnicoEL.Dominio.Locacao.ObjetosValor;
using TesteTecnicoEL.Dominio.Veiculos;

namespace TesteTecnicoEL.Dominio.Locacao
{
    public class Aluguel : Simulacao
    {
        public Aluguel(DateTime dataInicio, DateTime dataDevolucaoPrevista, long idVeiculo, long idUsuario) :
                    base(dataInicio, dataDevolucaoPrevista, idVeiculo)
        {
            IdUsuario = idUsuario;

            if (IdUsuario <= 0)
                AdicionarMensagemErro($"{nameof(IdUsuario)} é de preenchimento obrigatório");
        }

        public DateTime? DataDevolucaoReal { get; private set; }
        public long IdUsuario { get; private set; }
        public float? ValorCobradoDevolucao { get; private set; }

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

            Veiculo.MarcarComoDisponivel();
        }

        internal void ConfirmarAluguel()
        {
            if (IdUsuario <= 0)
                throw new InvalidOperationException("Nenhum usuário informado");
            Veiculo.MarcarComoIndisponivel();
        }
    }
}
