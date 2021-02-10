using System.Linq;
using System.Threading.Tasks;
using TesteTecnicoEL.Dominio.Usuarios;
using TesteTecnicoEL.Dominio.Usuarios.Repositorios;

namespace TesteTecnicoEL.Infraestrutura.Memoria.Usuarios
{
    public class ClienteRepositorio : RepositorioMemoriaBase<Cliente>, IClienteRepositorio
    {
        public override Task Inserir(Cliente obj)
        {
            return base.Inserir(obj);
        }
        public Task<Cliente> ObterPorCpf(string cpf)
        {
            return Task.FromResult(Itens.FirstOrDefault(c => c.CPF == cpf));
        }

        public Task<Cliente> ObterPorChave(string chave)
        {
            return Task.FromResult(Itens.FirstOrDefault(c => c.ChaveAutenticacao == chave));
        }
    }
}
