using Moq;
using System;
using System.Threading.Tasks;
using TesteTecnicoEL.Dominio.Locacao;
using TesteTecnicoEL.Dominio.Locacao.ObjetosValor;
using TesteTecnicoEL.Dominio.Locacao.Repositorios;
using TesteTecnicoEL.Dominio.Locacao.Servicos;
using TesteTecnicoEL.Dominio.Veiculos;
using TesteTecnicoEL.Dominio.Veiculos.Repositorios;
using Xunit;

namespace TesteTecnicoEL.TestesUnitarios.Dominio.Locacoes.Servicos
{
    public class ServicoAluguelTestes
    {
        private Mock<IAluguelRepositorio> _aluguelRepositorio;
        private Mock<IVeiculoRepositorio> _veiculoRepositorio;

        public ServicoAluguelTestes()
        {
            _aluguelRepositorio = new Mock<IAluguelRepositorio>();
            _veiculoRepositorio = new Mock<IVeiculoRepositorio>();
        }

        [Fact]
        public async Task TesteSimular_DadosPreenchidos_RetordaResultado()
        {
            var aluguel = new Aluguel(DateTime.Today.AddDays(1),
                                      DateTime.Today.AddDays(2),
                                      1, 1);
            var veiculo = new Veiculo("", 0, 0, 0, 0, 0);
            _veiculoRepositorio.Setup(m => m.ObterPorId(1))
                                .ReturnsAsync(veiculo);
            var servico = new ServicoAluguel(_aluguelRepositorio.Object, _veiculoRepositorio.Object);
            var resultado = await servico.SimularAluguel(aluguel);
            Assert.Equal(aluguel, resultado);
        }

        [Fact]
        public async Task TesteSimular_DadosInvalidos_RetordaErro()
        {
            var aluguel = new Aluguel(DateTime.Today.AddDays(1),
                                      DateTime.Today.AddDays(2),
                                      0, 0);
            var servico = new ServicoAluguel(_aluguelRepositorio.Object, _veiculoRepositorio.Object);
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                var resultado = await servico.SimularAluguel(aluguel);
            });
        }

        [Fact]
        public async Task TesteRealizarAluguel_DadosPreenchidos_RetordaResultado()
        {
            var aluguel = new Aluguel(DateTime.Today.AddDays(1),
                                      DateTime.Today.AddDays(2),
                                      2, 1);
            var veiculo = new Veiculo("", 0, 0, 0, 0, 0);
            _veiculoRepositorio.Setup(m => m.ObterPorId(2))
                                .ReturnsAsync(veiculo);
            var servico = new ServicoAluguel(_aluguelRepositorio.Object, _veiculoRepositorio.Object);
            var resultado = await servico.RealizarAluguel(aluguel);
            Assert.Equal(aluguel, resultado);
            _aluguelRepositorio.Verify(m => m.Inserir(aluguel));
        }

        [Fact]
        public async Task TesteRealizarAluguel_DadosInvalidos_RetordaErro()
        {
            var aluguel = new Aluguel(DateTime.Today.AddDays(1),
                                      DateTime.Today.AddDays(2),
                                      0, 0);
            var servico = new ServicoAluguel(_aluguelRepositorio.Object, _veiculoRepositorio.Object);
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                var resultado = await servico.RealizarAluguel(aluguel);
            });
            _aluguelRepositorio.Verify(m => m.Inserir(aluguel), Times.Never);
        }

        [Fact]
        public async Task TesteRealizarDevolucao_DadosPreenchidos_RetordaResultado()
        {
            var idAluguel = 3;
            var aluguel = new Aluguel(DateTime.Today.AddDays(1),
                                      DateTime.Today.AddDays(2),
                                      3, 1);
            aluguel.SetVeiculo(new Veiculo("", 1, 2, 20, 3, 4));
            aluguel.CalcularValorPrevistoAluguel();
            _aluguelRepositorio.Setup(m => m.ObterPorId(idAluguel))
                               .ReturnsAsync(aluguel);
            var checklist = new ChecklistDevolucao(true, true, true, true, aluguel.DataDevolucaoPrevista);
            var servico = new ServicoAluguel(_aluguelRepositorio.Object, _veiculoRepositorio.Object);
            var resultado = await servico.RealizarDevolucao(idAluguel, checklist);
            Assert.Equal(aluguel, resultado);
            Assert.Equal(20 * 24, resultado.ValorCobradoDevolucao);
            Assert.Equal(checklist.DataRealizacaoChecklist, aluguel.DataDevolucaoReal);
            _aluguelRepositorio.Verify(m => m.Atualizar(aluguel));
        }

        [Fact]
        public async Task TesteRealizarDevolucao_DadosInvalidos_RetordaErro()
        {
            var idAluguel = 4;
            var aluguel = new Aluguel(DateTime.Today.AddDays(1),
                                      DateTime.Today.AddDays(2),
                                      3, 1);
            aluguel.SetVeiculo(new Veiculo("", 1, 2, 20, 3, 4));
            aluguel.CalcularValorPrevistoAluguel();
            var checklist = new ChecklistDevolucao(true, true, true, true, aluguel.DataDevolucaoPrevista);
            aluguel.RealizarDevolucao(checklist);
            _aluguelRepositorio.Setup(m => m.ObterPorId(idAluguel))
                               .ReturnsAsync(aluguel);
            var servico = new ServicoAluguel(_aluguelRepositorio.Object, _veiculoRepositorio.Object);
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                var resultado = await servico.RealizarDevolucao(idAluguel, checklist);
            });
            _aluguelRepositorio.Verify(m => m.Inserir(aluguel), Times.Never);
        }
    }
}
