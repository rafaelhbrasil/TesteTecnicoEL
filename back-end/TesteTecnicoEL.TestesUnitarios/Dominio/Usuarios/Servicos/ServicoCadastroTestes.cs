using Moq;
using System;
using System.Threading.Tasks;
using TesteTecnicoEL.Dominio;
using TesteTecnicoEL.Dominio.Usuarios;
using TesteTecnicoEL.Dominio.Usuarios.Repositorios;
using TesteTecnicoEL.Dominio.Usuarios.Servicos;
using Xunit;

namespace TesteTecnicoEL.TestesUnitarios.Dominio.Usuarios.Servicos
{
    public class ServicoCadastroTestes
    {
        private Mock<IClienteRepositorio> _clienteRepositorio;
        private Mock<IOperadorRepositorio> _operadorRepositorio;

        public ServicoCadastroTestes()
        {
            _clienteRepositorio = new Mock<IClienteRepositorio>();
            _operadorRepositorio = new Mock<IOperadorRepositorio>();
        }

        [Fact]
        public async Task TesteCadastrar_ClienteValido_InsereBancoDeDados()
        {
            var cliente = new Cliente("nome", "00000000001", new DateTime(2000, 1, 1), null);
            cliente.SetSenha("123");
            var servico = new ServicoCadastro(_clienteRepositorio.Object,
                                                  _operadorRepositorio.Object);
            await servico.Cadastrar(cliente);
            _clienteRepositorio.Verify(m => m.Inserir(cliente), Times.Once);
        }

        [Fact]
        public async Task TesteCadastrar_CpfJaUsado_GeraExcecao()
        {
            var cliente = new Cliente("nome", "00000000002", new DateTime(2000, 1, 1), null);
            cliente.SetSenha("123");
            _clienteRepositorio.Setup(m => m.ObterPorCpf(cliente.CPF))
                               .ReturnsAsync(cliente);
            var servico = new ServicoCadastro(_clienteRepositorio.Object,
                                              _operadorRepositorio.Object);
            await Assert.ThrowsAsync<ValidacaoException>(async () =>
            {
                await servico.Cadastrar(cliente);
            });
            _clienteRepositorio.Verify(m => m.Inserir(cliente), Times.Never);
        }

        [Fact]
        public async Task TesteCadastrar_OperadorValido_InsereBancoDeDados()
        {
            var operador = new Operador("000001", "nome");
            operador.SetSenha("123");
            var servico = new ServicoCadastro(_clienteRepositorio.Object,
                                                  _operadorRepositorio.Object);
            await servico.Cadastrar(operador);
            _operadorRepositorio.Verify(m => m.Inserir(operador), Times.Once);
        }

        [Fact]
        public async Task TesteCadastrar_MatriculaJaUsada_GeraExcecao()
        {
            var operador = new Operador("000002", "nome");
            operador.SetSenha("123");
            _operadorRepositorio.Setup(m => m.ObterPorMatricula(operador.Matricula))
                               .ReturnsAsync(operador);
            var servico = new ServicoCadastro(_clienteRepositorio.Object,
                                              _operadorRepositorio.Object);
            await Assert.ThrowsAsync<ValidacaoException>(async () =>
            {
                await servico.Cadastrar(operador);
            });
            _operadorRepositorio.Verify(m => m.Inserir(operador), Times.Never);
        }
    }
}
