using System.Threading.Tasks;

namespace TesteTecnicoEL.Dominio.Usuarios.Repositorios
{
    public interface IOperadorRepositorio : IRepositorioBase<Operador>
    {
        Task<Operador> ObterPorMatricula(string matricula);
    }
}
