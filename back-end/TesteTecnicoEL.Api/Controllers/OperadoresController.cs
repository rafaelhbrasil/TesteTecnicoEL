using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TesteTecncicoEL.Api.Models;
using TesteTecnicoEL.Dominio.Usuarios;
using TesteTecnicoEL.Dominio.Usuarios.Repositorios;

namespace TesteTecncicoEL.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperadoresController : ControllerBase
    {
        private readonly IOperadorRepositorio _operadorRepositorio;

        public OperadoresController(IOperadorRepositorio operadorRepositorio)
        {
            this._operadorRepositorio = operadorRepositorio;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Operador>> ObterPorId(long id)
        {
            var usuario = await _operadorRepositorio.ObterPorId(id);
            if (usuario == null)
                return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult> Criar(OperadorDto operadorDto)
        {
            var operador = new Operador(operadorDto.Matricula,
                                      operadorDto.Nome);
            if (operador.EhValido())
            {
                operador.SetSenha(operadorDto.Senha);
                await _operadorRepositorio.Inserir(operador);
                return Created(Url.Action($"{nameof(ObterPorId)}", new { id = operador.Id }), null);
            }
            else
                return BadRequest(operador.Mensagens);
        }
    }
}
