using System.Threading.Tasks;
using TesteTecnicoEL.Api.Models;
using TesteTecnicoEL.Dominio.Usuarios;

namespace TesteTecnicoEL.AcessoDados
{
    public interface IUsuarioRepositorio
    {
        Task<object> Autenticar(string login, string senha);
        Task<Cliente> CadastrarCliente(ClienteDto cliente);
    }
}
