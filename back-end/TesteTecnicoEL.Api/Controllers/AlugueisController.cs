using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TesteTecncicoEL.Api.Models;
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
        private readonly IAluguelRepositorio _aluguelRepositorio;
        private readonly ServicoAluguel _servicoAluguel;

        public AlugueisController(IAluguelRepositorio aluguelRepositorio, ServicoAluguel servicoAluguel)
        {
            _aluguelRepositorio = aluguelRepositorio;
            _servicoAluguel = servicoAluguel;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Marca>> ObterPorId(long id)
        {
            var aluguel = await _aluguelRepositorio.ObterPorId(id);
            if (aluguel == null)
                return NotFound();
            return Ok(aluguel);
        }

        [HttpGet("usuario/{id}")]
        public async Task<ActionResult<Marca>> ListarPorUsuario(long id)
        {
            var alugueis = await _aluguelRepositorio.ListarPorUsuario(id);
            return Ok(alugueis);
        }

        [HttpPost("simular")]
        public async Task<ActionResult> Simular(ParametrosLocacaoDto aluguelDto)
        {
            var aluguel = new Aluguel(aluguelDto.DataInicio,
                                      aluguelDto.DataFim,
                                      aluguelDto.IdVeiculo,
                                      aluguelDto.IdUsuario);
            if (aluguel.EhValido())
            {
                aluguel = await _servicoAluguel.SimularAluguel(aluguel);
                return Ok(aluguel);
            }
            else
                return BadRequest(aluguel.Mensagens);
        }

        [HttpPost]
        public async Task<ActionResult> Criar(ParametrosLocacaoDto aluguelDto)
        {
            var aluguel = new Aluguel(aluguelDto.DataInicio,
                                      aluguelDto.DataFim,
                                      aluguelDto.IdVeiculo,
                                      aluguelDto.IdUsuario);
            if (aluguel.EhValido())
            {
                aluguel = await _servicoAluguel.RealizarAluguel(aluguel);
                return Created(Url.Action($"{nameof(ObterPorId)}", new { id = aluguel.Id }), aluguel);
            }
            else
                return BadRequest(aluguel.Mensagens);
        }
        [HttpPost("devolucao/{id}")]
        public async Task<ActionResult> Devolver(long id, ParametrosDevolucaoDto devolucaoDto)
        {
            var checklist = new ChecklistDevolucao(devolucaoDto.CarroLimpo,
                                                   devolucaoDto.TanqueCheio,
                                                   devolucaoDto.SemAmassados,
                                                   devolucaoDto.SemArranhoes,
                                                   devolucaoDto.DataRealizacaoChecklist);
            if (checklist.EhValido())
            {
                var aluguel = await _servicoAluguel.RealizarDevolucao(id, checklist);
                return Ok(aluguel);
            }
            else
                return BadRequest(checklist.Mensagens);
        }

    }
}
