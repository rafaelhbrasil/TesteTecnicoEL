using System;
using System.Collections.Generic;
using System.Text;
using TesteTecnicoEL.Dominio.Usuarios;
using TesteTecnicoEL.Dominio.Veiculos;
using Xunit;

namespace TesteTecnicoEL.TestesUnitarios.Dominio.Veiculos
{
    public class MarcaTestes
    {
        [Fact]
        public void TesteCriaMarca_TodosOsCamposPreenchidos_EntidadeValida()
        {
            var marca = new Marca("Nome");
            Assert.True(marca.EhValido());
        }

        [Fact]
        public void TesteCriaMarca_CamposFaltando_EntidadeInvalida()
        {
            var marca = new Marca(null);
            Assert.False(marca.EhValido());
            Assert.Equal(1, marca.Mensagens.Count);
            Assert.Contains(nameof(marca.Nome), marca.Mensagens[0]);
        }
    }
}
