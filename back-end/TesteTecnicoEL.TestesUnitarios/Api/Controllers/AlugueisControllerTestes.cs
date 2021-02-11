using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteTecncicoEL.Api.Controllers;
using TesteTecncicoEL.Api.Models;
using TesteTecnicoEL.Dominio;
using TesteTecnicoEL.Dominio.Locacao;
using TesteTecnicoEL.Dominio.Locacao.ObjetosValor;
using TesteTecnicoEL.Dominio.Locacao.Repositorios;
using TesteTecnicoEL.Dominio.Locacao.Servicos;
using TesteTecnicoEL.Dominio.Usuarios;
using Xunit;

namespace TesteTecnicoEL.TestesUnitarios.Api.Controllers
{
    public class AlugueisControllerTestes
    {
        private Mock<IAluguelRepositorio> _aluguelRepositorio;
        private Mock<IServicoAluguel> _servicoAluguel;
        private Mock<IUrlHelper> _urlHelper;

        public AlugueisControllerTestes()
        {
            _aluguelRepositorio = new Mock<IAluguelRepositorio>();
            _servicoAluguel = new Mock<IServicoAluguel>();
        }

        UserIdentity CriaUsuarioAutenticado(long Id, bool operador)
        {
            var identidade = new UserIdentity();
            if (operador) {
                var usuOperador = new Operador(Id.ToString("000000"), $"Usuario_{Id}");
                usuOperador.SetId(Id);
                identidade.ArmazenarUsuario(usuOperador);
            }
            else
            {
                var usuCliente = new Cliente($"Usuario_{Id}", Id.ToString("00000000000"), default, default);
                usuCliente.SetId(Id);
                identidade.ArmazenarUsuario(usuCliente);
            }
            return identidade;
        }
        Aluguel CriaAluguelVazio(long Id)
        {
            var aluguel = new Aluguel(default, default, Id*10, default);
            aluguel.SetId(Id);
            return aluguel;
        }

        [Fact]
        public async Task TesteObterPorId_Encontrado_Status200()
        {
            var aluguel = CriaAluguelVazio(1);
            _aluguelRepositorio.Setup(m => m.ObterPorId(aluguel.Id))
                               .ReturnsAsync(aluguel);
            var controller = new AlugueisController(null, _aluguelRepositorio.Object, _servicoAluguel.Object);
            var resposta = await controller.ObterPorId(aluguel.Id);
            Assert.Equal(200, (resposta.Result as OkObjectResult).StatusCode);
            Assert.Equal(aluguel, (resposta.Result as OkObjectResult).Value);
        }

        [Fact]
        public async Task TesteObterPorId_NaoEncontrado_Status404()
        {
            _aluguelRepositorio.Setup(m => m.ObterPorId(2))
                               .ReturnsAsync(null as Aluguel);
            var controller = new AlugueisController(null, _aluguelRepositorio.Object, _servicoAluguel.Object);
            var resposta = await controller.ObterPorId(2);
            Assert.Equal(404, (resposta.Result as StatusCodeResult).StatusCode);
        }

        [Fact]
        public async Task TesteListarDoUsuario_Autenticado_Status200() // anônimo nem chega no controller por causa do Request Filter
        {
            var aluguel = CriaAluguelVazio(101);
            var listaAlugueis = new List<Aluguel>() { aluguel };
            _aluguelRepositorio.Setup(m => m.ListarPorUsuario(11))
                               .ReturnsAsync(listaAlugueis);
            var controller = new AlugueisController(CriaUsuarioAutenticado(11, false), _aluguelRepositorio.Object, _servicoAluguel.Object);
            var resposta = await controller.ListarDoUsuario();
            Assert.Equal(200, (resposta.Result as OkObjectResult).StatusCode);
            Assert.Equal(listaAlugueis, (resposta.Result as OkObjectResult).Value);
        }

        [Fact]
        public async Task TesteSimular_DadosPreenchidos_Status200()
        {
            var aluguel = CriaAluguelVazio(201);
            var simulacao = new ParametrosLocacaoDto
            {
                DataInicio = DateTime.Now.AddMinutes(1),
                DataFim = DateTime.Now.AddMinutes(2),
                IdVeiculo = aluguel.IdVeiculo,
            };
            _servicoAluguel.Setup(m => m.SimularAluguel(It.Is<Simulacao>(s => s.IdVeiculo == simulacao.IdVeiculo)))
                               .ReturnsAsync(aluguel);
            var controller = new AlugueisController(null, _aluguelRepositorio.Object, _servicoAluguel.Object);
            var resposta = await controller.Simular(simulacao);
            Assert.Equal(200, (resposta.Result as OkObjectResult).StatusCode);
            Assert.Equal(aluguel, (resposta.Result as OkObjectResult).Value);
        }

        [Fact]
        public async Task TesteSimular_DadosInvalidos_Status400()
        {
            var simulacao = new ParametrosLocacaoDto();
            var aluguel = CriaAluguelVazio(202);
            var controller = new AlugueisController(null, _aluguelRepositorio.Object, _servicoAluguel.Object);
            await Assert.ThrowsAsync<ValidacaoException>(async () =>
            {
                await controller.Simular(simulacao);
            });
            _servicoAluguel.Verify(m => m.SimularAluguel(It.Is<Simulacao>(s => s.IdVeiculo == aluguel.IdVeiculo)), Times.Never);
        }

        [Fact]
        public async Task TesteCriar_DadosPreenchidos_Status200()
        {
            var aluguel = CriaAluguelVazio(203);
            var simulacao = new ParametrosLocacaoDto
            {
                DataInicio = DateTime.Now.AddMinutes(1),
                DataFim = DateTime.Now.AddMinutes(2),
                IdVeiculo = aluguel.IdVeiculo,
                IdUsuario = 23
            };
            _servicoAluguel.Setup(m => m.RealizarAluguel(It.Is<Aluguel>(s => s.IdVeiculo == simulacao.IdVeiculo)))
                               .ReturnsAsync(aluguel);
            var controller = new AlugueisController(null, _aluguelRepositorio.Object, _servicoAluguel.Object);
            controller.Url = new UrlHelperMock();
            var resposta = await controller.Criar(simulacao);
            Assert.Equal(201, (resposta.Result as CreatedResult).StatusCode);
            Assert.Equal(aluguel, (resposta.Result as CreatedResult).Value);
        }

        [Fact]
        public async Task TesteCriar_DadosInvalidos_Status400()
        {
            var simulacao = new ParametrosLocacaoDto();
            var aluguel = CriaAluguelVazio(204);
            var controller = new AlugueisController(null, _aluguelRepositorio.Object, _servicoAluguel.Object);
            await Assert.ThrowsAsync<ValidacaoException>(async () =>
            {
                await controller.Criar(simulacao);
            });
            _servicoAluguel.Verify(m => m.RealizarAluguel(It.Is<Aluguel>(s => s.IdVeiculo == aluguel.IdVeiculo)), Times.Never);
        }

        [Fact]
        public async Task TesteDevolver_DadosPreenchidos_Status200()
        {
            var aluguel = CriaAluguelVazio(205);
            var simulacao = new ParametrosDevolucaoDto() { DataRealizacaoChecklist = DateTime.Now.AddMinutes(1) };
            _servicoAluguel.Setup(m => m.RealizarDevolucao(aluguel.Id, It.IsAny<ChecklistDevolucao>()))
                               .ReturnsAsync(aluguel);
            var controller = new AlugueisController(null, _aluguelRepositorio.Object, _servicoAluguel.Object);
            controller.Url = new UrlHelperMock();
            var resposta = await controller.Devolver(aluguel.Id, simulacao);
            Assert.Equal(200, (resposta.Result as OkObjectResult).StatusCode);
            Assert.Equal(aluguel, (resposta.Result as OkObjectResult).Value);
        }

        [Fact]
        public async Task TesteDevolver_DadosInvalidos_Status400()
        {
            var simulacao = new ParametrosLocacaoDto();
            var aluguel = CriaAluguelVazio(204);
            var controller = new AlugueisController(null, _aluguelRepositorio.Object, _servicoAluguel.Object);
            await Assert.ThrowsAsync<ValidacaoException>(async () =>
            {
                await controller.Criar(simulacao);
            });
        }
    }

    class UrlHelperMock : IUrlHelper
    {
        public ActionContext ActionContext => throw new NotImplementedException();

        public string Action(UrlActionContext actionContext)
        {
            return "";
        }

        public string Content(string contentPath)
        {
            return "";
        }

        public bool IsLocalUrl(string url)
        {
            return true;
        }

        public string Link(string routeName, object values)
        {
            return "";
        }

        public string RouteUrl(UrlRouteContext routeContext)
        {
            return "";
        }
    }
}
