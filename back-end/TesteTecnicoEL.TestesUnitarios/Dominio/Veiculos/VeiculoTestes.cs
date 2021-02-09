using TesteTecnicoEL.Dominio.Veiculos;
using Xunit;

namespace TesteTecnicoEL.TestesUnitarios.Dominio.Veiculos
{
    public class VeiculoTestes
    {
        [Fact]
        public void TesteCriaVeiculo_TodosOsCamposPreenchidos_EntidadeValida()
        {
            var veiculo = new Veiculo("AAA0001", 1, 2020, 20, 1, 250);
            Assert.True(veiculo.EhValido());
        }

        [Fact]
        public void TesteCriaMarca_CamposFaltando_EntidadeInvalida()
        {
            var veiculo = new Veiculo(string.Empty, 0, 0, 0, 0, 0);
            Assert.False(veiculo.EhValido());
            Assert.Equal(6, veiculo.Mensagens.Count);
            Assert.Contains(nameof(veiculo.Placa), veiculo.Mensagens[0]);
            Assert.Contains(nameof(veiculo.IdModelo), veiculo.Mensagens[1]);
            Assert.Contains(nameof(veiculo.AnoFabricacao), veiculo.Mensagens[2]);
            Assert.Contains(nameof(veiculo.ValorHora), veiculo.Mensagens[3]);
            Assert.Contains(nameof(veiculo.IdCategoria), veiculo.Mensagens[4]);
            Assert.Contains(nameof(veiculo.CapacidadePortaMalaLitros), veiculo.Mensagens[5]);
        }
    }
}
