﻿using Moq;
using System;
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
            ServicoAutenticacao.CalcularSHA256("");
        }

        [Fact]
        public async Task TesteAutenticar_Cpf_RetornaCliente()
        {
            var cliente = new Cliente("nome", "10000000001", new DateTime(2000, 1, 1), null);
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
            var operador = new Operador("100001", "nome");
            operador.SetSenha("123");
            //operador.SetSenha("123");
            _operadorRepositorio.Setup(m => m.ObterPorMatricula(operador.Matricula))
                                .ReturnsAsync(operador);
            var servico = new ServicoAutenticacao(_clienteRepositorio.Object,
                                                  _operadorRepositorio.Object);
            var obj = await servico.Autenticar(operador.Matricula, "123");
            Assert.Equal(operador, obj);
        }

        [Fact]
        public async Task TesteObterPorChave_Cliente_RetornaCliente()
        {
            var cliente = new Cliente(null, null, default, default);
            _clienteRepositorio.Setup(m => m.ObterPorChave("chave1"))
                               .ReturnsAsync(cliente);
            var servico = new ServicoAutenticacao(_clienteRepositorio.Object,
                                                  _operadorRepositorio.Object);
            var obj = await servico.ObterPorChave("chave1");
            Assert.Equal(cliente, obj);
            _operadorRepositorio.Verify(m => m.ObterPorChave(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task TesteObterPorChave_Operador_RetornaOperador()
        {
            var operador = new Operador(null, null);
            _clienteRepositorio.Setup(m => m.ObterPorChave("chave2"))
                               .ReturnsAsync(null as Cliente);
            _operadorRepositorio.Setup(m => m.ObterPorChave("chave2"))
                               .ReturnsAsync(operador);
            var servico = new ServicoAutenticacao(_clienteRepositorio.Object,
                                                  _operadorRepositorio.Object);
            var obj = await servico.ObterPorChave("chave2");
            Assert.Equal(operador, obj);
            _clienteRepositorio.Verify(m => m.ObterPorChave(It.IsAny<string>()));
        }
    }
}
