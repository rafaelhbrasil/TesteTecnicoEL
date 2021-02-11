using System.Collections.Generic;
using System.Threading.Tasks;
using TesteTecnicoEL.Dominio.Veiculos;

namespace TesteTecnicoEL.AcessoDados
{
    public interface IVeiculoRepositorio
    {
        Task<List<Veiculo>> ListarVeiculos();
        Task<Veiculo> ObterPorId(long id);
    }
}
