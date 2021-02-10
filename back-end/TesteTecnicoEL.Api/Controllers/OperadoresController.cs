using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using TesteTecncicoEL.Api.Models;
using TesteTecnicoEL.Api.FiltrosDeRequisicao;
using TesteTecnicoEL.Dominio.Usuarios;
using TesteTecnicoEL.Dominio.Usuarios.Repositorios;
using TesteTecnicoEL.Dominio.Usuarios.Servicos;

namespace TesteTecncicoEL.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperadoresController : ControllerBase
    {
        private readonly UserIdentity _usuarioAutenticado;
        private readonly IOperadorRepositorio _operadorRepositorio;
        private readonly ServicoCadastro _servicoCadastro;

        public OperadoresController(UserIdentity usuario, IOperadorRepositorio operadorRepositorio, ServicoCadastro servicoCadastro)
        {
            _usuarioAutenticado = usuario;
            _operadorRepositorio = operadorRepositorio;
            _servicoCadastro = servicoCadastro;
        }

        /// <summary>
        /// Obtém os detalhes de um operador. Somente um operador pode obter detalhes de outro operador ou de si próprio.
        /// </summary>
        /// <param name="id">O ID do operador</param>
        /// <returns>Os detalhes do operador caso seja encontrado</returns>
        /// <response code="200">Operador retornado com sucesso</response>
        /// <response code="401">Você precisa se autenticar para acessar essa funcionalidade</response>
        /// <response code="404">Operador não encontrado</response>
        /// <response code="403">Você não tem permissão para obter este operador</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        [HttpGet("{id}")]
        [RotaAutenticada]
        public async Task<ActionResult<Operador>> ObterPorId(long id)
        {
            if (!_usuarioAutenticado.EhOperador)
                return StatusCode((int)HttpStatusCode.Forbidden);
            var usuario = await _operadorRepositorio.ObterPorId(id);
            if (usuario == null)
                return NotFound();
            return Ok(usuario);
        }

        /// <summary>
        /// Cria um novo operador. Somente um operador pode criar novos operadores.
        /// </summary>
        /// <param name="operadorDto">Os dados do novo operador a ser criado</param>
        /// <returns></returns>
        /// <response code="201">O operador foi criado com sucesso</response>
        /// <response code="400">Dados inválidos. O operador não será salvo.</response>
        /// <response code="401">Você precisa se autenticar para acessar essa funcionalidade</response>
        /// <response code="403">Você não tem permissão para criar operadores</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string[]))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [HttpPost]
        [RotaAutenticada]
        public async Task<ActionResult> Criar(OperadorDto operadorDto)
        {
            if (!_usuarioAutenticado.EhOperador)
                return StatusCode((int)HttpStatusCode.Forbidden);
            var operador = new Operador(operadorDto.Matricula,
                                      operadorDto.Nome);
            operador.ValidarELancarErroSeInvalido();
            operador.SetSenha(operadorDto.Senha);
            await _servicoCadastro.Cadastrar(operador);
            return Created(Url.Action(nameof(ObterPorId), new { id = operador.Id }), null);
        }

        /// <summary>
        /// Altera os dados de um operador. Somente um operador pode alterar operadores.
        /// </summary>
        /// <param name="id">O ID do operador a ser alterado</param>
        /// <param name="operadorDto">Os novos dados do operador</param>
        /// <returns></returns>
        /// <response code="204">O operador foi alterado com sucesso</response>
        /// <response code="400">Dados inválidos. O operador não será salvo.</response>
        /// <response code="401">Você precisa se autenticar para acessar essa funcionalidade</response>
        /// <response code="403">Você não tem permissão para alterar operadores</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string[]))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [HttpPut("{id}")]
        [RotaAutenticada]
        public async Task<ActionResult> Alterar(long id, OperadorDto operadorDto)
        {
            if (!_usuarioAutenticado.EhOperador)
                return StatusCode((int)HttpStatusCode.Forbidden);
            var operador = new Operador(operadorDto.Matricula,
                                      operadorDto.Nome);
            operador.ValidarELancarErroSeInvalido();
            operador.SetId(id);
            await _operadorRepositorio.Alterar(operador);
            return NoContent();
        }

        /// <summary>
        /// Exclui um operador. Somente um operador pode excluir operadores.
        /// </summary>
        /// <param name="id">O ID do operador a ser excluído</param>
        /// <returns></returns>
        /// <response code="204">O operador foi excluído com sucesso</response>
        /// <response code="401">Você precisa se autenticar para acessar essa funcionalidade</response>
        /// <response code="403">Você não tem permissão para excluir operadores</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [HttpDelete("{id}")]
        [RotaAutenticada]
        public async Task<ActionResult> Excluir(long id)
        {
            if (!_usuarioAutenticado.EhOperador)
                return StatusCode((int)HttpStatusCode.Forbidden);
            await _operadorRepositorio.Excluir(id);
            return NoContent();
        }
    }
}
