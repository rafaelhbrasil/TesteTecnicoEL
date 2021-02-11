using System.Threading.Tasks;
using TesteTecnicoEL.Dominio.Usuarios.Repositorios;

namespace TesteTecnicoEL.Dominio.Usuarios.Servicos
{
    public interface IServicoCadastro
    {
        Task Cadastrar(Cliente cliente);
        Task Cadastrar(Operador operador);
    }
}
