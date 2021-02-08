using System;
using System.Collections.Generic;
using System.Text;
using TesteTecnicoEL.Dominio.Usuarios;
using TesteTecnicoEL.Dominio.Usuarios.ObjetosValor;
using Xunit;

namespace TesteTecnicoEL.TestesUnitarios.Dominio.Usuarios
{
    public class ClienteTestes
    {
        [Fact]
        public void TesteCriaCliente_TodosOsCamposPreenchidos_EntidadeValida()
        {
            var cliente = new Cliente("Nome",
                                    "12345678901",
                                    new DateTime(2000, 1, 2),
                                    new Endereco("Rua", 1, null, "BH", "MG"));
            Assert.True(cliente.EhValido());
        }

        [Fact]
        public void TesteCriaCliente_EnderecoInvalido_EntidadeInvalida()
        {
            var endereco = new Endereco("", 1, null, "BH", "MG");
            var cliente = new Cliente("Nome",
                                    "12345678901",
                                    new DateTime(2000, 1, 2),
                                    endereco);
            Assert.False(cliente.EhValido());
            Assert.False(endereco.EhValido());
            Assert.Equal(1, cliente.Mensagens.Count);
            Assert.Contains(nameof(cliente.Endereco), cliente.Mensagens[0]);
        }

        [Fact]
        public void TesteCriaCliente_CamposFaltando_EntidadeInvalida()
        {
            var cliente = new Cliente("",
                                    "",
                                    default,
                                    new Endereco("Rua", 1, null, "BH", "MG"));
            Assert.False(cliente.EhValido());
            Assert.Equal(3, cliente.Mensagens.Count);
            Assert.Contains(nameof(cliente.Nome), cliente.Mensagens[0]);
            Assert.Contains(nameof(cliente.CPF), cliente.Mensagens[1]);
            Assert.Contains(nameof(cliente.Nascimento), cliente.Mensagens[2]);
        }
    }
}
