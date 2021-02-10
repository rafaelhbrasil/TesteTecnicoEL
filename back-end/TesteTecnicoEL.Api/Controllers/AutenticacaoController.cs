using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TesteTecnicoEL.Dominio.Usuarios;
using TesteTecnicoEL.Dominio.Usuarios.Repositorios;
using TesteTecnicoEL.Dominio.Usuarios.Servicos;

namespace TesteTecncicoEL.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IOperadorRepositorio _operadorRepositorio;
        private readonly ServicoAutenticacao _servicoAutenticacao;

        public AutenticacaoController(IClienteRepositorio clienteRepositorio,
            IOperadorRepositorio operadorRepositorio,
            ServicoAutenticacao servicoAutenticacao)
        {
            this._clienteRepositorio = clienteRepositorio;
            this._operadorRepositorio = operadorRepositorio;
            this._servicoAutenticacao = servicoAutenticacao;
        }

        /// <summary>
        /// Autentica um usuário com base em um nome de usuário (CPF ou matrícula) e uma senha
        /// </summary>
        /// <param name="login">CPF ou matrícula</param>
        /// <param name="senha">Senha do usuário</param>
        /// <returns>Dados do usuário autenticado</returns>
        /// <response code="200">Usuário autenticado com sucesso</response>
        /// <response code="401">Usuário ou senha inválidos ou usuário inexistente</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [HttpGet("autenticar")]
        public async Task<ActionResult<Cliente>> Autenticar(string login, string senha)
        {
            var usuario = await _servicoAutenticacao.Autenticar(login, senha);
            if (usuario == null)
                return Unauthorized();
            return Ok(usuario);
        }

    }
}
