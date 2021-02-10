using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TesteTecncicoEL.Api.Models;
using TesteTecnicoEL.Api.FiltrosDeRequisicao;
using TesteTecnicoEL.Dominio.Veiculos;
using TesteTecnicoEL.Dominio.Veiculos.Repositorios;

namespace TesteTecncicoEL.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarcasController : ControllerBase
    {
        private readonly IMarcaRepositorio _marcaRepositorio;

        public MarcasController(IMarcaRepositorio marcaRepositorio)
        {
            this._marcaRepositorio = marcaRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<Marca>> ObterPorTodas()
        {
            var marcas = await _marcaRepositorio.Listar();
            return Ok(marcas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Marca>> ObterPorId(long id)
        {
            var marca = await _marcaRepositorio.ObterPorId(id);
            if (marca == null)
                return NotFound();
            return Ok(marca);
        }

        [HttpPost]
        [RotaAutenticada]
        public async Task<ActionResult> Criar(MarcaDto marcaDto)
        {
            var marca = new Marca(marcaDto.Nome);
            if (marca.EhValido())
            {
                await _marcaRepositorio.Inserir(marca);
                return Created(Url.Action(nameof(ObterPorId), new { id = marca.Id }), null);
            }
            else
                return BadRequest(marca.Mensagens);
        }
    }
}
