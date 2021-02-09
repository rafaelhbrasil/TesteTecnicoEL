using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteTecncicoEL.Api.Models;
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
        // GET: AluguelController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AluguelController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AluguelController/Create
        public async Task<ActionResult> Simular(int id)
        {
            ViewBag.Veiculo = await _veiculoRepositorio.ObterPorId(id);
            return View();
        }

        // POST: AluguelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Simular(ParametrosLocacaoDto parametrosLocacao)
        {
            
            try
            {
                var resultadoSimulacao = await _aluguelRepositorio.Simular(parametrosLocacao);
                return View(nameof(Resultado), resultadoSimulacao);
            }
            catch(ValidacaoException ex)
            {
                ViewBag.Erro = ex.Mensagens;
                ViewBag.Veiculo = await _veiculoRepositorio.ObterPorId(parametrosLocacao.IdVeiculo);
                return View(parametrosLocacao);
            }
        }

        // GET: AluguelController/Edit/5
        public ActionResult Resultado(int id)
        {
            return View();
        }

        // POST: AluguelController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Confirmacao(ParametrosLocacaoDto parametrosLocacao)
        {
            try
            {
                parametrosLocacao.IdUsuario = ClienteAutenticado.Id;
                var resultadoSimulacao = await _aluguelRepositorio.RealizarLocacao(parametrosLocacao);
                return View(nameof(Resultado), resultadoSimulacao);
            }
            catch (ValidacaoException ex)
            {
                ViewBag.Erro = ex.Mensagens;
                ViewBag.Veiculo = await _veiculoRepositorio.ObterPorId(parametrosLocacao.IdVeiculo);
                return View(parametrosLocacao);
            }
        }

        // GET: AluguelController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AluguelController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
