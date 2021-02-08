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
    public class ModelosController : ControllerBase
    {
        private readonly IModeloRepositorio _modeloRepositorio;

        public ModelosController(IModeloRepositorio modeloRepositorio)
        {
            this._modeloRepositorio = modeloRepositorio;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Modelo>> ObterPorId(long id)
        {
            var marca = await _modeloRepositorio.ObterPorId(id);
            if (marca == null)
                return NotFound();
            return Ok(marca);
        }

        [HttpPost]
        public async Task<ActionResult> Criar(ModeloDto modeloDto)
        {
            var modelo = new Modelo(modeloDto.Nome,
                                    modeloDto.IdMarca,
                                    modeloDto.Combustivel);
            if (modelo.EhValido())
            {
                await _modeloRepositorio.Inserir(modelo);
                return Created(Url.Action($"{nameof(ObterPorId)}", new { id = modelo.Id }), null);
            }
            else
                return BadRequest(modelo.Mensagens);
        }

        [HttpGet("marca/{id}")]
        public async Task<ActionResult<Modelo>> ListarPorMarca(long id)
        {
            var marcas = await _modeloRepositorio.ListarPorMarca(id);
            return Ok(marcas);
        }
    }
}
