using System;
using System.Collections.Generic;
using System.Text;
using TesteTecnicoEL.Dominio.Usuarios;
using TesteTecnicoEL.Dominio.Veiculos;
using Xunit;

namespace TesteTecnicoEL.TestesUnitarios.Dominio.Veiculos
{
    public class ModeloTestes
    {
        [Fact]
        public void TesteCriaModelo_TodosOsCamposPreenchidos_EntidadeValida()
        {
            var modelo = new Modelo("Nome", 1, Combustivel.Diesel);
            Assert.True(modelo.EhValido());
        }

        [Fact]
        public void TesteCriaMarca_CamposFaltando_EntidadeInvalida()
        {
            var modelo = new Modelo(null, 0, (Combustivel)0);
            Assert.False(modelo.EhValido());
            Assert.Equal(3, modelo.Mensagens.Count);
            Assert.Contains(nameof(modelo.Nome), modelo.Mensagens[0]);
            Assert.Contains(nameof(modelo.IdMarca), modelo.Mensagens[1]);
            Assert.Contains(nameof(modelo.Combustivel), modelo.Mensagens[2]);
        }
    }
}
