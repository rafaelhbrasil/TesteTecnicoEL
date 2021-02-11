using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TesteTecnicoEL.Api.Models;
using TesteTecnicoEL.AcessoDados;
using TesteTecnicoEL.AcessoDados.DTOs;

namespace TesteTecnicoEL.WebUI.Controllers
{
    public class AluguelController : BaseController
    {
        private readonly IVeiculoRepositorio _veiculoRepositorio;
        private readonly IAluguelRepositorio _aluguelRepositorio;

        public AluguelController(IVeiculoRepositorio veiculoRepositorio, IAluguelRepositorio aluguelRepositorio)
        {
            this._veiculoRepositorio = veiculoRepositorio;
            this._aluguelRepositorio = aluguelRepositorio;
        }

        [Authorize]
        public async Task<ActionResult> Index()
        {
            var historico = await _aluguelRepositorio.ListarHistoricoDoCliente();
            return View(historico.OrderBy(a => a.DataInicio));
        }

        // GET: AluguelController/Details/5
        public ActionResult Details(long id)
        {
            return View();
        }

        // GET: AluguelController/Create
        public async Task<ActionResult> Simular(long id)
        {
            ViewBag.Veiculo = await _veiculoRepositorio.ObterPorId(id);
            return View();
        }

        // POST: AluguelController/Create
        [HttpPost]
        [ActionName("Simular")]
        public async Task<ActionResult> RealizarSimulacao(ParametrosLocacaoDto parametrosLocacao)
        {

            try
            {
                var resultadoSimulacao = await _aluguelRepositorio.Simular(parametrosLocacao);
                return View("ResultadoSimulacao", resultadoSimulacao);
            }
            catch (ValidacaoException ex)
            {
                ViewBag.Erro = ex.Mensagens;
                ViewBag.Veiculo = await _veiculoRepositorio.ObterPorId(parametrosLocacao.IdVeiculo);
                return View(parametrosLocacao);
            }
        }

        // GET: AluguelController/Edit/5
        public ActionResult Resultado(long id)
        {
            return View();
        }

        // POST: AluguelController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Confirmar(ParametrosLocacaoDto parametrosLocacao)
        {
            try
            {
                if (ClienteAutenticado == null)
                {
                    ViewBag.Erro = new[] { nameof(UnauthorizedAccessException) };
                    return await RealizarSimulacao(parametrosLocacao);
                }
                parametrosLocacao.IdUsuario = ClienteAutenticado.Id;
                var resultadoLocacao = await _aluguelRepositorio.RealizarLocacao(parametrosLocacao);
                return View("ResultadoLocacao", resultadoLocacao);
            }
            catch (ValidacaoException ex)
            {
                ViewBag.Erro = ex.Mensagens;
                return await RealizarSimulacao(parametrosLocacao);
            }
        }

    }
}
