using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteTecnicoEL.Dominio.Usuarios;
using TesteTecnicoEL.Dominio.Usuarios.Repositorios;
using TesteTecnicoEL.Dominio.Usuarios.Servicos;
using Xunit;

namespace TesteTecnicoEL.TestesUnitarios.Dominio.Usuarios.Servicos
{
    public class ServicoAutenticacaoTestes
    {
        private Mock<IClienteRepositorio> _clienteRepositorio;
        private Mock<IOperadorRepositorio> _operadorRepositorio;

        public ServicoAutenticacaoTestes()
        {
            _clienteRepositorio = new Mock<IClienteRepositorio>();
            _operadorRepositorio = new Mock<IOperadorRepositorio>();
        }

        [Fact]
        public async Task TesteAutenticar_Cpf_RetornaCliente()
        {
            var cliente = new Cliente("nome", "00000000001", new DateTime(2000, 1, 1), null);
            cliente.SetSenha("123");
            _clienteRepositorio.Setup(m => m.ObterPorCpf(cliente.CPF))
                               .ReturnsAsync(cliente);
            var servico = new ServicoAutenticacao(_clienteRepositorio.Object,
                                                  _operadorRepositorio.Object);
            var obj = await servico.Autenticar(cliente.CPF, "123");
            Assert.Equal(cliente, obj);
        }

        [Fact]
        public async Task TesteAutenticar_matricula_RetornaOperador()
        {
            var operador = new Operador("000001", "nome");
            operador.SetSenha("123");
            _operadorRepositorio.Setup(m => m.ObterPorMatricula(operador.Matricula))
                                .ReturnsAsync(operador);
            var servico = new ServicoAutenticacao(_clienteRepositorio.Object,
                                                  _operadorRepositorio.Object);
            var obj = await servico.Autenticar(operador.Matricula, "123");
            Assert.Equal(operador, obj);
        }
    }
}
