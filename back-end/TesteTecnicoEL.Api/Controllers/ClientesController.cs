using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
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
        private readonly IServicoCadastro _servicoCadastro;

        public ClientesController(UserIdentity usuario, IClienteRepositorio clienteRepositorio, IServicoCadastro servicoCadastro)
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
            var endereco = new Endereco(clienteDto.Endereco?.Logradouro,
                                        clienteDto.Endereco?.Numero,
                                        clienteDto.Endereco?.Complemento,
                                        clienteDto.Endereco?.Cidade,
                                        clienteDto.Endereco?.Estado);
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

        /// <summary>
        /// Altera os dados de um cliente. Somente um operador pode alterar clientes. Um cliente pode alterar seus próprios dados.
        /// </summary>
        /// <param name="id">O ID do cliente a ser alterado</param>
        /// <param name="clienteDto">Os novos dados do cliente</param>
        /// <returns></returns>
        /// <response code="204">O cliente foi alterado com sucesso</response>
        /// <response code="400">Dados inválidos. O cliente não será salvo.</response>
        /// <response code="401">Você precisa se autenticar para acessar essa funcionalidade</response>
        /// <response code="403">Você não tem permissão para alterar este cliente</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string[]))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [HttpPut("{id}")]
        [RotaAutenticada]
        public async Task<ActionResult> Alterar(long id, ClienteDto clienteDto)
        {
            if (!_usuarioAutenticado.EhOperador && _usuarioAutenticado.Cliente.Id != id)
                return StatusCode((int)HttpStatusCode.Forbidden);
            var endereco = new Endereco(clienteDto.Endereco.Logradouro,
                                        clienteDto.Endereco.Numero,
                                        clienteDto.Endereco.Complemento,
                                        clienteDto.Endereco.Cidade,
                                        clienteDto.Endereco.Estado);
            var cliente = new Cliente(clienteDto.Nome,
                                      clienteDto.CPF,
                                      clienteDto.Nascimento,
                                      endereco);
            cliente.SetId(id);
            if (cliente.EhValido())
            {
                await _clienteRepositorio.Alterar(cliente);
                return NoContent();
            }
            return BadRequest(cliente.Mensagens.Concat(endereco.Mensagens));
        }

        /// <summary>
        /// Exclui um cliente. Somente um operador pode excluir clientes.
        /// </summary>
        /// <param name="id">O ID do cliente a ser excluído</param>
        /// <returns></returns>
        /// <response code="204">O cliente foi excluído com sucesso</response>
        /// <response code="401">Você precisa se autenticar para acessar essa funcionalidade</response>
        /// <response code="403">Você não tem permissão para excluir clientes</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [HttpDelete("{id}")]
        [RotaAutenticada]
        public async Task<ActionResult> Excluir(long id)
        {
            if (!_usuarioAutenticado.EhOperador)
                return StatusCode((int)HttpStatusCode.Forbidden);
            await _clienteRepositorio.Excluir(id);
            return NoContent();
        }
    }
}
