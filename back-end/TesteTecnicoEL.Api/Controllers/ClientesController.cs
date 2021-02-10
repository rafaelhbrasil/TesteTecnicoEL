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
