using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using TesteTecncicoEL.Api.Models;
using TesteTecnicoEL.Dominio.Veiculos;
using TesteTecnicoEL.Dominio.Veiculos.Repositorios;

namespace TesteTecncicoEL.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VeiculosController : ControllerBase
    {
        private readonly UserIdentity _usuarioAutenticado;
        private readonly IVeiculoRepositorio _veiculoRepositorio;

        public VeiculosController(UserIdentity usuario, IVeiculoRepositorio veiculoRepositorio)
        {
            _usuarioAutenticado = usuario;
            _veiculoRepositorio = veiculoRepositorio;
        }


        [HttpGet]
        public async Task<ActionResult<Veiculo>> ListarTodos()
        {
            var marca = await _veiculoRepositorio.Listar();
            if (marca == null)
                return NotFound();
            return Ok(marca);
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
            if (!_usuarioAutenticado.EhOperador)
                return StatusCode((int)HttpStatusCode.Forbidden);
            var veiculo = new Veiculo(veiculoDto.Placa,
                                      veiculoDto.IdModelo,
                                      veiculoDto.AnoFabricacao,
                                      veiculoDto.ValorHora,
                                      veiculoDto.IdCategoria,
                                      veiculoDto.CapacidadePortaMalaLitros);
            if (veiculo.EhValido())
            {
                await _veiculoRepositorio.Inserir(veiculo);
                return Created(Url.Action(nameof(ObterPorId), new { id = veiculo.Id }), null);
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
        public async Task<ActionResult<Modelo>> ListarCategorias()
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
