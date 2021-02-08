using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteTecncicoEL.Api.Models;
using TesteTecnicoEL.Dominio.Usuarios;
using TesteTecnicoEL.Dominio.Usuarios.ObjetosValor;
using TesteTecnicoEL.Dominio.Usuarios.Repositorios;
using TesteTecnicoEL.Dominio.Usuarios.Servicos;
using TesteTecnicoEL.Dominio.Veiculos;
using TesteTecnicoEL.Dominio.Veiculos.Repositorios;

namespace TesteTecncicoEL.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VeiculosController : ControllerBase
    {
        private readonly IVeiculoRepositorio _veiculoRepositorio;

        public VeiculosController(IVeiculoRepositorio veiculoRepositorio)
        {
            this._veiculoRepositorio = veiculoRepositorio;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Veiculo>> ObterPorId(long id)
        {
            var marca = await _veiculoRepositorio.ObterPorId(id);
            if (marca == null)
                return NotFound();
            return Ok(marca);
        }

        [HttpPost]
        public async Task<ActionResult> Criar(VeiculoDto veiculoDto)
        {
            var veiculo = new Veiculo(veiculoDto.Placa,
                                      veiculoDto.IdModelo,
                                      veiculoDto.AnoFabricacao,
                                      veiculoDto.ValorHora,
                                      veiculoDto.IdCategoria,
                                      veiculoDto.CapacidadePortaMalaLitros);
            if (veiculo.EhValido())
            {
                await _veiculoRepositorio.Inserir(veiculo);
                return Created(Url.Action($"{nameof(ObterPorId)}", new { id = veiculo.Id }), null);
            }
            else
                return BadRequest(veiculo.Mensagens);
        }

        [HttpGet("modelo/{id}")]
        public async Task<ActionResult<Modelo>> ListarPorModelo(long id)
        {
            var veiculos = await _veiculoRepositorio.ListarPorModelo(id);
            return Ok(veiculos);
        }

        [HttpGet("categorias")]
        public async Task<ActionResult<Modelo>> ListarPorCategoria()
        {
            var veiculos = await _veiculoRepositorio.ListarCategorias();
            return Ok(veiculos);
        }

        [HttpGet("categoria/{id}")]
        public async Task<ActionResult<Modelo>> ListarPorCategoria(long id)
        {
            var veiculos = await _veiculoRepositorio.ListarPorCategoria(id);
            return Ok(veiculos);
        }
    }
}
