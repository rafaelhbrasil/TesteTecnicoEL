using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TesteTecncicoEL.Api.Models;
using TesteTecnicoEL.Api.FiltrosDeRequisicao;
using TesteTecnicoEL.Dominio.Locacao;
using TesteTecnicoEL.Dominio.Locacao.ObjetosValor;
using TesteTecnicoEL.Dominio.Locacao.Repositorios;
using TesteTecnicoEL.Dominio.Locacao.Servicos;
using TesteTecnicoEL.Dominio.Veiculos;

namespace TesteTecncicoEL.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlugueisController : ControllerBase
    {
        private readonly UserIdentity _usuarioAutenticado;
        private readonly IAluguelRepositorio _aluguelRepositorio;
        private readonly ServicoAluguel _servicoAluguel;

        public AlugueisController(UserIdentity usuario, IAluguelRepositorio aluguelRepositorio, ServicoAluguel servicoAluguel)
        {
            _usuarioAutenticado = usuario;
            _aluguelRepositorio = aluguelRepositorio;
            _servicoAluguel = servicoAluguel;
        }

        /// <summary>
        /// Obtém os detalhes de um aluguel
        /// </summary>
        /// <param name="id">O ID do aluguel</param>
        /// <returns>Os detalhes do aluguel caso seja encontrado</returns>
        /// <response code="200">Aluguel encontrado e retornado com sucesso</response>
        /// <response code="404">Aluguel não encontrado</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Marca>> ObterPorId(long id)
        {
            var aluguel = await _aluguelRepositorio.ObterPorId(id);
            if (aluguel == null)
                return NotFound();
            return Ok(aluguel);
        }

        /// <summary>
        /// Lista todo o histórico de aluguéis cadastrados no sistema que são de um dado cliente
        /// </summary>
        /// <param name="id">O ID do cliente a ser filtrado</param>
        /// <returns>Uma lista contendo todos os aluguéis deste cliente</returns>
        /// <response code="200">Lista retornada com sucesso</response>
        /// <response code="401">Você precisa se autenticar para acessar essa funcionalidade</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [HttpGet("usuario")]
        [RotaAutenticada]
        public async Task<ActionResult<Marca>> ListarDoUsuario()
        {
            var alugueis = await _aluguelRepositorio.ListarPorUsuario(_usuarioAutenticado.Cliente.Id);
            return Ok(alugueis);
        }

        /// <summary>
        /// Simula um novo aluguel
        /// </summary>
        /// <param name="aluguelDto">Os dados do novo aluguel a ser simulado</param>
        /// <returns>O resultado da simulação</returns>
        /// <response code="201">A simulação foi feita com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string[]))]
        [HttpPost("simular")]
        public async Task<ActionResult> Simular(ParametrosLocacaoDto aluguelDto)
        {
            var aluguel = new Simulacao(aluguelDto.DataInicio,
                                      aluguelDto.DataFim,
                                      aluguelDto.IdVeiculo);

            aluguel.ValidarELancarErroSeInvalido();
            aluguel = await _servicoAluguel.SimularAluguel(aluguel);
            return Ok(aluguel);
        }

        /// <summary>
        /// Efetiva um novo aluguel
        /// </summary>
        /// <param name="aluguelDto">Os dados do novo aluguel a ser efetivado</param>
        /// <returns>O aluguel efetivado</returns>
        /// <response code="201">Aluguel efetivado com sucesso</response>
        /// <response code="400">Dados inválidos. Nada foi salvo.</response>
        /// <response code="401">Você precisa se autenticar para acessar essa funcionalidade</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string[]))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [HttpPost]
        [RotaAutenticada]
        public async Task<ActionResult> Criar(ParametrosLocacaoDto aluguelDto)
        {
            var aluguel = new Aluguel(aluguelDto.DataInicio,
                                      aluguelDto.DataFim,
                                      aluguelDto.IdVeiculo,
                                      aluguelDto.IdUsuario);
            aluguel.ValidarELancarErroSeInvalido();
            aluguel = await _servicoAluguel.RealizarAluguel(aluguel);
            return Created(Url.Action(nameof(ObterPorId), new { id = aluguel.Id }), aluguel);
        }

        /// <summary>
        /// Encerra um aluguel
        /// </summary>
        /// <param name="id">O ID do aluguel a ser encerrado</param>
        /// <param name="devolucaoDto">Os dados do aluguel a ser encerrado, incluindo checklist de devolução</param>
        /// <returns>O aluguel atualizado com os dados da devolução</returns>
        /// <response code="201">Devolução realuzada com sucesso</response>
        /// <response code="400">Dados inválidos. Nada foi feito.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string[]))]
        [HttpPost("devolucao/{id}")]
        public async Task<ActionResult> Devolver(long id, ParametrosDevolucaoDto devolucaoDto)
        {
            var checklist = new ChecklistDevolucao(devolucaoDto.CarroLimpo,
                                                   devolucaoDto.TanqueCheio,
                                                   devolucaoDto.SemAmassados,
                                                   devolucaoDto.SemArranhoes,
                                                   devolucaoDto.DataRealizacaoChecklist);
            checklist.ValidarELancarErroSeInvalido();
            var aluguel = await _servicoAluguel.RealizarDevolucao(id, checklist);
            return Ok(aluguel);
        }

    }
}
