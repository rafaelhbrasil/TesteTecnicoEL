using System.Collections.Generic;
using System.Threading.Tasks;

namespace TesteTecnicoEL.Dominio.Veiculos.Repositorios
{
    public interface IMarcaRepositorio : IRepositorioBase<Marca>
    {
        Task<List<Marca>> Listar();
    }
}
