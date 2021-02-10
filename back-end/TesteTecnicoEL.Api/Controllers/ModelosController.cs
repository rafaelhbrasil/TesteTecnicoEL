using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using TesteTecncicoEL.Api.Models;
using TesteTecnicoEL.Api.FiltrosDeRequisicao;
using TesteTecnicoEL.Dominio.Veiculos;
using TesteTecnicoEL.Dominio.Veiculos.Repositorios;

namespace TesteTecncicoEL.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModelosController : ControllerBase
    {
        private readonly IModeloRepositorio _modeloRepositorio;
        private readonly UserIdentity _usuarioAutenticado;

        public ModelosController(UserIdentity usuario, IModeloRepositorio modeloRepositorio)
        {
            _modeloRepositorio = modeloRepositorio;
            _usuarioAutenticado = usuario;
        }

        /// <summary>
        /// Obtém os detalhes de um modelo de veículo
        /// </summary>
        /// <param name="id">O ID do modelo</param>
        /// <returns>Os detalhes do modelo caso seja encontrado</returns>
        /// <response code="200">Modelo encontrado e retornado com sucesso</response>
        /// <response code="404">Modelo não encontrado</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Modelo>> ObterPorId(long id)
        {
            var marca = await _modeloRepositorio.ObterPorId(id);
            if (marca == null)
                return NotFound();
            return Ok(marca);
        }

        /// <summary>
        /// Cria um novo modelo de veículo. Somente um operador pode criar modelos.
        /// </summary>
        /// <param name="modeloDto">Os dados do novo modelo a ser criado</param>
        /// <returns></returns>
        /// <response code="201">O modelo foi criado com sucesso</response>
        /// <response code="400">Dados inválidos. O modelo não será salvo.</response>
        /// <response code="401">Você precisa se autenticar para acessar essa funcionalidade</response>
        /// <response code="403">Você não tem permissão para criar modelos de veículos</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string[]))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [HttpPost]
        [RotaAutenticada]
        public async Task<ActionResult> Criar(ModeloDto modeloDto)
        {
            if (!_usuarioAutenticado.EhOperador)
                return StatusCode((int)HttpStatusCode.Forbidden);
            var modelo = new Modelo(modeloDto.Nome,
                                    modeloDto.IdMarca,
                                    modeloDto.Combustivel);
            modelo.ValidarELancarErroSeInvalido();
            await _modeloRepositorio.Inserir(modelo);
            return Created(Url.Action(nameof(ObterPorId), new { id = modelo.Id }), null);
        }

        /// <summary>
        /// Lista todos os modelos de veículos cadastrados no sistema que são de uma dada marca
        /// </summary>
        /// <param name="id">O ID da marca a ser filtrada</param>
        /// <returns>Uma lista contendo todos os modelos desta marca</returns>
        /// <response code="200">Lista retornada com sucesso</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("marca/{id}")]
        public async Task<ActionResult<Modelo>> ListarPorMarca(long id)
        {
            var marcas = await _modeloRepositorio.ListarPorMarca(id);
            return Ok(marcas);
        }

        /// <summary>
        /// Altera os dados de um modelo de veículo. Somente um operador pode alterar modelos.
        /// </summary>
        /// <param name="id">O ID do modelo a ser alterado</param>
        /// <param name="modeloDto">Os novos dados do modelo</param>
        /// <returns></returns>
        /// <response code="204">O modelo foi alterado com sucesso</response>
        /// <response code="400">Dados inválidos. O modelo não será salvo.</response>
        /// <response code="401">Você precisa se autenticar para acessar essa funcionalidade</response>
        /// <response code="403">Você não tem permissão para alterar modelos de veículos</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string[]))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [HttpPut("{id}")]
        [RotaAutenticada]
        public async Task<ActionResult> Alterar(long id, ModeloDto modeloDto)
        {
            if (!_usuarioAutenticado.EhOperador)
                return StatusCode((int)HttpStatusCode.Forbidden);
            var modelo = new Modelo(modeloDto.Nome,
                                    modeloDto.IdMarca,
                                    modeloDto.Combustivel);
            modelo.ValidarELancarErroSeInvalido();
            modelo.SetId(id);
            await _modeloRepositorio.Alterar(modelo);
            return NoContent();
        }

        /// <summary>
        /// Exclui um modelo de veículo. Somente um operador pode excluir modelos.
        /// </summary>
        /// <param name="id">O ID do modelo a ser excluído</param>
        /// <returns></returns>
        /// <response code="204">O modelo foi excluído com sucesso</response>
        /// <response code="401">Você precisa se autenticar para acessar essa funcionalidade</response>
        /// <response code="403">Você não tem permissão para excluir modelos de veículos</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [HttpDelete("{id}")]
        [RotaAutenticada]
        public async Task<ActionResult> Excluir(long id)
        {
            if (!_usuarioAutenticado.EhOperador)
                return StatusCode((int)HttpStatusCode.Forbidden);
            await _modeloRepositorio.Excluir(id);
            return NoContent();
        }
    }
}
