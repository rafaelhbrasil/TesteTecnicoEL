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

        [HttpPost]
        [RotaAutenticada]
        public async Task<ActionResult> Criar(OperadorDto operadorDto)
        {
            if (!_usuarioAutenticado.EhOperador)
                return StatusCode((int)HttpStatusCode.Forbidden);
            var operador = new Operador(operadorDto.Matricula,
                                      operadorDto.Nome);
            if (operador.EhValido())
            {
                operador.SetSenha(operadorDto.Senha);
                await _servicoCadastro.Cadastrar(operador);
                return Created(Url.Action(nameof(ObterPorId), new { id = operador.Id }), null);
            }
            else
                return BadRequest(operador.Mensagens);
        }
    }
}
