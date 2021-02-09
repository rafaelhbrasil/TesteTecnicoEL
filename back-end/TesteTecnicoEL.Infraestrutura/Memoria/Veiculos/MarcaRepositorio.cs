using System.Collections.Generic;
using System.Threading.Tasks;
using TesteTecnicoEL.Dominio.Veiculos;
using TesteTecnicoEL.Dominio.Veiculos.Repositorios;

namespace TesteTecnicoEL.Infraestrutura.Memoria.Veiculos
{
    public class MarcaRepositorio : RepositorioMemoriaBase<Marca>, IMarcaRepositorio
    {
        public Task<List<Marca>> Listar()
        {
            return Task.FromResult(Itens);
        }
    }
}
