using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Lista todos as marcas de veículos cadastradas no sistema
        /// </summary>
        /// <returns>Uma lista contendo todos as marcas</returns>
        /// <response code="200">Dados listados com sucesso</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<Marca>> ObterPorTodas()
        {
            var marcas = await _marcaRepositorio.Listar();
            return Ok(marcas);
        }

        /// <summary>
        /// Obtém os detalhes de uma marca de veículo
        /// </summary>
        /// <param name="id">O ID da marca</param>
        /// <returns>Os detalhes da marca caso seja encontrado</returns>
        /// <response code="200">Marca encontrada e retornada com sucesso</response>
        /// <response code="404">Marca não encontrada</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Marca>> ObterPorId(long id)
        {
            var marca = await _marcaRepositorio.ObterPorId(id);
            if (marca == null)
                return NotFound();
            return Ok(marca);
        }

        /// <summary>
        /// Cria uma nova marca de veículo. Somente um operador pode criar marcas.
        /// </summary>
        /// <param name="marcaDto">Os dados da nova marca a ser criada</param>
        /// <returns></returns>
        /// <response code="201">A marca foi com sucesso</response>
        /// <response code="400">Dados inválidos. A marca não será salva.</response>
        /// <response code="401">Você precisa se autenticar para acessar essa funcionalidade</response>
        /// <response code="403">Você não tem permissão para criar marcas.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string[]))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
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
