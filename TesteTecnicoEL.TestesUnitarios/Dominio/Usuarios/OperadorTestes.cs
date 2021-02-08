using System;
using System.Collections.Generic;
using System.Text;
using TesteTecnicoEL.Dominio.Usuarios;
using Xunit;

namespace TesteTecnicoEL.TestesUnitarios.Dominio.Usuarios
{
    public class OperadorTestes
    {
        [Fact]
        public void TesteCriaOperador_TodosOsCamposPreenchidos_EntidadeValida()
        {
            var operador = new Operador("000000", "Nome");

            Assert.True(operador.EhValido());
        }

        [Fact]
        public void TesteCriaOperador_CamposFaltando_EntidadeInvalida()
        {
            var operador = new Operador("", null);
            Assert.False(operador.EhValido());
            Assert.Equal(2, operador.Mensagens.Count);
            Assert.Contains(nameof(operador.Matricula), operador.Mensagens[0]);
            Assert.Contains(nameof(operador.Nome), operador.Mensagens[1]);
        }
    }
}
