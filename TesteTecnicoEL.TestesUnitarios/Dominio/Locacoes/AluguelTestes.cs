using System;
using System.Collections.Generic;
using System.Text;
using TesteTecnicoEL.Dominio.Locacao;
using TesteTecnicoEL.Dominio.Locacao.ObjetosValor;
using TesteTecnicoEL.Dominio.Usuarios;
using TesteTecnicoEL.Dominio.Veiculos;
using Xunit;

namespace TesteTecnicoEL.TestesUnitarios.Dominio.Locacoes
{
    public class AluguelTestes
    {
        [Fact]
        public void TesteCriaAluguel_TodosOsCamposPreenchidos_EntidadeValida()
        {
            var aluguel = new Aluguel(DateTime.Today.AddDays(1), DateTime.Today.AddDays(2), 1, 1);

            Assert.True(aluguel.EhValido());
        }

        [Fact]
        public void TesteCriaOperador_CamposFaltando_EntidadeInvalida()
        {
            var aluguel = new Aluguel(default, default, 0, 0);
            Assert.False(aluguel.EhValido());
            Assert.Equal(4, aluguel.Mensagens.Count);
            Assert.Contains(nameof(aluguel.DataInicio), aluguel.Mensagens[0]);
            Assert.Contains(nameof(aluguel.DataDevolucaoPrevista), aluguel.Mensagens[1]);
            Assert.Contains(nameof(aluguel.IdVeiculo), aluguel.Mensagens[2]);
            Assert.Contains(nameof(aluguel.IdUsuario), aluguel.Mensagens[3]);
        }

        [Fact]
        public void TesteRealizarDevolucao_SemAvarias_ValorOriginal()
        {
            var aluguel = new Aluguel(DateTime.Today.AddDays(1), DateTime.Today.AddDays(2), 1, 1);
            aluguel.SetVeiculo(new Veiculo("", 1, 2, 20, 3, 4));
            aluguel.CalcularValorPrevistoAluguel();
            var checklist = new ChecklistDevolucao(true, true, true, true, aluguel.DataDevolucaoPrevista.AddHours(1));
            aluguel.RealizarDevolucao(checklist);

            Assert.Equal(480, aluguel.ValorAluguel);
            Assert.Equal(aluguel.ValorAluguel, aluguel.ValorCobradoDevolucao);
            Assert.Equal(aluguel.DataDevolucaoReal, checklist.DataRealizacaoChecklist);
        }

        [Fact]
        public void TesteRealizarDevolucao_ComAvarias_ValorComAcrescimo()
        {
            var aluguel = new Aluguel(DateTime.Today.AddDays(1), DateTime.Today.AddDays(2), 1, 1);
            aluguel.SetVeiculo(new Veiculo("", 1, 2, 20, 3, 4));
            aluguel.CalcularValorPrevistoAluguel();
            var checklist = new ChecklistDevolucao(false, false, false, false, aluguel.DataDevolucaoPrevista.AddHours(1));
            aluguel.RealizarDevolucao(checklist);

            Assert.Equal(480, aluguel.ValorAluguel);
            Assert.Equal(480 * 2.2, aluguel.ValorCobradoDevolucao);
            Assert.Equal(aluguel.DataDevolucaoReal, checklist.DataRealizacaoChecklist);
        }
    }
}
