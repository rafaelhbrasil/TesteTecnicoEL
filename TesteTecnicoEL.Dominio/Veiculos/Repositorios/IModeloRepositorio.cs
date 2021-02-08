using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TesteTecnicoEL.Dominio.Veiculos.Repositorios
{
    public interface IModeloRepositorio : IRepositorioBase<Modelo>
    {
        Task<List<Modelo>> ListarPorMarca(long idMarca);
    }
}
