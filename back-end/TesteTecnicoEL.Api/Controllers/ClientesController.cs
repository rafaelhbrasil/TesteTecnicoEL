using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TesteTecncicoEL.Api.Models;
using TesteTecnicoEL.Api.FiltrosDeRequisicao;
using TesteTecnicoEL.Dominio.Usuarios;
using TesteTecnicoEL.Dominio.Usuarios.ObjetosValor;
using TesteTecnicoEL.Dominio.Usuarios.Repositorios;
using TesteTecnicoEL.Dominio.Usuarios.Servicos;

namespace TesteTecncicoEL.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly UserIdentity _usuarioAutenticado;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly ServicoCadastro _servicoCadastro;

        public ClientesController(UserIdentity usuario, IClienteRepositorio clienteRepositorio, ServicoCadastro servicoCadastro)
        {
            _usuarioAutenticado = usuario;
            _clienteRepositorio = clienteRepositorio;
            _servicoCadastro = servicoCadastro;
        }

        /// <summary>
        /// Obtém os detalhes de um cliente
        /// </summary>
        /// <param name="id">O ID do cliente</param>
        /// <returns>Os detalhes do cliente caso seja encontrado</returns>
        /// <response code="200">Cliente encontrado e retornado com sucesso</response>
        /// <response code="401">Você precisa se autenticar para acessar essa funcionalidade</response>
        /// <response code="404">Cliente não encontrado</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        [HttpGet("{id}")]
        [RotaAutenticada]
        public async Task<ActionResult<Cliente>> ObterPorId(long id)
        {
            if (!_usuarioAutenticado.EhOperador) // somente operador pode consultar dados de um cliente. Cliente pode ver os proprios dados.
                id = _usuarioAutenticado.Cliente.Id;
            var usuario = await _clienteRepositorio.ObterPorId(id);
            if (usuario == null)
                return NotFound();
            return Ok(usuario);
        }

        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        /// <param name="clienteDto">Os dados do novo modelo a ser criado</param>
        /// <returns></returns>
        /// <response code="201">O cliente foi criado com sucesso</response>
        /// <response code="400">Dados inválidos. O cliente não será salvo.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string[]))]
        [HttpPost]
        public async Task<ActionResult> Criar(ClienteDto clienteDto)
        {
            var endereco = new Endereco(clienteDto.Endereco.Logradouro,
                                        clienteDto.Endereco.Numero,
                                        clienteDto.Endereco.Complemento,
                                        clienteDto.Endereco.Cidade,
                                        clienteDto.Endereco.Estado);
            var cliente = new Cliente(clienteDto.Nome,
                                      clienteDto.CPF,
                                      clienteDto.Nascimento,
                                      endereco);
            if (cliente.EhValido())
            {
                cliente.SetSenha(clienteDto.Senha);
                await _servicoCadastro.Cadastrar(cliente);
                return Created(Url.Action(nameof(ObterPorId), new { id = cliente.Id }), null);
            }
            else
                return BadRequest(cliente.Mensagens.Concat(endereco.Mensagens));
        }
    }
}
