using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TesteTecncicoEL.Api.Models;
using TesteTecnicoEL.Api.FiltrosDeRequisicao;
using TesteTecnicoEL.Dominio.Locacao;
using TesteTecnicoEL.Dominio.Locacao.ObjetosValor;
using TesteTecnicoEL.Dominio.Locacao.Repositorios;
using TesteTecnicoEL.Dominio.Locacao.Servicos;
using TesteTecnicoEL.Dominio.Usuarios;
using TesteTecnicoEL.Dominio.Veiculos;

namespace TesteTecncicoEL.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlugueisController : ControllerBase
    {
        private readonly Cliente _clienteAutenticado;
        private readonly UserIdentity _usuarioAutenticado;
        private readonly IAluguelRepositorio _aluguelRepositorio;
        private readonly ServicoAluguel _servicoAluguel;

        public AlugueisController(UserIdentity usuario, IAluguelRepositorio aluguelRepositorio, ServicoAluguel servicoAluguel)
        {
            _usuarioAutenticado = usuario;
            _aluguelRepositorio = aluguelRepositorio;
            _servicoAluguel = servicoAluguel;
        }

        [HttpGet("{id}", Order = 2)]
        public async Task<ActionResult<Marca>> ObterPorId(long id)
        {
            var aluguel = await _aluguelRepositorio.ObterPorId(id);
            if (aluguel == null)
                return NotFound();
            return Ok(aluguel);
        }

        [HttpGet("usuario")]
        [RotaAutenticada]
        public async Task<ActionResult<Marca>> ListarDoUsuario()
        {
            var alugueis = await _aluguelRepositorio.ListarPorUsuario(_usuarioAutenticado.Cliente.Id);
            return Ok(alugueis);
        }

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
