using System.Threading.Tasks;
using TesteTecnicoEL.Dominio.Usuarios.Repositorios;

namespace TesteTecnicoEL.Dominio.Usuarios.Servicos
{
    public class ServicoCadastro: IServicoCadastro
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IOperadorRepositorio _operadorRepositorio;

        public ServicoCadastro(IClienteRepositorio clienteRepositorio,
                               IOperadorRepositorio operadorRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
            _operadorRepositorio = operadorRepositorio;
        }

        public async Task Cadastrar(Cliente cliente)
        {
            var clienteExistente = await _clienteRepositorio.ObterPorCpf(cliente.CPF);
            if (clienteExistente != null)
            {
                cliente.AdicionarMensagemErro("CPF já em uso por outro cliente");
                cliente.ValidarELancarErroSeInvalido();
            }
            await _clienteRepositorio.Inserir(cliente);
        }

        public async Task Cadastrar(Operador operador)
        {
            var operadorExistente = await _operadorRepositorio.ObterPorMatricula(operador.Matricula);
            if (operadorExistente != null)
            {
                operador.AdicionarMensagemErro("Matricula já em uso por outro operador");
                operador.ValidarELancarErroSeInvalido();
            }
            await _operadorRepositorio.Inserir(operador);
        }
    }
}
